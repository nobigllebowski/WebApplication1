using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get;set; }
        public DbSet<Student> Students { get;set; }
        public DbSet<Department> Departments { get;set; }
        public DbSet<Grade> Grades { get;set; }
        public DbSet<Course> Courses { get;set; }
        public DbSet<LogEntry> Logs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(s => s.Department)
                      .WithMany(d => d.Students) 
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);  
            });
        }
    }
}
