using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class ColumnDiff2Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "diff",
                table: "ProjetoDetalhado",
                newName: "diffPlenario");

            migrationBuilder.AddColumn<bool>(
                name: "diffAprovado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diffAprovado",
                table: "ProjetoDetalhado");

            migrationBuilder.RenameColumn(
                name: "diffPlenario",
                table: "ProjetoDetalhado",
                newName: "diff");
        }
    }
}
