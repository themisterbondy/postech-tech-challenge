using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.MyFood.WebApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("514de985-d317-4bcf-b660-acbe9530ee46"));

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("42878b98-bacc-4d95-b3f5-bcd9829c5cdf"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("7d6a1a43-bc38-40b1-8785-fbd2ee1ac376"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m },
                    { new Guid("99ffb616-5103-48e9-947c-0c67cf0de18b"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m },
                    { new Guid("e3de7071-d9a6-476b-ac43-880283072683"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("f4991993-dab5-4066-81fa-a9052f16dfaa"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("42878b98-bacc-4d95-b3f5-bcd9829c5cdf"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("514de985-d317-4bcf-b660-acbe9530ee46"), "36697999071", "john.doe@email.com", "John Doe" });
        }
    }
}
