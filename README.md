# RemainingMediator

**RemainingMediator** é uma biblioteca leve e extensível para .NET que implementa o padrão de projeto Mediator, promovendo o baixo acoplamento e a separação de responsabilidades em aplicações modernas.

<img src="https://raw.githubusercontent.com/PauloZier/RemainingMediator/refs/heads/main/nuget.png" alt="NuGet" width="128"/> 

## ✨ Recursos

- Suporte a comandos, consultas e notificações.
- Integração fácil com injeção de dependência.
- Projetado para aplicações ASP.NET Core, APIs e microsserviços.
- Compatível com netstandard2.0, .NET 6 ou superior.
- Licenciado sob BSD-3-Clause.

## 📦 Instalação

Você pode instalar o pacote via NuGet:

```bash
dotnet add package RemainingMediator
```

## 🚀 Como Usar

### 1. Defina uma requisição (comando ou consulta)

```csharp
public class CriarPedidoCommand : IRequest<Guid>
{
    public string Produto { get; set; }
    public int Quantidade { get; set; }
}
```

### 2. Implemente o manipulador

```csharp
public class CriarPedidoHandler : IRequestHandler<CriarPedidoCommand, Guid>
{
    public Task<Guid> HandleAsync(CriarPedidoCommand request, CancellationToken cancellationToken)
    {
        // Lógica para criar o pedido
        // ...

        var novoPedidoId = Guid.NewGuid();
        return Task.FromResult(novoPedidoId);
    }
}
```

### 3. Registre os serviços no Program.cs

```csharp
services.AddRemainingMediator(assemblies: typeof(CriarPedidoHandler).Assembly);
```

### 4. Utilize o mediator em seu código

```csharp
var pedidoId = await mediator.SendAsync(new CriarPedidoCommand
{
    Produto = "Notebook",
    Quantidade = 1
});
```

## 📄 Licença
Este projeto está licenciado sob a Licença BSD-3-Clause.

## 🤝 Contribuindo
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests.