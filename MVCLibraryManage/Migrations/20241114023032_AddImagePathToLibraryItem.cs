using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLibraryManage.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToLibraryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "LibraryItems",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "LibraryItems");
        }
    }
}
