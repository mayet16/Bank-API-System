using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VirtualBank.Models
{
    public class GebyaContext: IdentityDbContext<Users, UserRoles, int>
    {
      
        public GebyaContext(DbContextOptions<GebyaContext> options) : base(options)
        {

        }
        public DbSet<Users> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().ToTable("User")
                .Property(p => p.Id).HasColumnName("U_ID");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }


    }
}
