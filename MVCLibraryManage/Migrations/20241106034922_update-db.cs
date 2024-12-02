using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLibraryManage.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BorrowingHistories",
                columns: table => new
                {
                    BorrowingHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryCardId = table.Column<int>(type: "int", nullable: false),
                    LibraryItemId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BorrowerLibraryCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingHistories", x => x.BorrowingHistoryId);
                    table.ForeignKey(
                        name: "FK_BorrowingHistories_Borrowers_BorrowerLibraryCardId",
                        column: x => x.BorrowerLibraryCardId,
                        principalTable: "Borrowers",
                        principalColumn: "LibraryCardId");
                    table.ForeignKey(
                        name: "FK_BorrowingHistories_LibraryItems_LibraryItemId",
                        column: x => x.LibraryItemId,
                        principalTable: "LibraryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingHistories_BorrowerLibraryCardId",
                table: "BorrowingHistories",
                column: "BorrowerLibraryCardId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingHistories_LibraryItemId",
                table: "BorrowingHistories",
                column: "LibraryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingHistories");
        }
    }
}
