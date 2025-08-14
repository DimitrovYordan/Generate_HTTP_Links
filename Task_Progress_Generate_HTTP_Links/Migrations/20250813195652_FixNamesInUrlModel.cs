using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Progress_Generate_HTTP_Links.Migrations
{
    /// <inheritdoc />
    public partial class FixNamesInUrlModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretURL",
                table: "Urls",
                newName: "SecretUrl");

            migrationBuilder.RenameColumn(
                name: "ShortlUrl",
                table: "Urls",
                newName: "ShortUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretUrl",
                table: "Urls",
                newName: "SecretURL");

            migrationBuilder.RenameColumn(
                name: "ShortUrl",
                table: "Urls",
                newName: "ShortlUrl");
        }
    }
}
