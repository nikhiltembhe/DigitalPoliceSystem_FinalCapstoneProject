using DigitalPoliceSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalPoliceSystem.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ComplaintCategory> ComplaintCategories { get; set; }

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<DigitalPoliceSystem.Models.PoliceStation> PoliceStation { get; set; }

        public DbSet<DigitalPoliceSystem.Models.ComplaintState> ComplaintState { get; set; }

        public DbSet<DigitalPoliceSystem.Models.UserProperty> UserProperty { get; set; }
    }
}
