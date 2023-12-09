/// This is the code generator for the Employee CRUD model. It should be used in conjunction with the EntityFramework. Core
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Microsoft_Employee_CRUD_Microservice.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) :base(opts)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
