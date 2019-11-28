using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatedcitytoprovince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Facilities",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clients",
                newName: "Province");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Facilities",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Clients",
                newName: "City");
        }
    }
}
