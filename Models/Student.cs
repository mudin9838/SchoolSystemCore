namespace SchoolSystemCore.Models;

public class Student
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string Email { get; set; }
    public string Addresss { get; set; }

    public DateTime DOB { get; set; }
    public string FileName { get; set; }

    public int? DepartmentId { get; set; }// student can have a dep't/can't have a dep't

    public virtual Department? Department { get; set; } //one student can only have one department not a list 
}

