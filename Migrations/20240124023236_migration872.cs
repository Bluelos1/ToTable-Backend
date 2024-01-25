using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToTable.Migrations
{
    /// <inheritdoc />
    public partial class migration872 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantObject",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantName = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    TableQuantity = table.Column<string>(type: "text", nullable: false),
                    WaiterQantity = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantObject", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "ProductObject",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductDescription = table.Column<string>(type: "text", nullable: false),
                    ProductPrice = table.Column<double>(type: "double precision", nullable: false),
                    ProductStatus = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ProductCategory = table.Column<string>(type: "text", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductObject", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductObject_RestaurantObject_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObject",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableObject",
                columns: table => new
                {
                    TabId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TabNum = table.Column<int>(type: "integer", nullable: false),
                    TabStatus = table.Column<bool>(type: "boolean", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableObject", x => x.TabId);
                    table.ForeignKey(
                        name: "FK_TableObject_RestaurantObject_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObject",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaiterObject",
                columns: table => new
                {
                    WaiterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WaiterName = table.Column<string>(type: "text", nullable: false),
                    WaiterSurname = table.Column<string>(type: "text", nullable: false),
                    WaiterLogin = table.Column<string>(type: "text", nullable: false),
                    WaiterPassw = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiterObject", x => x.WaiterId);
                    table.ForeignKey(
                        name: "FK_WaiterObject_RestaurantObject_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObject",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderObject",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    OrderComment = table.Column<string>(type: "text", nullable: true),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    WaiterId = table.Column<int>(type: "integer", nullable: true),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderObject", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderObject_RestaurantObject_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObject",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderObject_TableObject_TableId",
                        column: x => x.TableId,
                        principalTable: "TableObject",
                        principalColumn: "TabId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderObject_WaiterObject_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "WaiterObject",
                        principalColumn: "WaiterId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemObject",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemQuantity = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemObject", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_OrderItemObject_OrderObject_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderObject",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderItemObject_ProductObject_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductObject",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemObject_OrderId",
                table: "OrderItemObject",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemObject_ProductId",
                table: "OrderItemObject",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderObject_RestaurantId",
                table: "OrderObject",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderObject_TableId",
                table: "OrderObject",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderObject_WaiterId",
                table: "OrderObject",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductObject_RestaurantId",
                table: "ProductObject",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_TableObject_RestaurantId",
                table: "TableObject",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiterObject_RestaurantId",
                table: "WaiterObject",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemObject");

            migrationBuilder.DropTable(
                name: "OrderObject");

            migrationBuilder.DropTable(
                name: "ProductObject");

            migrationBuilder.DropTable(
                name: "TableObject");

            migrationBuilder.DropTable(
                name: "WaiterObject");

            migrationBuilder.DropTable(
                name: "RestaurantObject");
        }
    }
}
