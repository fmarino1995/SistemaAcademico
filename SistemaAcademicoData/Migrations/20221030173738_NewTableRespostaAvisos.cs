using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademicoData.Migrations
{
    public partial class NewTableRespostaAvisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RespostaAvisos",
                columns: table => new
                {
                    RespostaAvisoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraResposta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvisoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCriacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaAvisos", x => x.RespostaAvisoId);
                    table.ForeignKey(
                        name: "FK_RespostaAvisos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostaAvisos_Avisos_AvisoId",
                        column: x => x.AvisoId,
                        principalTable: "Avisos",
                        principalColumn: "AvisoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvisos_ApplicationUserId",
                table: "RespostaAvisos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAvisos_AvisoId",
                table: "RespostaAvisos",
                column: "AvisoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespostaAvisos");
        }
    }
}
