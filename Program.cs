using Azure.Storage.Blobs;
using SchoolSystemCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var sqlConnection = builder.Configuration["ConnectionStrings:DefaultConnection"];
var storageConnection = builder.Configuration["ConnectionStrings:Storage"];
builder.Services.AddSqlServer<CollegeDbContext>(sqlConnection,
    options => options.EnableRetryOnFailure());

builder.Services.AddSingleton(x => new BlobServiceClient(storageConnection));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
