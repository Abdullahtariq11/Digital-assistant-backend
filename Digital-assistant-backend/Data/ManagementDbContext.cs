using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend.Data
{
    public class ManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public ManagementDbContext(DbContextOptions<ManagementDbContext>options):base(options)
        {
        
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.ApplicationUser)
                .WithMany(u=>u.Projects)
                .HasForeignKey(p => p.ApplicationUserId);

            modelBuilder.Entity<Project>()
                 .HasMany(p => p.Tasks)
                 .WithOne(t => t.Project)
                 .HasForeignKey(t => t.ProjectId);

            base.OnModelCreating(modelBuilder);
            
        }

    }
}
