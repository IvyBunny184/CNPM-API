using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResortProjectAPI.Migrations
{
    public partial class CreateWeddingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfHalls",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfHalls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfPayments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfPayments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staffs_Permissions_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MaxTables = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Halls_TypeOfHalls_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TypeOfHalls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    HallID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GroomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrideName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Deposit = table.Column<float>(type: "real", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Halls_HallID",
                        column: x => x.HallID,
                        principalTable: "Halls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Shifts_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "Shifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageOfHalls",
                columns: table => new
                {
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HallID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOfHalls", x => x.Url);
                    table.ForeignKey(
                        name: "FK_ImageOfHalls_Halls_HallID",
                        column: x => x.HallID,
                        principalTable: "Halls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BookingID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Fee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bills_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_TypeOfPayments_PaymentType",
                        column: x => x.PaymentType,
                        principalTable: "TypeOfPayments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListServices",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ServiceID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListServices", x => new { x.BookingID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_ListServices_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListServices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FoodID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => new { x.BookingID, x.FoodID });
                    table.ForeignKey(
                        name: "FK_Menus_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BookingID",
                table: "Bills",
                column: "BookingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentType",
                table: "Bills",
                column: "PaymentType");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HallID",
                table: "Bookings",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShiftID",
                table: "Bookings",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_TypeID",
                table: "Halls",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageOfHalls_HallID",
                table: "ImageOfHalls",
                column: "HallID");

            migrationBuilder.CreateIndex(
                name: "IX_ListServices_ServiceID",
                table: "ListServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FoodID",
                table: "Menus",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_RoleID",
                table: "Staffs",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "ImageOfHalls");

            migrationBuilder.DropTable(
                name: "ListServices");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "TypeOfPayments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "TypeOfHalls");
        }
    }
}
