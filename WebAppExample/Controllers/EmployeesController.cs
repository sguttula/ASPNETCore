using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppExample.Models;
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

        [Authorize]
        public IActionResult List()
        {
            var employees = employeeService.GetEmployees();
            ViewData["employees1"] = employees;
            ViewBag.employees2 = employees;
            return View(employees);
        }

        public IActionResult Two(int id1, int id2)
        {
            var employee1 = employeeService.GetEmployee(id1);
            var employee2 = employeeService.GetEmployee(id2);
            return View((employee1, employee2));
        }

        public IActionResult View(int id)
        {
            return View(employeeService.GetEmployee(id));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            employee.Username = employee.Name;
            employee.Password = "abcd";
            employeeService.AddEmployee(employee);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
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
        public IActionResult Edit(int id, Employee update)
        {
            update.EmployeeId = id;
            employeeService.UpdateEmployee(update);
            return RedirectToAction(nameof(List));
        }
    }
}
