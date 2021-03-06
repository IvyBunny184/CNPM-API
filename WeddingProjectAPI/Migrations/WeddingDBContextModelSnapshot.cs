// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResortProjectAPI.ModelEF;

namespace ResortProjectAPI.Migrations
{
    [DbContext(typeof(WeddingDBContext))]
    partial class WeddingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Bill", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("BookingID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("DateOfPayment")
                        .HasColumnType("datetime2");

                    b.Property<float>("Fee")
                        .HasColumnType("real");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.HasIndex("BookingID")
                        .IsUnique();

                    b.HasIndex("PaymentType");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Booking", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("BrideName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Deposit")
                        .HasColumnType("real");

                    b.Property<string>("GroomName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HallID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ShiftID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.HasIndex("HallID");

                    b.HasIndex("ShiftID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Food", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Describe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Hall", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Describe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxTables")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("TypeID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.HasIndex("TypeID");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.ImageOfHall", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HallID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Url");

                    b.HasIndex("HallID");

                    b.ToTable("ImageOfHalls");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.ListServices", b =>
                {
                    b.Property<string>("BookingID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("ServiceID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("BookingID", "ServiceID");

                    b.HasIndex("ServiceID");

                    b.ToTable("ListServices");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Menu", b =>
                {
                    b.Property<string>("BookingID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("FoodID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("BookingID", "FoodID");

                    b.HasIndex("FoodID");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Permission", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Service", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Describe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Shift", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Staff", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleID")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.TypeOfHall", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<float>("MinPrice")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("TypeOfHalls");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.TypeOfPayment", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Describe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("TypeOfPayments");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Bill", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Booking", "Booking")
                        .WithOne("Bill")
                        .HasForeignKey("ResortProjectAPI.ModelEF.Bill", "BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResortProjectAPI.ModelEF.TypeOfPayment", "TypeOfPayment")
                        .WithMany("Bills")
                        .HasForeignKey("PaymentType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("TypeOfPayment");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Booking", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Hall", "Hall")
                        .WithMany("Bookings")
                        .HasForeignKey("HallID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResortProjectAPI.ModelEF.Shift", "Shift")
                        .WithMany("Bookings")
                        .HasForeignKey("ShiftID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Hall", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.TypeOfHall", "TypeOfHall")
                        .WithMany("Halls")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeOfHall");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.ImageOfHall", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Hall", "Hall")
                        .WithMany("Images")
                        .HasForeignKey("HallID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.ListServices", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Booking", "Booking")
                        .WithMany("ListServices")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResortProjectAPI.ModelEF.Service", "Service")
                        .WithMany("ListServices")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Menu", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Booking", "Booking")
                        .WithMany("Menus")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResortProjectAPI.ModelEF.Food", "Food")
                        .WithMany("Menus")
                        .HasForeignKey("FoodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Staff", b =>
                {
                    b.HasOne("ResortProjectAPI.ModelEF.Permission", "Permission")
                        .WithMany("Staffs")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Booking", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("ListServices");

                    b.Navigation("Menus");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Food", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Hall", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Permission", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Service", b =>
                {
                    b.Navigation("ListServices");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.Shift", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.TypeOfHall", b =>
                {
                    b.Navigation("Halls");
                });

            modelBuilder.Entity("ResortProjectAPI.ModelEF.TypeOfPayment", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
