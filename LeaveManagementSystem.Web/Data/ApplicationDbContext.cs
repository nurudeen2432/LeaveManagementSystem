using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                Id = "58be2e73-bfb7-4ea1-93a3-77cd3fb709d4",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
                
                
                },
                new IdentityRole 
                {
                    Id = "0550cad7-5e34-41f9-ae0a-47684276b90e",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR",
                },
                new IdentityRole 
                {

                    Id = "3b14c17f-e1b5-4bfb-b690-59cf0b071031",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                }
                );
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData
                (
                new ApplicationUser
                {  
                    Id = "df4117f5-e8ba-4e41-81dc-497942b8d686",
                    Email="admin@localhost.com",
                    NormalizedEmail="ADMIN@LOCALHOST.COM",
                    UserName="admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed=true,
                    FirstName="Default",
                    LastName="Admin",
                    DateOfBirth= new DateOnly(1965,12,01)
                }
                );

            builder.Entity<IdentityUserRole<string>>().HasData
                (
                new IdentityUserRole<string>
                {
                    RoleId= "3b14c17f-e1b5-4bfb-b690-59cf0b071031",
                    UserId= "df4117f5-e8ba-4e41-81dc-497942b8d686"
                }
                );
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

      

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<Period> Periods { get; set; }
        
    }
}
