using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class NewCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemModel_CartModel_CartModelId",
                table: "CartItemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItemModel_TbLaptops_ItemId",
                table: "CartItemModel");

            migrationBuilder.DropTable(
                name: "TbCart");

            migrationBuilder.DropIndex(
                name: "IX_CartItemModel_ItemId",
                table: "CartItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel");

            migrationBuilder.RenameTable(
                name: "CartModel",
                newName: "TbCarts");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "CartItemModel",
                newName: "CartId");

            migrationBuilder.AlterColumn<int>(
                name: "CartModelId",
                table: "CartItemModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartItemModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartItemModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TbCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbCarts",
                table: "TbCarts",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbCarts",
                table: "TbCarts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItemModel");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartItemModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbCarts");

            migrationBuilder.RenameTable(
                name: "TbCarts",
                newName: "CartModel");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItemModel",
                newName: "ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "CartModelId",
                table: "CartItemModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TbCart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId1 = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_TbCart_CartModel_CartId1",
                        column: x => x.CartId1,
                        principalTable: "CartModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemModel_ItemId",
                table: "CartItemModel",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TbCart_CartId1",
                table: "TbCart",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemModel_CartModel_CartModelId",
                table: "CartItemModel",
                column: "CartModelId",
                principalTable: "CartModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemModel_TbLaptops_ItemId",
                table: "CartItemModel",
                column: "ItemId",
                principalTable: "TbLaptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
