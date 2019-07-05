using Microsoft.EntityFrameworkCore;
using WebAppExample.Models;

namespace WebAppExample.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectMember>().HasKey(m => new { m.ProjectId, m.MemberId });
        }
    }
}
