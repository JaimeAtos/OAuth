using Microsoft.EntityFrameworkCore;
using OAuth.Models;

namespace OAuth
{
    public class OAuthContext : DbContext
    {
        public OAuthContext(DbContextOptions<OAuthContext> opt) : base(opt)
        {

        }
        public DbSet<Users> User { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Users>()
                .HasIndex(user => user.Name)
                .IsUnique();

            modelBuilder.Entity<Users>()
                .Property(user => user.Name)
                .HasMaxLength(60).IsRequired();

            modelBuilder.Entity<Users>()
                .Property(user => user.Password)
                .HasMaxLength(60).IsRequired();

            modelBuilder.Entity<Role>()
                .HasIndex(role => role.RoleName)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(role => role.RoleId)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasData(new { RoleId = 1, RoleName = "Admin"},
                         new { RoleId = 2, RoleName = "Guest"},
                         new { RoleId = 3, RoleName = "Intrude" });

        }
    }
}
