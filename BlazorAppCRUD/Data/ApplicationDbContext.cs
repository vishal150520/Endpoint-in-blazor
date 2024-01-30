using BlazorAppCRUD.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Serialization;

namespace BlazorAppCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Order> order { get; set; }
        public DbSet<SubElement> subelement { get; set; }
        public DbSet<Window> window { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Window>()
                .HasOne(w => w.Orders)
                .WithMany(o => o.Windows)
                .HasForeignKey(w => w.OrderId);

            modelBuilder.Entity<SubElement>()
                .HasOne(s => s.Windows)
                .WithMany(w => w.SubElements)
                .HasForeignKey(s => s.WindowId);
        }
    };
}
