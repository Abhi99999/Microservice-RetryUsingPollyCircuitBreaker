using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Achivement> Achivement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Achivement>(entity =>
            {
                entity.HasOne(d => d.Driver)
                      .WithMany(p => p.Achivements)
                      .HasForeignKey(d => d.DriverID)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Achievement_Driver");
            });
        }
    }
}
