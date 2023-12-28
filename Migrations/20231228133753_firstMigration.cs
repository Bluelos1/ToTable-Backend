using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToTable.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentItems",
                columns: table => new
                {
                    PayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PayCost = table.Column<int>(type: "integer", nullable: false),
                    PayMethod = table.Column<string>(type: "text", nullable: false),
                    PayStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItems", x => x.PayId);
                });

            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductDescription = table.Column<string>(type: "text", nullable: false),
                    ProductPrice = table.Column<float>(type: "real", nullable: false),
                    ProductStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItems", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "TableItems",
                columns: table => new
                {
                    TabId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TabNum = table.Column<int>(type: "integer", nullable: false),
                    TabStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableItems", x => x.TabId);
                });

            migrationBuilder.CreateTable(
                name: "WaiterItems",
                columns: table => new
                {
                    WaiterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WaiterName = table.Column<string>(type: "text", nullable: false),
                    WaiterSuma = table.Column<int>(type: "integer", nullable: false),
                    WaiterLogin = table.Column<string>(type: "text", nullable: false),
                    WaiterPassw = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiterItems", x => x.WaiterId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderTime = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    OrderComment = table.Column<string>(type: "text", nullable: false),
                    WaiterId = table.Column<int>(type: "integer", nullable: false),
                    PaymentPayId = table.Column<int>(type: "integer", nullable: false),
                    TableTabId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderItems_PaymentItems_PaymentPayId",
                        column: x => x.PaymentPayId,
                        principalTable: "PaymentItems",
                        principalColumn: "PayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_TableItems_TableTabId",
                        column: x => x.TableTabId,
                        principalTable: "TableItems",
                        principalColumn: "TabId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_WaiterItems_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "WaiterItems",
                        principalColumn: "WaiterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fruits",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemQuantity = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruits", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Fruits_OrderItems_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fruits_ProductItems_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductItems",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fruits_OrderId",
                table: "Fruits",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Fruits_ProductId",
                table: "Fruits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PaymentPayId",
                table: "OrderItems",
                column: "PaymentPayId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TableTabId",
                table: "OrderItems",
                column: "TableTabId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_WaiterId",
                table: "OrderItems",
                column: "WaiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fruits");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "PaymentItems");

            migrationBuilder.DropTable(
                name: "TableItems");

            migrationBuilder.DropTable(
                name: "WaiterItems");
        }
    }
}
