# API Financeira

Projeto backend em .NET 10 que expõe endpoints para ganhos, gastos, reservas e resumo.

Pré-requisitos

- .NET 10 SDK

Como rodar

```powershell
cd "c:\Users\lenvy\source\repos\api financeiro"
dotnet build
dotnet run
```

Banco de dados

O projeto inclui Migrations do EF Core. Para aplicar migrações localmente:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef database update
```

Contribuição

- Verifique se não está incluindo arquivos sensíveis (veja `.gitignore`).

Licença

- Coloque aqui a informação de licença, se desejar.
