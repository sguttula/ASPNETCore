using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppExample.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateHired { get; set; }

        public int? SupervisorId { get; set; }
        public Employee Supervisor { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LeaderId { get; set; }
        public Employee Leader { get; set; }

        public List<ProjectMember> Members { get; set; }
    }

    public class ProjectMember
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int MemberId { get; set; }
        public List<Employee> Members { get; set; }
    }
}
