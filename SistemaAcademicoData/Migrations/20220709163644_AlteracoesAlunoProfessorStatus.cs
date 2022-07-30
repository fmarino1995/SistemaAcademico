using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlteracoesAlunoProfessorStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Alunos");
        }
    }
}
