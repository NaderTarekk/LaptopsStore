using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel");

            migrationBuilder.AlterColumn<int>(
                name: "CartModelId",
                table: "CartItemModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel",
                column: "CartModelId",
                principalTable: "TbCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel");

            migrationBuilder.AlterColumn<int>(
                name: "CartModelId",
                table: "CartItemModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemModel_TbCarts_CartModelId",
                table: "CartItemModel",
                column: "CartModelId",
                principalTable: "TbCarts",
                principalColumn: "Id");
        }
    }
}
