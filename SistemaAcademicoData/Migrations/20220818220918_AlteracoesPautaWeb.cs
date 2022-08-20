using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlteracoesPautaWeb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "PresencaAlunos");

            migrationBuilder.DropColumn(
                name: "Ano",
                table: "DisciplinasAlunos");

            migrationBuilder.RenameColumn(
                name: "Semestre",
                table: "PresencaAlunos",
                newName: "SemestreVigenteId");

            migrationBuilder.RenameColumn(
                name: "Semestre",
                table: "DisciplinasAlunos",
                newName: "SemestreVigenteId");

            migrationBuilder.AlterColumn<double>(
                name: "NotaFinal",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "NotaAvaliacao2",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "NotaAvaliacao1",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_PresencaAlunos_SemestreVigenteId",
                table: "PresencaAlunos",
                column: "SemestreVigenteId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasAlunos_SemestreVigenteId",
                table: "DisciplinasAlunos",
                column: "SemestreVigenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasAlunos_SemestresVigentes_SemestreVigenteId",
                table: "DisciplinasAlunos",
                column: "SemestreVigenteId",
                principalTable: "SemestresVigentes",
                principalColumn: "SemestreVigenteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PresencaAlunos_SemestresVigentes_SemestreVigenteId",
                table: "PresencaAlunos",
                column: "SemestreVigenteId",
                principalTable: "SemestresVigentes",
                principalColumn: "SemestreVigenteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasAlunos_SemestresVigentes_SemestreVigenteId",
                table: "DisciplinasAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_PresencaAlunos_SemestresVigentes_SemestreVigenteId",
                table: "PresencaAlunos");

            migrationBuilder.DropIndex(
                name: "IX_PresencaAlunos_SemestreVigenteId",
                table: "PresencaAlunos");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinasAlunos_SemestreVigenteId",
                table: "DisciplinasAlunos");

            migrationBuilder.RenameColumn(
                name: "SemestreVigenteId",
                table: "PresencaAlunos",
                newName: "Semestre");

            migrationBuilder.RenameColumn(
                name: "SemestreVigenteId",
                table: "DisciplinasAlunos",
                newName: "Semestre");

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "PresencaAlunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "NotaFinal",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NotaAvaliacao2",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NotaAvaliacao1",
                table: "DisciplinasAlunos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "DisciplinasAlunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
