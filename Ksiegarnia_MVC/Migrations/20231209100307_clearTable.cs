using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ksiegarnia_MVC.Migrations
{
    /// <inheritdoc />
    public partial class clearTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Book");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
