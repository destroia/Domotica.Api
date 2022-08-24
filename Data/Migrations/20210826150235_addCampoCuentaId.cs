using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addCampoCuentaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Cuentas_CuentaId",
                table: "Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lugares_Cuentas_CuentaId",
                table: "Lugares");

            migrationBuilder.DropIndex(
                name: "IX_Lugares_CuentaId",
                table: "Lugares");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivos_CuentaId",
                table: "Dispositivos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMact",
                table: "Dispositivos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CuentaId",
                table: "Cuentas",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DispositivosQuery",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CuentaId = table.Column<Guid>(nullable: false),
                    LugarRegionId = table.Column<Guid>(nullable: false),
                    MacAddress = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaMact = table.Column<DateTime>(nullable: false),
                    TotalRecords = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DispositivosQuery");

            migrationBuilder.DropColumn(
                name: "FechaMact",
                table: "Dispositivos");

            migrationBuilder.DropColumn(
                name: "CuentaId",
                table: "Cuentas");

            migrationBuilder.CreateIndex(
                name: "IX_Lugares_CuentaId",
                table: "Lugares",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_CuentaId",
                table: "Dispositivos",
                column: "CuentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Cuentas_CuentaId",
                table: "Dispositivos",
                column: "CuentaId",
                principalTable: "Cuentas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lugares_Cuentas_CuentaId",
                table: "Lugares",
                column: "CuentaId",
                principalTable: "Cuentas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
