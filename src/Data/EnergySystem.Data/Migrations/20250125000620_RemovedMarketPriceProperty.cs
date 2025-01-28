using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedMarketPriceProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "MarketPrices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "MarketPrices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
