using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlteracoesModelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_AspNetUsers_UserId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_AspNetUsers_UserId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_SetorId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_UserId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_UserId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "SetorId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DisciplinasAlunos",
                columns: table => new
                {
                    DisciplinasAlunosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinasAlunos", x => x.DisciplinasAlunosId);
                    table.ForeignKey(
                        name: "FK_DisciplinasAlunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisciplinasAlunos_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "DisplicinaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasAlunos_AlunoId",
                table: "DisciplinasAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasAlunos_DisciplinaId",
                table: "DisciplinasAlunos",
                column: "DisciplinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SetorId",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Funcionarios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Alunos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    SetorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.SetorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_SetorId",
                table: "Funcionarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_UserId",
                table: "Funcionarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_UserId",
                table: "Alunos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_AspNetUsers_UserId",
                table: "Alunos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_AspNetUsers_UserId",
                table: "Funcionarios",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Setores_SetorId",
                table: "Funcionarios",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "SetorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
