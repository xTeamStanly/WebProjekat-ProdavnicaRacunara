using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjekat.Migrations
{
    public partial class Web1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MemoryGB = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    FrequencyGHz = table.Column<double>(type: "float", nullable: true),
                    Cores = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MemoryGB = table.Column<int>(type: "int", nullable: false),
                    FrequencyMHz = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MemoryGB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CPUID = table.Column<int>(type: "int", nullable: true),
                    GPUID = table.Column<int>(type: "int", nullable: true),
                    RAMID = table.Column<int>(type: "int", nullable: true),
                    MBID = table.Column<int>(type: "int", nullable: true),
                    STORAGEID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Configurations_GraphicsCards_GPUID",
                        column: x => x.GPUID,
                        principalTable: "GraphicsCards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configurations_Motherboards_MBID",
                        column: x => x.MBID,
                        principalTable: "Motherboards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configurations_Processors_CPUID",
                        column: x => x.CPUID,
                        principalTable: "Processors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configurations_RAMs_RAMID",
                        column: x => x.RAMID,
                        principalTable: "RAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configurations_Storages_STORAGEID",
                        column: x => x.STORAGEID,
                        principalTable: "Storages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vendors_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    VendorID = table.Column<int>(type: "int", nullable: false),
                    ConfigurationID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchases_Configurations_ConfigurationID",
                        column: x => x.ConfigurationID,
                        principalTable: "Configurations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorContacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorID = table.Column<int>(type: "int", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VendorContacts_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CPUID",
                table: "Configurations",
                column: "CPUID");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_GPUID",
                table: "Configurations",
                column: "GPUID");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_MBID",
                table: "Configurations",
                column: "MBID");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_RAMID",
                table: "Configurations",
                column: "RAMID");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_STORAGEID",
                table: "Configurations",
                column: "STORAGEID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerID",
                table: "CustomerContacts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ConfigurationID",
                table: "Purchases",
                column: "ConfigurationID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CustomerID",
                table: "Purchases",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_StoreID",
                table: "Purchases",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_VendorID",
                table: "Purchases",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorContacts_VendorID",
                table: "VendorContacts",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_StoreID",
                table: "Vendors",
                column: "StoreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "VendorContacts");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "GraphicsCards");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "RAMs");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
