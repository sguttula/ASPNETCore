using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppExample.Models;

namespace WebAppExample.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext db;

        public EmployeeService(AppDbContext db)
        {
            this.db = db;
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.Include(e => e.Supervisor).OrderBy(e => e.EmployeeId).ToList();
        }

        public Employee GetEmployee(int id)
        {
            return db.Employees.Where(e => e.EmployeeId == id).Include(e => e.Supervisor).SingleOrDefault();
        }
    }

    public class MockEmployeeService : IEmployeeService
    {
        private Employee[] employees =
        {
            new Employee
            {
                EmployeeId = 1,
                Name = "John",
                DateHired = new DateTime(2015, 1, 10)
            },
            new Employee
            {
                EmployeeId = 2,
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
