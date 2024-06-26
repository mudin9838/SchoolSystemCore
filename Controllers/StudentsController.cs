﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolSystemCore.Data.Constants;
using SchoolSystemCore.Models;
using SchoolSystemCore.Service;


namespace SchoolSystemCore.Controllers;

public class StudentsController : Controller
{
    private readonly CollegeDbContext _context;
    AzureBlobService _service;

    public StudentsController(CollegeDbContext context, AzureBlobService service)
    {
        _context = context;
        _service = service;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        var collegeDbContext = _context.Students.Include(s => s.Department);
        return View(await collegeDbContext.ToListAsync());
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
        return student == null ? NotFound() : View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName");
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,StudentName,Email,Addresss,DOB,FileName,DepartmentId")] Student student, IFormFile file)
    {

        if (student != null)
        {
            var response = await _service.UploadFiles(file);
            student.FileName = $"{BlobClientUri.BlobClientUrl}/{file.FileName}";
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName", student.DepartmentId);
        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName", student.DepartmentId);
        return View(student);
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,Email,Addresss,DOB,FileName,DepartmentId")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName", student.DepartmentId);
        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
        return student == null ? NotFound() : View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
