using Microsoft.EntityFrameworkCore;
using Lab.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab.Data
{
    public class LabDbContext : IdentityDbContext<LabUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        public LabDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<LabUser>().Property(lu => lu.NameForLabApp)
            //    .HasColumnName("NameLab")
            //    .HasMaxLength(50)
            //    .IsUnicode();
        }
    }
}
