using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.MyFood.WebApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingTheCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("c058b864-17f1-4798-8a0a-68e92e00cfe5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("492744af-c4df-4393-9eb2-ec7b82ee835b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b75b1275-7661-47c7-9a9d-5409c2defae7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b7d7d112-a680-48aa-bb79-6a5d320931d0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f8afe4e1-8a4d-490e-981f-f24367ec34aa"));

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerCpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("af303eba-906b-49f7-8b9e-45c1bcbf58ba"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("2207e965-58e6-48ab-be58-0071a2904467"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m },
                    { new Guid("27a6459b-06f3-48cd-9242-4f0ce8fd1d23"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("d621ce20-f819-4c61-a6fe-bb97e06d626f"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m },
                    { new Guid("fe62b2d3-5f82-4a8e-bd5e-2e875c3d8742"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("af303eba-906b-49f7-8b9e-45c1bcbf58ba"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2207e965-58e6-48ab-be58-0071a2904467"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("27a6459b-06f3-48cd-9242-4f0ce8fd1d23"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d621ce20-f819-4c61-a6fe-bb97e06d626f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe62b2d3-5f82-4a8e-bd5e-2e875c3d8742"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("c058b864-17f1-4798-8a0a-68e92e00cfe5"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("492744af-c4df-4393-9eb2-ec7b82ee835b"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m },
                    { new Guid("b75b1275-7661-47c7-9a9d-5409c2defae7"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("b7d7d112-a680-48aa-bb79-6a5d320931d0"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m },
                    { new Guid("f8afe4e1-8a4d-490e-981f-f24367ec34aa"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m }
                });
        }
    }
}
