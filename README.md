Aqui está um exemplo de README detalhado para o seu projeto **UserWalletApi**. Ele inclui explicações de funcionalidades, estrutura do código e exemplos de testes.

---

# **UserWalletApi**

A **UserWalletApi** é uma aplicação ASP.NET Core que oferece funcionalidades de gerenciamento de usuários e carteiras. A API permite a criação de usuários e carteiras associadas a eles, armazenando essas informações em um banco de dados SQL Server. Esta API inclui testes automatizados para garantir que as operações de criação e consulta funcionem corretamente.

## **Estrutura do Projeto**

A estrutura do projeto é organizada em várias pastas para separar as responsabilidades de cada camada:

```
/UserWalletApi
  /Controllers
  /Models
  /Repositories
  /Services
  /Tests
  /Data
  /Program.cs
  /appsettings.json
  /UserWalletApi.csproj
```

### **Camadas do Projeto:**

1. **Controllers**: Responsáveis por definir os endpoints da API e interagir com os serviços.
2. **Models**: Contém as classes de dados representando as entidades.
3. **Repositories**: Responsáveis pela interação com o banco de dados.
4. **Services**: Contém a lógica de negócios, que interage com os repositórios.
5. **Tests**: Contém os testes unitários e de integração.
6. **Data**: Contém a configuração do banco de dados e o contexto de dados (DbContext).

---

## **Instalação**

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-repositorio/UserWalletApi.git
   ```

2. Navegue até a pasta do projeto:
   ```bash
   cd UserWalletApi
   ```

3. Instale as dependências:
   ```bash
   dotnet restore
   ```

4. Execute a aplicação:
   ```bash
   dotnet run
   ```

---

## **Configuração do Banco de Dados**

O banco de dados utilizado é o **SQL Server**, e a configuração da string de conexão está em `appsettings.json`. A conexão padrão usa o servidor local, mas pode ser alterada conforme necessário.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\MSSQLSERVER;Database=UserWalletDb;Trusted_Connection=True;Encrypt=False;"
}
```

A aplicação também pode ser configurada para usar um banco de dados em memória para testes, conforme configurado na classe `ApplicationDbContextFactory`.

---

## **EndPoints da API**

### **1. Criar Usuário**
**POST** `/api/users`

Este endpoint cria um novo usuário. Ele espera um objeto `User` no corpo da requisição.

#### Exemplo de Corpo da Requisição:
```json
{
  "Nome": "João Silva",
  "Nascimento": "1990-01-01",
  "CPF": "12345678900"
}
```

#### Exemplo de Código:
```csharp
[HttpPost]
public async Task<IActionResult> CreateUser([FromBody] User user)
{
    var createdUser = await _userService.CreateUserAsync(user);
    return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
}
```

Esse código cria um usuário, chama o serviço `CreateUserAsync` para salvar o usuário no banco de dados, e retorna um `CreatedAtActionResult` com o usuário recém-criado.

### **2. Criar Carteira**
**POST** `/api/wallets`

Este endpoint cria uma nova carteira associada a um usuário. O corpo da requisição deve conter um objeto `Wallet`.

#### Exemplo de Corpo da Requisição:
```json
{
  "UserID": 1,
  "ValorAtual": 1000.00,
  "Banco": "Banco do Brasil",
  "UltimaAtualizacao": "2023-12-26T12:00:00"
}
```

#### Exemplo de Código:
```csharp
[HttpPost]
public async Task<IActionResult> CreateWallet([FromBody] Wallet wallet)
{
    var createdWallet = await _walletService.CreateWalletAsync(wallet);
    return CreatedAtAction(nameof(GetWallet), new { id = createdWallet.Id }, createdWallet);
}
```

Esse código cria uma nova carteira, chama o serviço `CreateWalletAsync` e retorna um `CreatedAtActionResult`.

### **3. Obter Carteiras de um Usuário**
**GET** `/api/wallets/{userId}`

Este endpoint retorna todas as carteiras associadas a um usuário específico.

