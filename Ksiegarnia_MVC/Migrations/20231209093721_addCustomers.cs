using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ksiegarnia_MVC.Migrations
{
    /// <inheritdoc />
    public partial class addCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Book",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CustomerId",
                table: "Book",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Book_CustomerId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Book");
        }
    }
}
