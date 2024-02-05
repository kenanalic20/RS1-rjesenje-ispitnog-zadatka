using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class maticnaKnjiga2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gijenaSkolarine",
                table: "MaticnaKnjiga",
                newName: "cijenaSkolarine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cijenaSkolarine",
                table: "MaticnaKnjiga",
                newName: "gijenaSkolarine");
        }
    }
}
