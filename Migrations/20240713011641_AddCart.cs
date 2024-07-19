using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemModel_CartModel_CartModelId",
                        column: x => x.CartModelId,
                        principalTable: "CartModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItemModel_TbLaptops_ItemId",
                        column: x => x.ItemId,
                        principalTable: "TbLaptops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CartItemModel_CartModelId",
                table: "CartItemModel",
                column: "CartModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemModel_ItemId",
                table: "CartItemModel",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TbCart_CartId1",
                table: "TbCart",
                column: "CartId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemModel");

            migrationBuilder.DropTable(
                name: "TbCart");

            migrationBuilder.DropTable(
                name: "CartModel");
        }
    }
}
