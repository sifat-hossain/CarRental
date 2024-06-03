using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Campoverde.QMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: new Guid("056ac989-5eb4-4f07-8630-069098584cfe"));

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: new Guid("29dc8766-06fc-45ee-9f20-1f342555bf6e"));

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: new Guid("bb4b94ca-5ad7-4a12-8857-e24c6ef04ef0"));

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VehicleSize",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeEnum",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleSize",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleTypeEnum",
                table: "Vehicle");

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "IsActive", "Model" },
                values: new object[,]
                {
                    { new Guid("056ac989-5eb4-4f07-8630-069098584cfe"), true, "Ford Ka" },
                    { new Guid("29dc8766-06fc-45ee-9f20-1f342555bf6e"), true, "Toyota Auris" },
                    { new Guid("bb4b94ca-5ad7-4a12-8857-e24c6ef04ef0"), true, "VW Polo" }
                });
        }
    }
}
