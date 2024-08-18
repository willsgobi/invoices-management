# Invoice Management

Este é um projeto **Fullstack** com o propósito de ter relatórios e visualizações de notas fiscais.

#### Frontend
Desenvolvido com Blazor Web App e rodando com Docker, este projeto se comunica com API para buscar os relatórios e notas fiscais. Com visualizações em gráficos, filtros diversos e com uma UI bem intuitiva.

#### Backend
Desenvolvido com .NET Core e rodando com Docker, realiza comunicações com o banco de dados SQL Server para obter as informações solicitadas pela UI já com os filtros necessários. Conta com paginação quando a busca não tiver filtros.

#### Database
O banco de dados é o SQL Server e roda localmente na máquina.

#### Versões das tecnologias utilizadas
• APS.NET Core Web App (Razor pages) e .NET Core API: .NET 8.0
• Bootstrap: 5.1.0
• ChartsJS: 2.9.4
• SQL Server: 16.0.1000.6
• Docker Desktop: Docker Engine 27.0.3
• SQL Server Management Studio: 20.2

#### Scripts SQL

Os scripts SQL necessários para este projeto estão na pasta *sql*:

Rode-os na seguinte order:
• CreateDatabase
• CreateTable
• InsertDataInvoice

## Debug

Para debugar os projetos, primeiramente você precisará ter o docker instalado em sua máquina. Feito isso, basta executar a API e o Web App.

**Atente-se:** Ao iniciar a API, verifique a porta que será disponibilizada para acesso da mesma está igual a informada no _Program.cs_ do Web App:
```
client.BaseAddress = new Uri("https://host.docker.internal:32773");
```

Caso seja diferente, basta alterar essa porta e tudo funcionará como deve ser.

### UI
![image](https://github.com/user-attachments/assets/efc710aa-d56b-48d0-a945-548333fdf3cd)

![image](https://github.com/user-attachments/assets/0207d03d-d85f-4a9b-a195-97691f37d0ec)
