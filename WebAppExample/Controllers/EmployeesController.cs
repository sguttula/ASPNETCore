using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppExample.Models;
using WebAppExample.Services;

namespace WebAppExample.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAuthorizationService authService;

        public EmployeesController(IEmployeeService employeeService, IAuthorizationService authorizationService)
        {
            this.employeeService = employeeService;
            this.authService = authorizationService;
        }

        public IActionResult List()
        {
            var employees = employeeService.GetEmployees();
            return View(employees);
        }

        public IActionResult Two(int id1, int id2)
        {
            var employee1 = employeeService.GetEmployee(id1);
            var employee2 = employeeService.GetEmployee(id2);
            return View((employee1, employee2));
        }

        public async Task<IActionResult> View(int id)
        {
            var authResult = await authService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            return View(employeeService.GetEmployee(id));
        }

        [HttpGet]
        [Authorize("IsAdmin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize("IsAdmin")]
        public IActionResult Add(Employee employee)
        {
            employee.Username = employee.Name;
            employee.Password = "abcd";
            employeeService.AddEmployee(employee);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var authResult = await authService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            ViewBag.Supervisors = employeeService.GetEmployees()
                .Where(e => e.EmployeeId != id)
                .Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.Name
                }).ToList();
            return View(employeeService.GetEmployee(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee update)
        {
            var authResult = await authService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
                return Forbid();

            update.EmployeeId = id;
            employeeService.UpdateEmployee(update);
            return RedirectToAction(nameof(List));
        }
    }
}
