// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Industry> Industries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure UserName (Login) as unique
            builder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();

            // Seed data
            builder.Entity<Industry>().HasData(
                new Industry { Id = 1, Name = "Technology" },
                new Industry { Id = 2, Name = "Finance" },
                new Industry { Id = 3, Name = "Healthcare" },
                new Industry { Id = 4, Name = "Education" },
                new Industry { Id = 5, Name = "Manufacturing" }
            );
        }
    }
}
