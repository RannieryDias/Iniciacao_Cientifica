using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class ChangeTramitacaoProjetoDetalhado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Tramitacao");

            migrationBuilder.DropColumn(
                name: "diffAprovado",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "diffPlenario",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "foiAprovado",
                table: "ProjetoDetalhado");

            migrationBuilder.RenameColumn(
                name: "foiAPlenario",
                table: "ProjetoDetalhado",
                newName: "arquivado");

            migrationBuilder.AlterColumn<bool>(
                name: "apensado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao",
                column: "sequencia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao");

            migrationBuilder.RenameColumn(
                name: "arquivado",
                table: "ProjetoDetalhado",
                newName: "foiAPlenario");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Tramitacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "apensado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<bool>(
                name: "diffAprovado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "diffPlenario",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "foiAprovado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao",
                column: "id");
        }
    }
}
