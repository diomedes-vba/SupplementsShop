using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplementsShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemProductNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNumber",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNumber",
                table: "OrderItems");
        }
    }
}
