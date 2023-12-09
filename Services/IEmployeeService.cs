using Microsoft_Employee_CRUD_Microservice.Models.Domain;
using System.Collections.Generic;

namespace Microsoft_Employee_CRUD_Microservice.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
