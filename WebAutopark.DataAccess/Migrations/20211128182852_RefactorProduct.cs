using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAutopark.DataAccess.Migrations
{
    public partial class RefactorProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ProductModelId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductModelId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelId",
                table: "Product",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ProductModelId",
                table: "Product",
                column: "ProductModelId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
