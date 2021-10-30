using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class Added3MoreColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "codAprovado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "codPlenario",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "diff",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "Autor",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codAprovado",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "codPlenario",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "diff",
                table: "ProjetoDetalhado");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Autor",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
