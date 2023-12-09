using Microsoft.EntityFrameworkCore;
using Microsoft_Employee_CRUD_Microservice.Models.Domain;
using Microsoft_Employee_CRUD_Microservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Register the EmployeeService as a scoped service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//whatever services are needed in application will be declared here

//..........................................
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// section for configuring middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Index method of home controller will be execute first thing in this application
// it is a default url
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=AddEmployee}/{id?}");

app.Run();
