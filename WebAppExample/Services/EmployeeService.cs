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
        Employee GetEmployee(string username);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee update);
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

        public Employee GetEmployee(string username)
        {
            return db.Employees.Where(e => e.Username.ToUpper() == username.ToUpper()).SingleOrDefault();
        }

        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void UpdateEmployee(Employee update)
        {
            Employee employee = db.Employees.Find(update.EmployeeId);
            employee.Name = update.Name;
            employee.DateHired = update.DateHired;
            employee.SupervisorId = update.SupervisorId;
            db.SaveChanges();
        }
    }

    public class MockEmployeeService : IEmployeeService
    {
        private List<Employee> employees = new List<Employee>();

        public MockEmployeeService()
        {
            var john = new Employee
            {
                EmployeeId = 1,
                Name = "John",
                DateHired = new DateTime(2015, 1, 10)
            };
            var jane = new Employee
            {
                EmployeeId = 2,
                Name = "Jane",
                DateHired = new DateTime(2015, 2, 20),
                SupervisorId = 1,
                Supervisor = john
            };
            employees.Add(john);
            employees.Add(jane);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return employees[id - 1];
        }

        public Employee GetEmployee(string username)
        {
            foreach (var employee in employees)
                if (employee.Username.ToUpper() == username.ToUpper())
                    return employee;
            return null;
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void UpdateEmployee(Employee update)
        {
            var employee = GetEmployee(update.EmployeeId);
            employee.Name = update.Name;
            employee.DateHired = update.DateHired;
        }
    }
}
