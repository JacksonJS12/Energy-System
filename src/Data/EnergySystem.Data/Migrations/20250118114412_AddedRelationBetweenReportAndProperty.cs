using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationBetweenReportAndProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PropertyId",
                table: "Reports",
                column: "PropertyId");

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
                name: "FK_Reports_Properties_PropertyId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PropertyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Reports");
        }
    }
}
