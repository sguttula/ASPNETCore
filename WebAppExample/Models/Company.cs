using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppExample.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime DateHired { get; set; }

        public int? SupervisorId { get; set; }
        public Employee Supervisor { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
    }

    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public int? LeaderId { get; set; }
        public Employee Leader { get; set; }

        public List<ProjectMember> Members { get; set; }
    }

    [Table("ProjectMembers")]
    public class ProjectMember
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
