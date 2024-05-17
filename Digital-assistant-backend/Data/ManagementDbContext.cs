using Digital_assistant_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend.Data
{
    public class ManagementDbContext : DbContext
    {
        public ManagementDbContext(DbContextOptions<ManagementDbContext>options):base(options)
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Dashboard>  Dashboards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dashboard>()
                .HasOne(d => d.User)
                .WithOne(u => u.Dashboard)
                .HasForeignKey<Dashboard>(d => d.UserId);

            modelBuilder.Entity<Project>()
                .HasOne<User>(d=>d.User)
                .WithMany(u=>u.Projects)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Project>()
                 .HasMany<ProjectTask>(d => d.Tasks)
                 .WithOne(u => u.Project)
                 .HasForeignKey(d => d.ProjectId);

            base.OnModelCreating(modelBuilder);
            
        }

    }
}
