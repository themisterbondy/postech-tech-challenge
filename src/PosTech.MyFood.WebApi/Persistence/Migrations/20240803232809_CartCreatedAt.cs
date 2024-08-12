using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.MyFood.WebApi.Persistence.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class CartCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("bb9ff95d-53ea-4d18-8831-ff6672247f33"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3eb30302-fcef-410e-902d-b3784ca9b9d4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("41271808-b044-4880-8d01-8c8e1cbfd524"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ddb4a9a5-66bd-495c-ba95-e19955203500"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("decf08dd-57df-4a2a-abca-c812ed9d0df2"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Carts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("19efb1c9-922b-48a3-a77a-0048bfdfe0fb"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("130663f3-295b-426c-b9cd-f0f308715c4a"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m },
                    { new Guid("7d1e2cbb-63b4-4c0a-85e4-7b342636d403"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("d2a1092e-f204-4e5a-b70c-57b021cece85"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m },
                    { new Guid("f97de478-3e94-4ea0-b425-17a71c27c628"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("19efb1c9-922b-48a3-a77a-0048bfdfe0fb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("130663f3-295b-426c-b9cd-f0f308715c4a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d1e2cbb-63b4-4c0a-85e4-7b342636d403"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d2a1092e-f204-4e5a-b70c-57b021cece85"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f97de478-3e94-4ea0-b425-17a71c27c628"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CPF", "Email", "Name" },
                values: new object[] { new Guid("bb9ff95d-53ea-4d18-8831-ff6672247f33"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3eb30302-fcef-410e-902d-b3784ca9b9d4"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m },
                    { new Guid("41271808-b044-4880-8d01-8c8e1cbfd524"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m },
                    { new Guid("ddb4a9a5-66bd-495c-ba95-e19955203500"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m },
                    { new Guid("decf08dd-57df-4a2a-abca-c812ed9d0df2"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m }
                });
        }
    }
}