using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLibraryManage.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingHistories_Borrowers_BorrowerLibraryCardId",
                table: "BorrowingHistories");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerLibraryCardId",
                table: "BorrowingHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "Borrowers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isStaff = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staffs_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrowers_AccountID",
                table: "Borrowers",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_AccountID",
                table: "Staffs",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowers_Accounts_AccountID",
                table: "Borrowers",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingHistories_Borrowers_BorrowerLibraryCardId",
                table: "BorrowingHistories",
                column: "BorrowerLibraryCardId",
                principalTable: "Borrowers",
                principalColumn: "LibraryCardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowers_Accounts_AccountID",
                table: "Borrowers");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingHistories_Borrowers_BorrowerLibraryCardId",
                table: "BorrowingHistories");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Borrowers_AccountID",
                table: "Borrowers");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Borrowers");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerLibraryCardId",
                table: "BorrowingHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingHistories_Borrowers_BorrowerLibraryCardId",
                table: "BorrowingHistories",
                column: "BorrowerLibraryCardId",
                principalTable: "Borrowers",
                principalColumn: "LibraryCardId");
        }
    }
}
