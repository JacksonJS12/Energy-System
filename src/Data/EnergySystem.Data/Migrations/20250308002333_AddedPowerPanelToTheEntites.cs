using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPowerPanelToTheEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PowerPanelId",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PowerPanels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPanels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PowerPanelId",
                table: "Properties",
                column: "PowerPanelId",
                unique: true,
                filter: "[PowerPanelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PowerPanels_PowerPanelId",
                table: "Properties",
                column: "PowerPanelId",
                principalTable: "PowerPanels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PowerPanels_PowerPanelId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "PowerPanels");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PowerPanelId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PowerPanelId",
                table: "Properties");
        }
    }
}
