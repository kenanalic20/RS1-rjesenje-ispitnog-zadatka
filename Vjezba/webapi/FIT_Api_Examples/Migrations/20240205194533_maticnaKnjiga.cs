using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class maticnaKnjiga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaticnaKnjiga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    godinaStudija = table.Column<int>(type: "int", nullable: true),
                    gijenaSkolarine = table.Column<float>(type: "real", nullable: true),
                    obnova = table.Column<bool>(type: "bit", nullable: true),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    akademskaGodinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaticnaKnjiga", x => x.id);
                    table.ForeignKey(
                        name: "FK_MaticnaKnjiga_AkademskaGodina_akademskaGodinaId",
                        column: x => x.akademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaticnaKnjiga_akademskaGodinaId",
                table: "MaticnaKnjiga",
                column: "akademskaGodinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaticnaKnjiga");
        }
    }
}
