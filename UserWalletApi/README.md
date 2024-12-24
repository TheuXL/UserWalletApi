---

# **UserWallet API**

Este projeto é uma API em **ASP.NET Core 9** que permite gerenciar usuários e suas carteiras. Ele implementa operações para cadastrar usuários e carteiras, bem como listar as carteiras de um usuário. O banco de dados usado é o **SQL Server**.

## **Índice**

- [Pré-requisitos](#pré-requisitos)
- [Configuração do Banco de Dados](#configuração-do-banco-de-dados)
- [Dependências](#dependências)
- [Configuração da API](#configuração-da-api)
- [Executando a API](#executando-a-api)
- [Testes Unitários](#testes-unitários)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Considerações Finais](#considerações-finais)

---

## **Pré-requisitos**

Para rodar este projeto, você precisará das seguintes ferramentas instaladas:

1. **.NET 9 SDK**: Para desenvolver e rodar o projeto.
   - [Baixe o .NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

2. **SQL Server**: O banco de dados utilizado é o SQL Server. Você pode usar uma instância local ou um SQL Server remoto.
   - [Baixe o SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou use uma instância existente.

3. **Visual Studio ou VS Code**: Uma IDE para desenvolver e rodar o projeto.
   - [Baixe o Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) ou instale o [VS Code](https://code.visualstudio.com/).

---

## **Configuração do Banco de Dados**

### Passo 1: Instalar o SQL Server
Caso ainda não tenha o SQL Server instalado, siga as instruções acima para instalar a versão mais recente do SQL Server Express ou use uma instância existente.

### Passo 2: Criar Banco de Dados

1. Abra o **SQL Server Management Studio (SSMS)** ou **Azure Data Studio**.
2. Conecte-se à sua instância do SQL Server.
3. Crie um banco de dados para a aplicação:

   ```sql
   CREATE DATABASE UserWalletDb;
   ```

### Passo 3: Configuração da String de Conexão

No arquivo `appsettings.json`, configure a string de conexão para o banco de dados. A string de conexão padrão é:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\MSSQLSERVER;Database=UserWalletDb;Trusted_Connection=True;Encrypt=False;"
  }
}
```

- Substitua `localhost\\MSSQLSERVER` com o nome correto da sua instância SQL Server, se necessário.

### Passo 4: Executando as Migrations

1. Abra o terminal ou o **Package Manager Console** no Visual Studio.
2. Execute o comando abaixo para aplicar as migrations e criar as tabelas no banco de dados:

   ```bash
   dotnet ef database update
   ```

Isso criará as tabelas `Users` e `Wallets` no banco de dados `UserWalletDb`.

---

## **Dependências**

O projeto utiliza as seguintes dependências para funcionar corretamente:

- **Microsoft.AspNetCore.Mvc**: Para criar a API RESTful.
- **Microsoft.EntityFrameworkCore.SqlServer**: Para a integração com o SQL Server.
- **Microsoft.EntityFrameworkCore.Design**: Para trabalhar com migrations.
- **Moq**: Para criar testes unitários simulando dependências.
- **xUnit**: Framework de testes unitários.
- **Microsoft.EntityFrameworkCore.InMemory**: Para testar sem precisar de banco de dados real.
- **Microsoft.AspNetCore.Mvc.Testing**: Para realizar testes de integração na API.

Execute os seguintes comandos no diretório do projeto para instalar todas as dependências necessárias:

```bash
dotnet restore
```

---

## **Configuração da API**

1. **UserController**:
   - Responsável por cadastrar um novo usuário.
   
   **Endpoint**:
   - `POST /api/users`: Cadastra um novo usuário.
   - **Body** (exemplo):
     ```json
     {
       "nome": "João Silva",
       "nascimento": "1990-01-01",
       "cpf": "12345678900"
     }
     ```

2. **WalletController**:
   - Responsável por cadastrar uma carteira e listar as carteiras de um usuário.

   **Endpoints**:
   - `POST /api/wallets`: Cadastra uma nova carteira.
   - `GET /api/wallets/{userId}`: Lista todas as carteiras de um usuário.
   
   **Body** (exemplo de criação de carteira):
   ```json
   {
     "userId": 1,
     "valorAtual": 1000.00,
     "banco": "Banco do Brasil",
     "ultimaAtualizacao": "2023-12-23T10:00:00"
   }
   ```

---

## **Executando a API**

Para rodar o projeto localmente:

1. Abra o terminal no diretório do projeto.
2. Execute o comando para rodar a API:

   ```bash
   dotnet run
   ```

A API estará disponível em `http://localhost:5000`.

---

## **Testes Unitários**

Para rodar os testes unitários, siga os passos abaixo:

1. Abra o terminal ou o **Package Manager Console**.
2. Navegue até o diretório onde os testes estão localizados.
3. Execute os testes com o comando:

   ```bash
   dotnet test
   ```

Isso executará todos os testes e mostrará os resultados no terminal.

---

## **Estrutura do Projeto**

A estrutura do projeto é a seguinte:

```
C:\UserWalletApi\UserWalletApi
├── Controllers/
│   ├── UserController.cs       # Controlador para gerenciar usuários
│   └── WalletController.cs     # Controlador para gerenciar carteiras
├── Models/
│   ├── User.cs                 # Modelo de dados do usuário
│   └── Wallet.cs               # Modelo de dados da carteira
├── Services/
│   ├── IUserService.cs         # Interface do serviço de usuário
│   ├── UserService.cs          # Implementação do serviço de usuário
│   ├── IWalletService.cs       # Interface do serviço de carteira
│   └── WalletService.cs        # Implementação do serviço de carteira
├── Repositories/
│   ├── IUserRepository.cs      # Interface do repositório de usuários
│   ├── UserRepository.cs       # Implementação do repositório de usuários
│   ├── IWalletRepository.cs    # Interface do repositório de carteiras
│   └── WalletRepository.cs     # Implementação do repositório de carteiras
├── Data/
│   ├── ApplicationDbContext.cs # Contexto de dados do Entity Framework
├── Migrations/
│   ├── <Arquivos da migration> # Arquivos de migração do Entity Framework
├── Program.cs                  # Configuração da aplicação ASP.NET Core
└── appsettings.json            # Configurações da aplicação, incluindo string de conexão
```

---

## **Considerações Finais**

- Este projeto é uma implementação básica de uma API RESTful usando ASP.NET Core com um banco de dados SQL Server.
- Ele não implementa autenticação ou outras funcionalidades avançadas, mas pode ser facilmente expandido para um ambiente de produção com a adição de autenticação, validação, e testes de segurança.
- A aplicação utiliza o **Entity Framework Core** para interagir com o banco de dados e a migração para criar as tabelas.

Se houver algum problema ou dúvida, entre em contato e ficaremos felizes em ajudar!

---

Esse `README.md` fornece informações claras e detalhadas sobre como configurar o banco de dados, instalar dependências, configurar a API, rodar o projeto e executar os testes.