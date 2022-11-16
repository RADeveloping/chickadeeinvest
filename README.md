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

## Back-End Controllers

After doing ```dot net watch run``` and you are led to a localhost environment, you can add ```/swagger``` behind the URL to access swagger and all the API paths provisioned in the controllers.
# POST requests

Except for the ```Ticket``` model, the models in the ```/Models``` directory all have their Ids created automatically via ```Guid.NewGuid().ToString()``` so one need not provision the POST call with the id inside the object that you pass through. The following are examples of JSON objects that you can pass to create objects for each data model.

# Company

```Authorized for SuperAdmin only```
```
{
    "name": "Company A",
    "address": "785 Evergreen Terrace",
    "phone": "778-777-7777",
    "email": "company@gmail.com"
}
```

# Message

```Anyone can send messages```
```
{
    "content": "content",
    "senderId": "4752bbeb-696a-4641-b91d-ce5f526ab16d",
    "ticketId": 1
}
```
Where senderId is the Id of the current user and ticketId is the Id of the ticket.

# Property

```Authorized for PropertyManager and SuperAdmin```
```
{
    "name": "The Orient managed by PM1",
    "address": "785 Evergreen Terrace"
}
```

# Property Manager

```Authorized for SuperAdmin only```
```
{
    "email": "james@jones.com",
    "firstName":"James",
    "lastName":"Jones",
    "profilePicture": null
}
```

# Tenants

```Authorized for PropertyManager and SuperAdmin```

```Requires propertyId and unitId beforehand```

```
propertyId: 1421253d-b524-419c-8cad-f7b616092409
unitId:     b4804e35-ee25-44b3-980f-fed3a965af10

{
    "email": "james@tortia.com",
    "phoneNumber": "7788888888",
    "firstName": "James",
    "lastName": "Tortia",
    "unitId": "b4804e35-ee25-44b3-980f-fed3a965af10"
}
```

# Units

```Authorized for SuperAdmin only```

```Requires propertyId```

```
propertyId: 1421253d-b524-419c-8cad-f7b616092409

{
    "unitNo": 101,
    "unitType": 0,
    "propertyId": "1421253d-b524-419c-8cad-f7b616092409"
}
```
You can also provide ```propertyManagerId``` as well

# Tickets

```Anyone can make tickets```

```Requires propertyId and unitId```

```
propertyId:  1421253d-b524-419c-8cad-f7b616092409
unitId:      b4804e35-ee25-44b3-980f-fed3a965af10

{
    "problem": "Problem",
    "description": "Short Description here",
    "estimatedDate": "2022-11-08T05:57:50.681Z",
    "status": 0,
    "severity": 3,
    "unitId": "b4804e35-ee25-44b3-980f-fed3a965af10"
}
```
Do not need to fill out ```createdOn``` as it will be filled automatically as the object
is created.

Do not need to fill in the ```createdById``` as it will take the id of the current user

# TicketImages

```Only the creator of the ticket can put images```

```Requires ticketId```

```
createdById:  4752bbeb-696a-4641-b91d-ce5f526ab16d
{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "ticketId": 1,
    "createdById": "4752bbeb-696a-4641-b91d-ce5f526ab16d"
}
```
```createdById``` can be attained by GET request for the specific ticket.
# UnitImages

```Authorized for PropertyManager and SuperAdmin```

```Requires propertyId and unitId```

```
propertyId:  d9087d78-fb59-474c-87fc-67174186be48
unitId:      fb904b58-b19f-463a-b6ff-c94e811165e2

{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "unitId": "fb904b58-b19f-463a-b6ff-c94e811165e2"
}
```
```data``` is a ```byte[]``` type.

# UnitNotes

```Authorized for PropertyManager in charge of the unit```

```
{
    "message": "hey",
    "unitId": "03c253b1-54d1-4c25-afe9-63100dce6303"
}
```
# VerificationDocuments

```Authorized for Tenants```

```
{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "documentType": 0
}
```
The datatype of ```data``` is ```byte[]```.

The ```tenantId``` will be filled out with the current user's Id (Given that the user is a tenant who exists in the database, of course.).

## Navigation Menu

The navigation menu for the ASP.NET side is included in the ```/Areas/Identity/Pages/``` directory, inside ```_ManageNav.cshtml``` 

## Improvements

 - ASP.NET "partials" can be used to reduce the amount of code between the pages since most pages have similar content.
 - Depending on what the client wants, fields can be added or removed from each of the individual pages.

