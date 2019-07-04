using Microsoft.AspNetCore.Mvc;
using WebAppExample.Services;

namespace WebAppExample.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public IActionResult List()
        {
            return View(employeeService.GetEmployees());
        }
    }
}
