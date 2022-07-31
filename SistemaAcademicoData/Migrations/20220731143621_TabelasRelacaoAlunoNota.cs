using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class TabelasRelacaoAlunoNota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAulasValidas",
                table: "DisciplinasAlunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotaAlunos",
                columns: table => new
                {
                    NotaAlunoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplinaAlunoId = table.Column<int>(type: "int", nullable: false),
                    NotaAvaliacao1 = table.Column<double>(type: "float", nullable: false),
                    NotaAvaliacao2 = table.Column<double>(type: "float", nullable: false),
                    ProvaFinal = table.Column<double>(type: "float", nullable: true),
                    NotaFinal = table.Column<double>(type: "float", nullable: false),
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaAlunos");

            migrationBuilder.DropColumn(
                name: "TotalAulasValidas",
                table: "DisciplinasAlunos");
        }
    }
}
