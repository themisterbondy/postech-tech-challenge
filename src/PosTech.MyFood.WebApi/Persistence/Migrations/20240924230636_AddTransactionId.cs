using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.MyFood.WebApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Customers",
                newName: "Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CPF",
                table: "Customers",
                newName: "IX_Customers_Cpf");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Orders",
                type: "character varying(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Carts",
                type: "character varying(36)",
                maxLength: 36,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Customers",
                newName: "CPF");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Cpf",
                table: "Customers",
                newName: "IX_Customers_CPF");

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
    }
}
