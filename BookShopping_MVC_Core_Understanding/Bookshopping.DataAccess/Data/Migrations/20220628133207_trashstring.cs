using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShopping_MVC_Core_Understanding.Data.Migrations
{
    public partial class trashstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrasactionId",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<int>(
                name: "TrasactionId",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
