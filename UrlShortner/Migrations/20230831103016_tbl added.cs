using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortner.Migrations
{
    /// <inheritdoc />
    public partial class tbladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortenedUrl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    CreateAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrl", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrl_Code",
                table: "ShortenedUrl",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortenedUrl");
        }
    }
}
