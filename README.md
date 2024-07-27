# MyFood

## Descrição

O MyFood é um sistema de pedidos de comida desenvolvido utilizando ASP.NET Core Minimal API com uma arquitetura hexagonal. Ele permite que os clientes montem combos personalizados e acompanhem o status de seus pedidos. O sistema também oferece funcionalidades administrativas para gerenciar produtos, categorias e campanhas promocionais.

## Sobre o Projeto

Este projeto faz parte do Tech Challenge da Pós Tech da FIAP do curso de Software Architecture. Trata-se de uma atividade obrigatória que deve ser desenvolvida em grupo e vale 90% da nota de todas as disciplinas da fase. 

## Funcionalidades

### Cliente

- **Identificação e Cadastro**:
    - Identificação via CPF
    - Cadastro com nome e e-mail
    - Opção de não se identificar

- **Montagem de Combo**:
    - Lanche
    - Acompanhamento
    - Bebida
    - Sobremesa
    - Exibição de nome, descrição e preço de cada produto

- **Acompanhamento**:
    - Monitoramento do progresso do pedido: Recebido, Em preparação, Pronto, Finalizado

### Administrativo

- **Gerenciamento de Clientes**:
    - Cadastro e manutenção para campanhas promocionais

- **Gerenciamento de Produtos e Categorias**:
    - Lanche, Acompanhamento, Bebida, Sobremesa

- **Acompanhamento de Pedidos**:
    - Monitoramento de pedidos em andamento e tempo de espera

## APIs

- **Cadastro do Cliente**
- **Identificação do Cliente via CPF**
- **Criar, Editar e Remover Produtos**
- **Buscar Produtos por Categoria**
- **Fake Checkout**
- **Listar Pedidos**

## Tecnologias Utilizadas

- **ASP.NET Core Minimal API**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker e Docker Compose**
- **Swagger**
- **OpenTelemetry**
- **Serilog**
- **MediatR**
- **FluentValidation**

## Estrutura do Projeto

- **Arquitetura Hexagonal**
- **Feature Folder**
- **Documentação com Event Storming**

## Documentação

A documentação do sistema foi desenvolvida seguindo os padrões de Domain-Driven Design (DDD) com Event Storming, cobrindo os fluxos de realização do pedido e pagamento, preparação e entrega do pedido.

Os desenhos e diagramas do Event Storming podem ser encontrados [aqui](https://miro.com/app/board/uXjVK06l1is=/).

## Instruções para Configuração

### Requisitos

- Docker
- Docker Compose

### Passos para Configuração

1. Clone o repositório:
    ```sh
    git clone https://github.com/themisterbondy/postech-tech-challenge.git
    cd postech-tech-challenge
    ```

2. Configure o arquivo `docker-compose.yml` e o `Dockerfile` conforme necessário.

3. Configure o certificado HTTPS executando os seguintes comandos:
    ```sh
    dotnet dev-certs https --clean
    dotnet dev-certs https -ep $env:userprofile\.aspnet\https\aspnetapp.pfx -p password123
    dotnet dev-certs https --trust
    ```

4. Suba os containers:
    ```sh
    docker-compose up --build
    ```

5. Acesse o Swagger para explorar as APIs:
    ```
    http://localhost:8080/swagger
    ```

### Infraestrutura

- 1 instância para banco de dados
- 1 instância para executar aplicação
## Migrações e Dados Pré-Incluídos

O sistema utiliza migrações do Entity Framework Core para gerenciar o esquema do banco de dados. As migrações são aplicadas automaticamente durante a inicialização da aplicação.

### Clientes Pré-Incluídos

- **John Doe**
    - ID: c058b864-17f1-4798-8a0a-68e92e00cfe5
    - CPF: 36697999071
    - Email: john.doe@email.com

### Produtos Pré-Incluídos

- **McFritas Média**
    - ID: 492744af-c4df-4393-9eb2-ec7b82ee835b
    - Categoria: Acompanhamento
    - Descrição: A batata frita mais famosa do mundo. Deliciosas batatas selecionadas, fritas, crocantes por fora, macias por dentro, douradas, irresistíveis, saborosas, famosas, e todos os outros adjetivos positivos que você quiser dar.
    - Preço: $2.99
    - Imagem: ![McFritas Média](https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kUXGZHtB/200/200/original?country=br)

- **Casquinha Chocolate**
    - ID: b75b1275-7661-47c7-9a9d-5409c2defae7
    - Categoria: Sobremesa
    - Descrição: A sobremesa que o Brasil todo adora. Uma casquinha supercrocante, com bebida láctea sabor chocolate que vai bem a qualquer hora.
    - Preço: $1.49
    - Imagem: ![Casquinha Chocolate](https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kpXyfJ7k/200/200/original?country=br)

- **Big Mac**
    - ID: b7d7d112-a680-48aa-bb79-6a5d320931d0
    - Categoria: Lanche
    - Descrição: Dois hambúrgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim.
    - Preço: $5.99
    - Imagem: ![Big Mac](https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kzXCTbnv/200/200/original?country=br)

- **Coca-Cola 300ml**
    - ID: f8afe4e1-8a4d-490e-981f-f24367ec34aa
    - Categoria: Bebida
    - Descrição: Refrescante e geladinha. Uma bebida assim refresca a vida. Você pode escolher entre Coca-Cola, Coca-Cola Zero, Sprite sem Açúcar, Fanta Guaraná e Fanta Laranja.
    - Preço: $1.99
    - Imagem: ![Coca-Cola 300ml](https://cache-backend-mcd.mcdonaldscupones.com/media/image/product$kNXZJR6V/200/200/original?country=br)