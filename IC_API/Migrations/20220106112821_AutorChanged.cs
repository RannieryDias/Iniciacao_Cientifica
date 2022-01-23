using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class AutorChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Autor",
                table: "Autor");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Autor");

            migrationBuilder.AddColumn<int>(
                name: "idProjeto",
                table: "Autor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autor",
                table: "Autor",
                columns: new[] { "idProjeto", "codDeputado" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Autor",
                table: "Autor");

            migrationBuilder.DropColumn(
                name: "idProjeto",
                table: "Autor");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "Autor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autor",
                table: "Autor",
                column: "id");
        }
    }
}
