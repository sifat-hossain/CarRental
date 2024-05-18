using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campoverde.QMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceColumnInVehicleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Vehicle",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vehicle");
        }
    }
}
