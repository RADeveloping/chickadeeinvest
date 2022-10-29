![Chickadee Logo](./wwwroot/images/logo.png)

# chickadee

🐥 chickadee powers homes

This repo has two major branches: `main` and `develop`, and feature branches for any feature in development.

- `main` - Has the most recent stable version of the application
- `develop` - Has the most recent in-development versions of the application; used for development and integration testing of front end and back end before merging to `main`

## Setup

```bash
# Run SQL Server in Linux Container.
docker run --cap-add SYS_PTRACE -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=SqlPassword! -p 1444:1433 --name azsql -d mcr.microsoft.com/azure-sql-edge

# Install the necessary CLI tools for Entity Framework Core.
dotnet tool install -g dotnet-ef

# Apply database migrations to update the database.
dotnet ef database update

# Serve both front end and back end at https://localhost:7114.
dotnet run
```

### Client Setup for Development (Optional)

```bash
# Go to the directory ClientApp.
cd ClientApp

# Install dependencies.
npm install

# Serve with hot reload at https://localhost:44443.
npm start
```

For a detailed explanation of how things work, check out the [guide](https://reactjs.org/docs/getting-started.html) and [API docs](https://reactjs.org/docs/react-api.html) for React; and the [guide](https://learn.microsoft.com/en-us/aspnet/tutorials) and [API docs](https://learn.microsoft.com/en-us/aspnet/core/) for ASP.NET.
