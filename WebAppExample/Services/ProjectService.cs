using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppExample.Models;

namespace WebAppExample.Services
{
    public class ProjectService
    {
        private readonly AppDbContext db;

        public ProjectService(AppDbContext db)
        {
            this.db = db;
        }

        public List<Project> GetProjects()
        {
            return db.Projects.Include(p => p.Leader).ToList();
        }

        public Project GetProject(int id)
        {
            return db.Projects.Where(p => p.ProjectId == id)
                .Include(p => p.Leader)
                .Include(p => p.Members).ThenInclude(m => m.Employee)
                .SingleOrDefault();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
