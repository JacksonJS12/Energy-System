using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesToReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatteryId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GridId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_BatteryId",
                table: "Reports",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GridId",
                table: "Reports",
                column: "GridId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PropertyId",
                table: "Reports",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Batteries_BatteryId",
                table: "Reports",
                column: "BatteryId",
                principalTable: "Batteries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Grids_GridId",
                table: "Reports",
                column: "GridId",
                principalTable: "Grids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Properties_PropertyId",
                table: "Reports",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Batteries_BatteryId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Grids_GridId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Properties_PropertyId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_BatteryId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_GridId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PropertyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BatteryId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "GridId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