#### Exemplo de Código:
```csharp
[HttpGet("{userId}")]
public async Task<IActionResult> GetWallets(int userId)
{
    var wallets = await _walletService.GetWalletsByUserIdAsync(userId);
    if (wallets == null || wallets.Count == 0)
    {
        return NotFound();
    }
    return Ok(wallets);
}
```

---

## **Classes Principais**

### **User**
A classe `User` representa um usuário. Ela contém as propriedades `Nome`, `Nascimento` e `CPF`.

```csharp
public class User
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime Nascimento { get; set; }
    public string CPF { get; set; }
}
```

### **Wallet**
A classe `Wallet` representa uma carteira associada a um usuário. Ela contém as propriedades `UserID`, `ValorAtual`, `Banco`, e `UltimaAtualizacao`.

```csharp
public class Wallet
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public decimal ValorAtual { get; set; }
    public string Banco { get; set; }
    public DateTime UltimaAtualizacao { get; set; }
}
```

### **UserService**
A classe `UserService` contém a lógica de negócios para a manipulação de usuários, delegando as operações para o repositório `IUserRepository`.

```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
        return user;
    }
}
```

### **WalletService**
A classe `WalletService` contém a lógica de negócios para a manipulação de carteiras, delegando as operações para o repositório `IWalletRepository`.

```csharp
public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<Wallet> CreateWalletAsync(Wallet wallet)
    {
        await _walletRepository.AddWalletAsync(wallet);
        return wallet;
    }
}
```

---

## **Testes**

A aplicação inclui testes automatizados utilizando **xUnit** e **Moq** para simular interações com os repositórios. Abaixo estão alguns exemplos de testes.

### **Testando o Controlador de Usuário**
O teste abaixo valida a criação de um usuário, verificando se o retorno é um `CreatedAtActionResult` com os dados corretos.

```csharp
[Fact]
public async Task CreateUser_ReturnsCreatedAtActionResult_WhenValidUser()
{
    // Arrange
    var user = new User { Nome = "João Silva", Nascimento = new DateTime(1990, 1, 1), CPF = "12345678900" };
    _userServiceMock.Setup(service => service.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

    // Act
    var result = await _userController.CreateUser(user);

    // Assert
    var actionResult = Assert.IsType<CreatedAtActionResult>(result);
    var returnValue = Assert.IsType<User>(actionResult.Value);
    Assert.Equal(user.Nome, returnValue.Nome);
}
```

### **Testando o Controlador de Carteiras**
Este teste valida a criação de uma carteira e a obtenção de todas as carteiras associadas a um usuário.

```csharp
[Fact]
public async Task GetWallets_ReturnsOkResult_WhenWalletsExist()
{
    // Arrange
    var wallets = new List<Wallet>
    {
        new Wallet { UserID = 1, ValorAtual = 1000.00M, Banco = "Banco do Brasil", UltimaAtualizacao = DateTime.Now },
        new Wallet { UserID = 1, ValorAtual = 1500.00M, Banco = "Caixa Econômica", UltimaAtualizacao = DateTime.Now }
    };
    _walletServiceMock.Setup(service => service.GetWalletsByUserIdAsync(1)).ReturnsAsync(wallets);

    // Act
    var result = await _walletController.GetWallets(1);

    // Assert
    var actionResult = Assert.IsType<OkObjectResult>(result);
    var returnValue = Assert.IsType<List<Wallet>>(actionResult.Value);
    Assert.Equal(2, returnValue.Count);
}
```

---

## **Tecnologias Utilizadas**

- **ASP.NET Core 9.0**
- **Entity Framework Core 9.0**
- **SQL Server**
- **Moq** para mockar dependências nos testes
- **xUnit** para execução de testes unitários

---

## **Conclusão**

A **UserWalletApi** é uma aplicação robusta que fornece uma API RESTful para gerenciamento de usuários e carteiras. Ela utiliza uma arquitetura limpa com separação de responsabilidades entre camadas e implementa testes automatizados para garantir a qualidade do código.

