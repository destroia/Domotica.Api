using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class macaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MacAddress",
                table: "Dispositivos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_LugarRegionId",
                table: "Dispositivos",
                column: "LugarRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_LugarRegiones_LugarRegionId",
                table: "Dispositivos",
                column: "LugarRegionId",
                principalTable: "LugarRegiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_LugarRegiones_LugarRegionId",
                table: "Dispositivos");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivos_LugarRegionId",
                table: "Dispositivos");

            migrationBuilder.DropColumn(
                name: "MacAddress",
                table: "Dispositivos");
        }
    }
}
