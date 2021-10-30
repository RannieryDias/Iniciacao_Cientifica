using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class AddedApensadoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "apensado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apensado",
                table: "ProjetoDetalhado");
        }
    }
}
