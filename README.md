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