using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IC_API.Migrations
{
    public partial class ChangeTramitacao3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao");

            migrationBuilder.AlterColumn<int>(
                name: "sequencia",
                table: "Tramitacao",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao",
                columns: new[] { "projetoId", "sequencia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao");

            migrationBuilder.AlterColumn<int>(
                name: "sequencia",
                table: "Tramitacao",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tramitacao",
                table: "Tramitacao",
                column: "sequencia");
        }
    }
}
