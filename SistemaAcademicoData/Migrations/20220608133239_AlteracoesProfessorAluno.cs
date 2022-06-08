using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlteracoesProfessorAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Funcionarios_FuncionarioId",
                table: "Professores");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "Professores",
                newName: "EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Professores_FuncionarioId",
                table: "Professores",
                newName: "IX_Professores_EnderecoId");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraCadastro",
                table: "Professores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacao",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Enderecos_EnderecoId",
                table: "Professores",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Enderecos_EnderecoId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "DataHoraCadastro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacao",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "Professores",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Professores_EnderecoId",
                table: "Professores",
                newName: "IX_Professores_FuncionarioId");

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EnderecoId",
                table: "Funcionarios",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Funcionarios_FuncionarioId",
                table: "Professores",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
