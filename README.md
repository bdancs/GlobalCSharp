# 🔐 SecurityState API

### 👥 Membros do Grupo
- Arthur Candido de Abreu - RM 98283
- Bianca Carvalho Dancs Firsoff - RM 551645
- Giuliano Romaneto Marques - RM 99694

------------------------------------------------------------------------

## 🚀 Sobre o Projeto

A **SecurityState API** é um sistema desenvolvido em **.NET 8**,
utilizando **API Versioning**, **Entity Framework Core 8**, **Swagger
(Swashbuckle)** e banco de dados **SQL Server**.\
Ela fornece endpoints para gerenciamento de entidades relacionadas à
área de segurança, como:
-   **Incidentes**
-   **Agentes**
-   **Relatórios**
------------------------------------------------------------------------

## 📦 Tecnologias Utilizadas
-   **.NET 8**
-   **ASP.NET Core Web API**
-   **Entity Framework Core 8**
-   **API Versioning 5.1.0**
-   **Swagger / Swashbuckle 10.0.1**
-   **SQL Server**

------------------------------------------------------------------------

## ⚙️ Pré-requisitos
Certifique-se de ter instalado:
-   **.NET SDK 8.0**
-   **SQL Server**
-   **SQL Server Management Studio (SSMS)** *(opcional)*

------------------------------------------------------------------------

## 🗃️ Banco de Dados (Migrations com EF Core)
### Criar as migrações

    dotnet ef migrations add InitialCreate

### Aplicar as migrações

    dotnet ef database update

------------------------------------------------------------------------

## ▶️ Rodando o Projeto

    dotnet run

A API iniciará em algo como:

    http://localhost:5000

------------------------------------------------------------------------

## 📘 Swagger

Acesse:

    http://localhost:5000/swagger

------------------------------------------------------------------------

## 📚 Estrutura do Projeto
-   **Controllers** -- Endpoints (versões)
-   **Data** -- DbContext e EF Core
-   **Entities** -- Modelos do domínio
-   **DTOs** -- Objetos de transferência
-   **Swagger** -- Configuração Swashbuckle
-   **Program.cs** -- Pipeline e serviços

------------------------------------------------------------------------

## 🛠️ Funcionalidades Principais
-   Cadastro e consulta de **Incidentes**
-   Gerenciamento de **Agentes**
-   Versionamento de API
-   Documentação com Swagger
-   Persistência com EF Core

------------------------------------------------------------------------