using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAutopark.DataAccess.Migrations
{
    public partial class AddedOrderUpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ProductModelProductId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductModelProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelId",
                table: "Product",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Orders_OrderId",
                table: "Product",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ProductModelId",
                table: "Product",
                column: "ProductModelId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Orders_OrderId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ProductModelId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductModelId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelProductId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelProductId",
                table: "Product",
                column: "ProductModelProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ProductModelProductId",
                table: "Product",
                column: "ProductModelProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
