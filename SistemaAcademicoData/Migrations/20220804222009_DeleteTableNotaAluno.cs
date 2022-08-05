using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class DeleteTableNotaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaAlunos");

            migrationBuilder.RenameColumn(
                name: "DisplicinaId",
                table: "Disciplinas",
                newName: "DisciplinaId");

            migrationBuilder.AddColumn<double>(
                name: "NotaAvaliacao1",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NotaAvaliacao2",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NotaFinal",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProvaFinal",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeFalta",
                table: "DisciplinasAlunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadePresenca",
                table: "DisciplinasAlunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotaAvaliacao1",
                table: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "NotaAvaliacao2",
                table: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "NotaFinal",
                table: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "ProvaFinal",
                table: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "QuantidadeFalta",
                table: "DisciplinasAlunos");

            migrationBuilder.DropColumn(
                name: "QuantidadePresenca",
                table: "DisciplinasAlunos");

            migrationBuilder.RenameColumn(
                name: "DisciplinaId",
                table: "Disciplinas",
                newName: "DisplicinaId");

            migrationBuilder.CreateTable(
                name: "NotaAlunos",
                columns: table => new
                {
                    NotaAlunoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplinaAlunoId = table.Column<int>(type: "int", nullable: false),
                    NotaAvaliacao1 = table.Column<double>(type: "float", nullable: false),
                    NotaAvaliacao2 = table.Column<double>(type: "float", nullable: false),
                    NotaFinal = table.Column<double>(type: "float", nullable: false),
                    ProvaFinal = table.Column<double>(type: "float", nullable: true),
                    QuantidadeFalta = table.Column<int>(type: "int", nullable: false),
                    QuantidadePresenca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaAlunos", x => x.NotaAlunoId);
                    table.ForeignKey(
                        name: "FK_NotaAlunos_DisciplinasAlunos_DisciplinaAlunoId",
                        column: x => x.DisciplinaAlunoId,
                        principalTable: "DisciplinasAlunos",
                        principalColumn: "DisciplinasAlunosId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotaAlunos_DisciplinaAlunoId",
                table: "NotaAlunos",
                column: "DisciplinaAlunoId");
        }
    }
}
