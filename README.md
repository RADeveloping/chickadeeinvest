![Chickadee Logo](./wwwroot/images/logo.png)

# chickadee

🐥 chickadee powers homes

This repo has two major branches: `main` and `develop`, and feature branches for any feature in development.

- `main` - Has the most recent stable version of the application
- `develop` - Has the most recent in-development versions of the application; used for development and integration testing of front end and back end before merging to `main`

## Local Environment Setup

Use the following instructions to start the project on your local machine:

### Docker Compose

1. Download and install [Docker Desktop](https://www.docker.com/products/docker-desktop/).
2. Serve both the front end and back end at https://localhost:8888.

```bash
docker-compose up
```

### Manual

1. Download and install dependencies:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js](https://nodejs.org/en/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

2. Run SQL Server in Linux Container.

```bash
docker run --cap-add SYS_PTRACE -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=SqlPassword! -p 1444:1433 --name azsql -d mcr.microsoft.com/azure-sql-edge
```

3. Install the necessary CLI tools for Entity Framework Core.

```bash
dotnet tool install -g dotnet-ef
```

4. Apply database migrations to update the database.

```bash
dotnet ef database update
```

5. Serve both the front end and back end at https://localhost:7114.

```bash
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

---
# ASP.NET MVC

## Layout File

The ```Areas/Identity/Pages/Account/Manage/_Layout.cshtml``` file is the starting point of all ASP.NET pages.

This file contains the main container and then rendering each of the other pages using ``` @RenderBody()```.

## Profile Page View ASP.NET

Anything related to the user profile, including changing password, email, etc are in the ```Areas/Identity/Pages/Account``` directory.

Each page has a ```.cshtml``` file and a corresponding ```.cshtml.cs``` file. 

<i>For the most part, in this directory, you shouldn't really need to go inside the ```.cshtml.cs``` file.</i>

## SuperAdmin View ASP.NET

The controllers are in the ```/Controllers``` directory
The views are in the ```/Views``` directory.
The models are in the ```/Models``` directory.

This structure must be maintained. If the structure is modified, then the api mapping must also be changed to reflect the changes.

The controllers for the SuperAdmin are prefixed with "SA" followed by the page name.

For example:  ```SACompanyController```

For each controller there is also a accompanying model in the ```/Models``` directory.

To make changes the views, simply go to ```/View/{ControllerName}``` directory and select the corresponding .cshtml file you would like to edit.

The pages use bootstrap, so updating them should be fairly simple.

For more information regarding how boostrap works, you can visit the official [boostrap](https://getbootstrap.com/) website..

The [bootstrap-table](https://bootstrap-table.com/) is being being used for the index pages and is highly customizable as well.

The SuperAdmin is not using the same API the front end is using. Therefore if the front end goes down, the SuperAdmin would still have access to the back end to make any changes.

There is too much to explain regarding the MVC in ASP.NET that wouldn't be beneficial to include here, however Microsoft's [Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-6.0) is a great resource to refer to.

## Navigation Menu

The navigation menu for the ASP.NET side is included in the ```/Areas/Identity/Pages/``` directory, inside ```_ManageNav.cshtml``` 

## Improvements

 - ASP.NET "partials" can be used to reduce the amount of code between the pages since most pages have similar content.
 - Depending on what the client wants, fields can be added or removed from each of the individual pages.

