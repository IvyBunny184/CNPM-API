using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortProjectAPI.ModelEF
{
    public class WeddingDBContext: DbContext
    {
        public WeddingDBContext(DbContextOptions<WeddingDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListServices>()
                .HasKey(x => new { x.BookingID, x.ServiceID });
            modelBuilder.Entity<Menu>()
                .HasKey(x => new { x.BookingID, x.FoodID });
        }

        #region Models DBSet
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<ListServices> ListServices { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<TypeOfPayment> TypeOfPayments { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<ImageOfHall> ImageOfHalls { get; set; }
        public DbSet<TypeOfHall> TypeOfHalls { get; set; }
        #endregion
    }
}
