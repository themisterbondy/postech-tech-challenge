CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "Carts" (
    "Id" uuid NOT NULL,
    "CustomerId" character varying(36) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "PaymentStatus" text NOT NULL,
    "TransactionId" character varying(36),
    CONSTRAINT "PK_Carts" PRIMARY KEY ("Id")
);

CREATE TABLE "Customers" (
    "Id" uuid NOT NULL DEFAULT (uuid_generate_v4()),
    "Name" character varying(255) NOT NULL,
    "Email" character varying(255) NOT NULL,
    "Cpf" character varying(11) NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Customers_Cpf" ON "Customers" ("Cpf");

CREATE UNIQUE INDEX "IX_Customers_Email" ON "Customers" ("Email");

CREATE TABLE "Orders" (
    "Id" uuid NOT NULL DEFAULT (uuid_generate_v4()),
    "CreatedAt" timestamp with time zone NOT NULL,
    "Status" text NOT NULL,
    "CustomerId" character varying(11),
    "TransactionId" character varying(36),
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id")
);

CREATE TABLE "Products" (
    "Id" uuid NOT NULL DEFAULT (uuid_generate_v4()),
    "Name" character varying(100) NOT NULL,
    "Description" character varying(500),
    "Price" numeric(18,2) NOT NULL,
    "Category" character varying(50) NOT NULL,
    "ImageUrl" character varying(800),
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
);

CREATE TABLE "CartItems" (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ProductName" character varying(255) NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "Quantity" integer NOT NULL,
    "CartId" uuid NOT NULL,
    "Category" text NOT NULL,
    CONSTRAINT "PK_CartItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CartItems_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CartItems_CartId" ON "CartItems" ("CartId");

CREATE TABLE "OrderItems" (
    "Id" uuid NOT NULL DEFAULT (uuid_generate_v4()),
    "OrderId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ProductName" character varying(100) NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "Quantity" integer NOT NULL,
    "Category" text NOT NULL,
    CONSTRAINT "PK_OrderItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");

INSERT INTO "Customers" ("Id", "Cpf", "Email", "Name")
VALUES ('404912ea-3558-4a6c-8318-3c433f0a4459', '36697999071', 'john.doe@email.com', 'John Doe');

INSERT INTO "Products" ("Id", "Category", "Description", "ImageUrl", "Name", "Price")
VALUES ('024fb6ba-5ebe-4131-a27e-d10a4041b32d', 'Sobremesa', 'A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.', 'https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br', 'Casquinha Chocolate', 1.49);
INSERT INTO "Products" ("Id", "Category", "Description", "ImageUrl", "Name", "Price")
VALUES ('6937a222-4e5e-4a75-abde-9ab3b9f58b0f', 'Acompanhamento', 'A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.', 'https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br', 'McFritas Média', 2.99);
INSERT INTO "Products" ("Id", "Category", "Description", "ImageUrl", "Name", "Price")
VALUES ('81e0a7f0-77e9-433f-9f2c-1b131c3317c3', 'Lanche', 'Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.', 'https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br', 'Big Mac', 5.99);
INSERT INTO "Products" ("Id", "Category", "Description", "ImageUrl", "Name", "Price")
VALUES ('84d18030-66cc-4f12-bf5f-988667805bf8', 'Bebida', 'Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.', 'https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br', 'Coca-Cola 300ml', 1.99);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241206000305_Initial', '8.0.7');

COMMIT;