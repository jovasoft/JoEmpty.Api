using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addedcityanddistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Clients");
        }
    }
}
