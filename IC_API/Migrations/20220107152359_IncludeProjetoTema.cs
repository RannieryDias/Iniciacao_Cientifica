using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class IncludeProjetoTema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjetoTema",
                columns: table => new
                {
                    idProjeto = table.Column<int>(type: "int", nullable: false),
                    idTema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoTema", x => new { x.idProjeto, x.idTema });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoTema");
        }
    }
}
