using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCartId3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItemModel",
                table: "CartItemModel");

            migrationBuilder.RenameTable(
                name: "CartItemModel",
                newName: "TbCartProducts");

            migrationBuilder.RenameIndex(
                name: "IX_CartItemModel_CartModelId",
                table: "TbCartProducts",
                newName: "IX_TbCartProducts_CartModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbCartProducts",
                table: "TbCartProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbCartProducts_TbCarts_CartModelId",
                table: "TbCartProducts",
                column: "CartModelId",
                principalTable: "TbCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbCartProducts_TbCarts_CartModelId",
                table: "TbCartProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbCartProducts",
                table: "TbCartProducts");

            migrationBuilder.RenameTable(
                name: "TbCartProducts",
                newName: "CartItemModel");

            migrationBuilder.RenameIndex(
                name: "IX_TbCartProducts_CartModelId",
                table: "CartItemModel",
                newName: "IX_CartItemModel_CartModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItemModel",
                table: "CartItemModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel",
                column: "CartModelId",
                principalTable: "TbCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
