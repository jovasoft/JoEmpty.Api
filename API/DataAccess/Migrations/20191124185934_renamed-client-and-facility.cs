using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class renamedclientandfacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.RenameColumn(
                name: "UnitCount",
                table: "Contracts",
                newName: "FacilityCount");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Contracts",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Areas",
                newName: "FacilityId");

            migrationBuilder.CreateTable(
                name: "ClientContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    InternalNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CurrentCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilitys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Station = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    WarrantyFinishDate = table.Column<DateTime>(nullable: true),
                    MaintenanceStatus = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    CurrentMaintenanceFee = table.Column<decimal>(nullable: false),
                    OldMaintenanceFee = table.Column<decimal>(nullable: false),
                    BreakdownFee = table.Column<decimal>(nullable: false),
                    Brand = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilitys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientContacts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Facilitys");

            migrationBuilder.RenameColumn(
                name: "FacilityCount",
                table: "Contracts",
                newName: "UnitCount");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Contracts",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "Areas",
                newName: "UnitId");

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    InternalNumber = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MailAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    CurrentCode = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    BreakdownFee = table.Column<decimal>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: false),
                    CurrentMaintenanceFee = table.Column<decimal>(nullable: false),
                    MaintenanceStatus = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OldMaintenanceFee = table.Column<decimal>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    Station = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    WarrantyFinishDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });
        }
    }
}
