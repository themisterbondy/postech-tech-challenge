using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.MyFood.WebApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatusInCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("45c39840-fcaf-4446-8b1c-7d27874ae6fb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1073ffe-8887-4e78-b89b-f67b2567dd51"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dab397cb-7e9e-4e9d-9602-432156c0ca62"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("efc50969-6416-49de-bb66-df893d87adc1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe326405-85e4-431f-8ffa-e5b7d61c6ddd"));

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Cpf", "Email", "Name" },
                values: new object[] { new Guid("a9bddd75-5d5b-47e6-8044-dbb22ae437f4"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("738dd22a-79db-48e6-a291-d5b8d3b32da6"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m },
                    { new Guid("78d7e7e6-afd8-49bb-88a6-076febf8cf5c"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("a21709b2-ab04-4ec8-933c-44bcf5312aca"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m },
                    { new Guid("dc843643-5fac-427e-8d2e-3681dada07fe"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a9bddd75-5d5b-47e6-8044-dbb22ae437f4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("738dd22a-79db-48e6-a291-d5b8d3b32da6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("78d7e7e6-afd8-49bb-88a6-076febf8cf5c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a21709b2-ab04-4ec8-933c-44bcf5312aca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dc843643-5fac-427e-8d2e-3681dada07fe"));

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Cpf", "Email", "Name" },
                values: new object[] { new Guid("45c39840-fcaf-4446-8b1c-7d27874ae6fb"), "36697999071", "john.doe@email.com", "John Doe" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("c1073ffe-8887-4e78-b89b-f67b2567dd51"), "Sobremesa", "A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br", "Casquinha Chocolate", 1.49m },
                    { new Guid("dab397cb-7e9e-4e9d-9602-432156c0ca62"), "Acompanhamento", "A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br", "McFritas Média", 2.99m },
                    { new Guid("efc50969-6416-49de-bb66-df893d87adc1"), "Bebida", "Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br", "Coca-Cola 300ml", 1.99m },
                    { new Guid("fe326405-85e4-431f-8ffa-e5b7d61c6ddd"), "Lanche", "Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.", "https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br", "Big Mac", 5.99m }
                });
        }
    }
}
