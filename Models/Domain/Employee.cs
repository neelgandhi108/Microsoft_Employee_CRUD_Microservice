
using System.ComponentModel.DataAnnotations;

namespace Microsoft_Employee_CRUD_Microservice.Models.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
