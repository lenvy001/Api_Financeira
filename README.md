# API Financeira 💰

Uma API RESTful para controle de ganhos, gastos e reservas financeiras pessoais. Construída com ASP.NET Core 10, Entity Framework Core e SQLite.

---

## 📋 Descrição

Esta API fornece endpoints para gerenciar finanças pessoais, permitindo:

- ✅ Registrar e gerenciar ganhos
- ✅ Registrar e gerenciar gastos
- ✅ Registrar e gerenciar reservas
- ✅ Consultar histórico por mês e ano
- ✅ Gerar resumo financeiro com saldo mensal

---

## 🚀 Tecnologias

| Tecnologia | Versão | Descrição |
|---|---|---|
| **.NET SDK** | 10.0 | Framework .NET |
| **ASP.NET Core** | 10.0 | Framework web |
| **Entity Framework Core** | 9.0.0 | ORM |
| **SQLite** | 9.0.0 | Banco de dados local |
| **Scalar** | 2.14.11 | Interface de documentação da API |
| **Newtonsoft.Json** | 13.0.3 | Serialização JSON |

---

## 📦 Requisitos

- **.NET 10 SDK** ou superior — [Download](https://dotnet.microsoft.com/download)
- **Visual Studio 2022** ou **VS Code**
- **Git** para controle de versão

---

## 🔧 Instalação

### 1. Clonar o repositório

```bash
git clone https://github.com/lenvy001/Api_Financeira.git
cd Api_Financeira
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Executar migrações do banco de dados

```bash
dotnet ef database update
```

### 4. Iniciar a aplicação

```bash
dotnet run
```

⚠️ Ambiente Local

Esta API roda localmente. Não há deploy disponível no momento.
A porta é definida automaticamente pelo .NET — verifique no terminal
ao rodar `dotnet run` ou no arquivo `Properties/launchSettings.json`.
---

## 📚 Documentação da API

Após iniciar a aplicação, acesse a interface interativa em:

```
https://localhost:[porta]/scalar/v1
```

---

## 🏗️ Estrutura do Projeto

```
Api_Financeira/
├── Controllers/
│   ├── GanhoController.cs
│   ├── GastoController.cs
│   ├── ReservaController.cs
│   └── ResumoController.cs
├── Models/
│   ├── Ganho.cs
│   ├── Gasto.cs
│   ├── Reserva.cs
│   └── Resumo.cs
├── Data/
│   └── AppDbContext.cs
├── Helpers/
│   └── DateOnlyJsonConverter.cs
├── Migrations/
├── Properties/
│   └── launchSettings.json
├── appsettings.json
└── Program.cs
```

---

## 🔌 Endpoints

### Ganho

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/ganho` | Listar todos os ganhos |
| `GET` | `/api/ganho/mes/{mes}/ano/{ano}` | Listar ganhos por mês e ano |
| `GET` | `/api/ganho/ano/{ano}` | Listar ganhos por ano |
| `POST` | `/api/ganho` | Criar novo ganho |
| `PUT` | `/api/ganho/{id}` | Atualizar ganho |
| `DELETE` | `/api/ganho/{id}` | Deletar ganho |

### Gasto

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/gasto` | Listar todos os gastos |
| `GET` | `/api/gasto/mes/{mes}/ano/{ano}` | Listar gastos por mês e ano |
| `GET` | `/api/gasto/ano/{ano}` | Listar gastos por ano |
| `POST` | `/api/gasto` | Criar novo gasto |
| `PUT` | `/api/gasto/{id}` | Atualizar gasto |
| `DELETE` | `/api/gasto/{id}` | Deletar gasto |

### Reserva

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/reserva` | Listar todas as reservas |
| `GET` | `/api/reserva/mes/{mes}/ano/{ano}` | Listar reservas por mês e ano |
| `GET` | `/api/reserva/ano/{ano}` | Listar reservas por ano |
| `POST` | `/api/reserva` | Criar nova reserva |
| `PUT` | `/api/reserva/{id}` | Atualizar reserva |
| `DELETE` | `/api/reserva/{id}` | Deletar reserva |

### Resumo

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/resumo` | Resumo geral de tudo |
| `GET` | `/api/resumo/mes/{mes}/ano/{ano}` | Resumo financeiro do mês |

---

## 📝 Exemplos de Uso

### Criar um Ganho

```http
POST https://localhost:[porta]/api/ganho
Content-Type: application/json

{
  "valor": 3000.00,
  "data": "2025-05-01"
}
```

### Criar um Gasto

```http
POST https://localhost:[porta]/api/gasto
Content-Type: application/json

{
  "valor": 150.00,
  "data": "2025-05-09",
  "descricao": "Mercado"
}
```

### Criar uma Reserva

```http
POST https://localhost:[porta]/api/reserva
Content-Type: application/json

{
  "valor": 500.00,
  "data": "2025-05-09",
  "descricao": "Viagem"
}
```

### Ver Resumo do Mês

```http
GET https://localhost:[porta]/api/resumo/mes/5/ano/2025
```

Resposta:

```json
{
  "mes": 5,
  "ano": 2025,
  "totalGanhos": 3000.00,
  "totalGastos": 150.00,
  "totalReservas": 500.00,
  "saldo": 2350.00
}
```

---

## 📊 Códigos de Resposta

| Código | Significado |
|---|---|
| `200` | Sucesso |
| `201` | Criado com sucesso |
| `204` | Sucesso sem conteúdo (DELETE/PUT) |
| `400` | Dados inválidos |
| `404` | Não encontrado |

---

## ⚙️ Configuração

### CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
```

### Banco de Dados

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=financeiro.db"));
```

---

## 🐛 Troubleshooting

### Banco de dados não encontrado

```bash
dotnet ef database update
```

### Porta já está em uso

```bash
dotnet run --urls "https://localhost:[porta]"
```

### Pacotes NuGet não encontrados

```bash
dotnet restore
```

### Problema com Entity Framework

```bash
dotnet tool uninstall -g dotnet-ef
dotnet tool install -g dotnet-ef --version 9.0.0
```

---

## 👨‍💻 Autor

**Vítor Fernando**

- GitHub: [@lenvy001](https://github.com/lenvy001)

---

⭐ Se este projeto foi útil, considere dar uma estrela!
