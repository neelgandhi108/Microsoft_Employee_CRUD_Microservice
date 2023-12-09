using Microsoft_Employee_CRUD_Microservice.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft_Employee_CRUD_Microservice.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseContext _ctx;

        public EmployeeService(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        /// Gets all employees. This is a view only and does not require authentication. Use Employee. IsAuthenticated to check if you have authenticated.
        /// 
        /// 
        /// @return List of all employees in the system or empty list if not authenticated ( 404 ) or there are no
        public List<Employee> GetAllEmployees()
        {
            return _ctx.Employees.ToList();
        }

        /// Gets employee by id. This method is used to get employee by id. If not found it returns null
        /// 
        /// @param id - Id of Employee to find
        /// 
        /// @return Employee or null if not found or error while querying Employee. Find ( id ) method in
        public Employee GetEmployeeById(int id)
        {
            return _ctx.Employees.Find(id);
        }

        /// Adds an employee to the database. This is a convenience method for adding a single employee to the data source.
        /// 
        /// @param employee - Employee to be added to the
        public void AddEmployee(Employee employee)
        {
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();
        }

        /// Update Employee in the database. This will be used in conjunction with UpdatePersonalized () to update Personalized information
        /// 
        /// @param employee - Employee object to be
        public void UpdateEmployee(Employee employee)
        {
            _ctx.Employees.Update(employee);
            _ctx.SaveChanges();
        }

        /// Deletes an employee from the data store. This is used to prevent accidental deletion of a non - existent employee
        /// 
        /// @param id - The id of the employee to
        public void DeleteEmployee(int id)
        {
            var employee = _ctx.Employees.Find(id);
            /// Remove employee from employee list and save changes
            if (employee != null)
            {
                _ctx.Employees.Remove(employee);
                _ctx.SaveChanges();
            }
        }
    }
}
