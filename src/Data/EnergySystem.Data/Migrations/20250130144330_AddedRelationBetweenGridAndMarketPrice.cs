using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationBetweenGridAndMarketPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarketPriceId",
                table: "Grids",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grids_MarketPriceId",
                table: "Grids",
                column: "MarketPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grids_MarketPrices_MarketPriceId",
                table: "Grids",
                column: "MarketPriceId",
                principalTable: "MarketPrices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grids_MarketPrices_MarketPriceId",
                table: "Grids");

            migrationBuilder.DropIndex(
                name: "IX_Grids_MarketPriceId",
                table: "Grids");

            migrationBuilder.DropColumn(
                name: "MarketPriceId",
                table: "Grids");
        }
    }
}
