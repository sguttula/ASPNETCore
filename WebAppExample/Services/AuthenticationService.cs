using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAppExample.Services
{
    public class AuthenticationService
    {
        private readonly IEmployeeService employeeService;

        public AuthenticationService(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public ClaimsIdentity Authenticate(string username, string password)
        {
            var employee = employeeService.GetEmployee(username);
            if (employee == null || employee.Password != password) return null;

            var claims = new List<Claim>
            {
                new Claim("EmployeeId", employee.EmployeeId.ToString()),
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim(ClaimTypes.NameIdentifier, employee.Username)
            };
            if (employee.IsAdmin) claims.Add(new Claim("IsAdmin", "Yes"));

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
