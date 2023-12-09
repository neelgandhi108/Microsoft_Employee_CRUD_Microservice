using Microsoft.AspNetCore.Mvc;
using Microsoft_Employee_CRUD_Microservice.Models.Domain;
using Microsoft_Employee_CRUD_Microservice.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft_Employee_CRUD_Microservice.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _memoryCache;
        private static readonly string EMPLOYEES = "employees";

        public EmployeeController(IEmployeeService employeeService, IMemoryCache memoryCache)
        {
            _employeeService = employeeService;
            _memoryCache = memoryCache;
        }

        /// Display CRUD operations on billions of employees. Note that this is the index page for the API but it's used to be more flexible and easy to read.
        /// 
        /// 
        /// @return ViewResult for the CRUD operations page ( empty if successful ). The result is a JSON object with the following fields : OperationID - ID of the operation OperationState - State of the operation
        public IActionResult Index()
        {
            ViewBag.prompt = "Design a microservice for CRUD operations on billions of Employee entities, ensuring high request-per-second (RPS) capability and public API exposure.";
            ViewData["prompt2"] = "Deliverables include a design document with assumptions, high-level architecture, and a GitHub repository link containing controllers and interfaces for backend operations, guided by Microsoft's ASP.NET MVC Controller Overview.";
            //ViewBag and ViewData can send data only from ControllerToView

            //Tempdata can send data from one controller method to another controller method
            TempData["prompt3"] = "Include test case outlines for each CRUD API, focusing on scalability and robustness, with implementation in C# or Java preferred.";
            return View();
        }

        /// The add employee view. This is the view for adding an employee to the data store.
        /// 
        /// 
        /// @return The view for adding an employee to the data store or an error if something goes wrong with the
        public IActionResult AddEmployee()
        {
            return View();
        }

        /// Adds the employee to the data store and redirects to the AddEmployee view. If the addition was successful the view is shown to the user with a message that they can see in the email
        /// 
        /// @param employee - The employee to be added
        /// 
        /// @return An ActionResult that redirects the user to the AddEmployee view if the addition was successful or a ViewResult with an error message
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            /// Returns the view that is currently being displayed.
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _employeeService.AddEmployee(employee);
                TempData["msg"] = "Added successfully";
                return RedirectToAction("AddEmployee");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not add!";
                return View();
            }
        }

        /// Displays the Employees view. This is the view that allows the user to select a list of employees.
        /// 
        /// 
        /// @return An ActionResult with the view data to render in the response to the request or null if the user is not logged in
        [HttpGet]
        public IActionResult DisplayEmployees()
        {

            // Try to get the cached employees
            if (!_memoryCache.TryGetValue("employees", out IEnumerable<Employee> employees))
            {
                // Key not in cache, so get data from the service
                employees = _employeeService.GetAllEmployees();

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // or use whatever expiration makes sense for your scenario

                // Save data in cache
                _memoryCache.Set("employees", employees, cacheEntryOptions);
            }

            return View(employees);
        }


        /// View for editing an employee. Used to add or edit a user's profile. This is a non - standard view that allows the user to edit the details of an employee without needing to create a new profile.
        /// 
        /// @param id - Employee id to edit. If it is 0 then an error will be shown.
        /// 
        /// @return View showing the details of an employee that was edited or null if there was no such employee
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            return View(employee);
        }

        /// Edits the employee and redirects to DisplayEmployees. This is a POST method. It should be used in conjunction with a redirect to the edit employee form
        /// 
        /// @param employee - The employee to be edited
        /// 
        /// @return An action result containing the view to display the employees of the employee that was edited or a redirect to the edit
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            /// Returns the view that is currently being displayed.
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _employeeService.UpdateEmployee(employee);
                return RedirectToAction("DisplayEmployees");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update!";
                return View();
            }
        }

        /// Deletes Employee with specified id. Redirects to DisplayEmployees action. Errors are swallowed by this method
        /// 
        /// @param id - Id of employee to delete
        /// 
        /// @return Action Result with list of employees with success or error message to display on failure. 
        /// 

        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return RedirectToAction("DisplayEmployees");
        }
    }
}

