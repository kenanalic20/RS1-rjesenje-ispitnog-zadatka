using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class maticnaKnjiga3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "studentId",
                table: "MaticnaKnjiga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaticnaKnjiga_studentId",
                table: "MaticnaKnjiga",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaticnaKnjiga_Student_studentId",
                table: "MaticnaKnjiga",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaticnaKnjiga_Student_studentId",
                table: "MaticnaKnjiga");

            migrationBuilder.DropIndex(
                name: "IX_MaticnaKnjiga_studentId",
                table: "MaticnaKnjiga");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "MaticnaKnjiga");
        }
    }
}
