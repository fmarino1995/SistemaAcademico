using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlterTablesAlunoProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Professores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Alunos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_ApplicationUserId",
                table: "Professores",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_ApplicationUserId",
                table: "Alunos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_AspNetUsers_ApplicationUserId",
                table: "Alunos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_AspNetUsers_ApplicationUserId",
                table: "Professores",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_AspNetUsers_ApplicationUserId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Professores_AspNetUsers_ApplicationUserId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_ApplicationUserId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_ApplicationUserId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Alunos");
        }
    }
}
