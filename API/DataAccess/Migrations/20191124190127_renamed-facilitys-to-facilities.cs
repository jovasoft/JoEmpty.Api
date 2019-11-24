using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class renamedfacilitystofacilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Facilitys",
                table: "Facilitys");

            migrationBuilder.RenameTable(
                name: "Facilitys",
                newName: "Facilities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities");

            migrationBuilder.RenameTable(
                name: "Facilities",
                newName: "Facilitys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facilitys",
                table: "Facilitys",
                column: "Id");
        }
    }
}
