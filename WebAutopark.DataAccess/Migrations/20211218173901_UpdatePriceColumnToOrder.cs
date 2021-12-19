using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAutopark.DataAccess.Migrations
{
    public partial class UpdatePriceColumnToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "TotalPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "Price");
        }
    }
}
