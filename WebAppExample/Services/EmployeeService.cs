using System;
using System.Collections.Generic;
using System.Linq;
using WebAppExample.Models;

namespace WebAppExample.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int id);
    }

    public class MockEmployeeService : IEmployeeService
    {
        private Employee[] employees =
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                DateHired = new DateTime(2015, 1, 10)
            },
            new Employee
            {
                Id = 2,
                Name = "Jane",
                DateHired = new DateTime(2015, 2, 20),
                SupervisorId = 1
            }
        };

        public List<Employee> GetEmployees()
        {
            return employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return employees[id - 1];
        }
    }
}
