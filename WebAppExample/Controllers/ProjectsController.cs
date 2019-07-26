using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppExample.Models;
using WebAppExample.Services;

namespace WebAppExample.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectService projectService;

        public ProjectsController(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult List()
        {
            return View(projectService.GetProjects());
        }

        public IActionResult View(int id)
        {
            return View(projectService.GetProject(id));
        }

        [HttpGet("api/projects")]
        public List<Project> GetProjects()
        {
            return projectService.GetProjects();
        }

        [HttpGet("api/projects/{id}")]
        public IActionResult GetProject(int id)
        {
            var project = projectService.GetProject(id);
            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }

        [HttpPost("api/projects/{projectId}/members/{employeeId}")]
        public IActionResult AddMember(int projectId, int employeeId)
        {
            var project = projectService.GetProject(projectId);
            project.Members.Add(new ProjectMember
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            });
            projectService.SaveChanges();

            return Ok();
        }

        [HttpDelete("api/projects/{projectId}/members/{employeeId}")]
        public IActionResult RemoveMember(int projectId, int employeeId)
        {
            var project = projectService.GetProject(projectId);
            if (project.LeaderId == employeeId)
                project.LeaderId = null;
            project.Members.RemoveAll(m => m.EmployeeId == employeeId);
            projectService.SaveChanges();

            return Ok();
        }
    }
}
