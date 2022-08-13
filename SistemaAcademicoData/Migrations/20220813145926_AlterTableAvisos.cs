using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class AlterTableAvisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Avisos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SemestresVigentes",
                columns: table => new
                {
                    SemestreVigenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    Vigente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemestresVigentes", x => x.SemestreVigenteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_ApplicationUserId",
                table: "Avisos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_AspNetUsers_ApplicationUserId",
                table: "Avisos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_AspNetUsers_ApplicationUserId",
                table: "Avisos");

            migrationBuilder.DropTable(
                name: "SemestresVigentes");

            migrationBuilder.DropIndex(
                name: "IX_Avisos_ApplicationUserId",
                table: "Avisos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Avisos");
        }
    }
}
