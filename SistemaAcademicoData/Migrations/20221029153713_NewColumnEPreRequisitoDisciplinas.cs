using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class NewColumnEPreRequisitoDisciplinas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EPreRequisito",
                table: "Disciplinas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EPreRequisito",
                table: "Disciplinas");
        }
    }
}
