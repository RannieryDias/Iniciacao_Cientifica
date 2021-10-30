using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class ChangeFKToAInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tramitacao_ProjetoDetalhado_projetoid",
                table: "Tramitacao");

            migrationBuilder.DropIndex(
                name: "IX_Tramitacao_projetoid",
                table: "Tramitacao");

            migrationBuilder.RenameColumn(
                name: "projetoid",
                table: "Tramitacao",
                newName: "projetoId");

            migrationBuilder.AlterColumn<int>(
                name: "projetoId",
                table: "Tramitacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "projetoId",
                table: "Tramitacao",
                newName: "projetoid");

            migrationBuilder.AlterColumn<int>(
                name: "projetoid",
                table: "Tramitacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_projetoid",
                table: "Tramitacao",
                column: "projetoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tramitacao_ProjetoDetalhado_projetoid",
                table: "Tramitacao",
                column: "projetoid",
                principalTable: "ProjetoDetalhado",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
