using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campoverde.QMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { new Guid("722901c9-31f4-486a-ae1c-058d6da261da"), true, "User" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f220d2ea-fec4-4d80-8a86-ff1ba10d3acd"),
                column: "Password",
                value: "Tta68KjQB9cHtfsDxpQC0YigXpuJ/1IXk3z+LIejRSl/2vmIRoQp38wBs/U5E/Z4");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "IsActive", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("e795266a-2e1f-4943-b878-21ae1bb5ebd4"), "user", true, "uTJndqPRWOmM2JsDYQ6ORQCpVkiozdlDFcWV06VV0Wz7guIzG4S3Wa4gJGe7Yo9x", "0123456789", new Guid("722901c9-31f4-486a-ae1c-058d6da261da") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e795266a-2e1f-4943-b878-21ae1bb5ebd4"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("722901c9-31f4-486a-ae1c-058d6da261da"));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f220d2ea-fec4-4d80-8a86-ff1ba10d3acd"),
                column: "Password",
                value: "ANznY7pOx7UkYvETjpkmbaKbhCxwZyUJhkpDg8QRajC/yruTQ1edEYQkHxfhdUFh");
        }
    }
}
