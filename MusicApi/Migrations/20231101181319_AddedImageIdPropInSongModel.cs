using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageIdPropInSongModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("2924b038-5a87-4b68-93e4-b0b17536d2ce"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("c246f095-b1d8-4791-b323-60fb5a1164dc"));

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Duration", "ImageId", "Language", "Title" },
                values: new object[,]
                {
                    { new Guid("18ef94b6-5cb9-45ad-95d7-64184988a42f"), "4:36", "test", "English", "Willow" },
                    { new Guid("cab40247-4371-4f8b-9bc3-57ae261d0924"), "3:20", "test", "English", "In The End" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("18ef94b6-5cb9-45ad-95d7-64184988a42f"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("cab40247-4371-4f8b-9bc3-57ae261d0924"));

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Songs");

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Duration", "Language", "Title" },
                values: new object[,]
                {
                    { new Guid("2924b038-5a87-4b68-93e4-b0b17536d2ce"), "4:36", "English", "Willow" },
                    { new Guid("c246f095-b1d8-4791-b323-60fb5a1164dc"), "3:20", "English", "In The End" }
                });
        }
    }
}
