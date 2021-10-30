using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjetoDetalhado_StatusProposicao_statusProposicaoid",
                table: "ProjetoDetalhado");

            migrationBuilder.DropIndex(
                name: "IX_ProjetoDetalhado_statusProposicaoid",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "statusProposicaoid",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "uriOrgaoNumerador",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "uri",
                table: "Autor");

            migrationBuilder.AddColumn<int>(
                name: "projetoid",
                table: "StatusProposicao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "foiAPlenario",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "foiAprovado",
                table: "ProjetoDetalhado",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "codDeputado",
                table: "Autor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tramitacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    projetoid = table.Column<int>(type: "int", nullable: true),
                    dataHora = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sequencia = table.Column<int>(type: "int", nullable: false),
                    siglaOrgao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uriOrgao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uriUltimoRelator = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricaoTramitacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codTipoTramitacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricaoSituacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codSituacao = table.Column<int>(type: "int", nullable: true),
                    despacho = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ambito = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramitacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tramitacao_ProjetoDetalhado_projetoid",
                        column: x => x.projetoid,
                        principalTable: "ProjetoDetalhado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StatusProposicao_projetoid",
                table: "StatusProposicao",
                column: "projetoid");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_projetoid",
                table: "Tramitacao",
                column: "projetoid");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusProposicao_ProjetoDetalhado_projetoid",
                table: "StatusProposicao",
                column: "projetoid",
                principalTable: "ProjetoDetalhado",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusProposicao_ProjetoDetalhado_projetoid",
                table: "StatusProposicao");

            migrationBuilder.DropTable(
                name: "Tramitacao");

            migrationBuilder.DropIndex(
                name: "IX_StatusProposicao_projetoid",
                table: "StatusProposicao");

            migrationBuilder.DropColumn(
                name: "projetoid",
                table: "StatusProposicao");

            migrationBuilder.DropColumn(
                name: "foiAPlenario",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "foiAprovado",
                table: "ProjetoDetalhado");

            migrationBuilder.DropColumn(
                name: "codDeputado",
                table: "Autor");

            migrationBuilder.AddColumn<int>(
                name: "statusProposicaoid",
                table: "ProjetoDetalhado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uriOrgaoNumerador",
                table: "ProjetoDetalhado",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "uri",
                table: "Autor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoDetalhado_statusProposicaoid",
                table: "ProjetoDetalhado",
                column: "statusProposicaoid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjetoDetalhado_StatusProposicao_statusProposicaoid",
                table: "ProjetoDetalhado",
                column: "statusProposicaoid",
                principalTable: "StatusProposicao",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
