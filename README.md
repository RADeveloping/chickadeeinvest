![Chickadee Logo](./wwwroot/images/logo.png)

# chickadee

🐥 chickadee powers homes
<!-- TOC -->

- [chickadee](#chickadee)
    - [Local Environment Setup](#local-environment-setup)
        - [Docker Compose](#docker-compose)
        - [Manual](#manual)
        - [Client Setup for Development Optional](#client-setup-for-development-optional)
    - [Remote Environment Setup](#remote-environment-setup)
        - [About Docker Compose Configuration Files](#about-docker-compose-configuration-files)
- [SuperAdmin and Profile View](#superadmin-and-profile-view)
    - [Layout File](#layout-file)
    - [Profile Page View ASP.NET](#profile-page-view-aspnet)
    - [SuperAdmin View ASP.NET](#superadmin-view-aspnet)
    - [Non-SA Back-End Controllers](#non-sa-back-end-controllers)
    - [GET requests](#get-requests)
    - [Property](#property)
    - [Unit](#unit)
    - [POST requests](#post-requests)
    - [Company](#company)
    - [Message](#message)
    - [Property](#property)
    - [Property Manager](#property-manager)
    - [Tenants](#tenants)
    - [Units](#units)
    - [Tickets](#tickets)
    - [TicketImages](#ticketimages)
    - [UnitImages](#unitimages)
    - [UnitNotes](#unitnotes)
    - [VerificationDocuments](#verificationdocuments)
    - [PATCH request](#patch-request)
    - [DELETE request](#delete-request)
    - [Navigation Menu](#navigation-menu)
    - [Improvements](#improvements)

<!-- /TOC -->
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

## Remote Environment Setup

Use the following instructions to deploy the project to one of the two available remote environments on Azure - `dev` and `dev2` as of 11/18/2022:

1. Go to the CI action for your commit. Depending on which branch you are on, you can find it in different places.
   1. If you are on the `main` or `develop` branch
      1. Click on the build status indicator next to the commit hash.
      
         <img width="320" alt="Screenshot 2022-11-18 at 12 05 23 AM" src="https://user-images.githubusercontent.com/5898658/202653247-c931e187-77a8-4fcc-a0ca-fcb1ec58815f.png">
      2. Find the CI job in the pop-up.
   2. If you are on your feature branch
      1. Open a pull request with either `develop` (recommended) or `main` as the base branch.
      2. Find the CI job at the bottom of your pull request. From now on, every commit you push to your branch will trigger a build action to run as a CI job.
      <img width="900" alt="Screenshot 2022-11-18 at 1 13 34 AM" src="https://user-images.githubusercontent.com/5898658/202665501-71bdf882-93a6-4545-88e6-63b31d31c89c.png">
2. Click **Details**.

   <img width="500" alt="Screenshot 2022-11-18 at 12 06 53 AM" src="https://user-images.githubusercontent.com/5898658/202653363-638a8a93-0ca3-4f75-9538-aef822570532.png">

3. In the **build** job, expand the step **Build and push the Docker image**.
   <img width="500" alt="Screenshot 2022-11-18 at 12 10 06 AM" src="https://user-images.githubusercontent.com/5898658/202653623-1fc0107d-3cdb-42a2-94a7-9f2cde8696fc.png">

4. Near the end of the output, copy the image tag after the message "Successfully tagged" e.g. `chickadeeinvest/chickadee:1667885238`. You will be deploying this specific image to a remote environment soon.
   <img width="500" alt="Screenshot 2022-11-18 at 12 11 25 AM" src="https://user-images.githubusercontent.com/5898658/202654133-d9d6358a-d240-41ac-8e34-caff270c3917.png">
5. Sign in to [Azure Portal](https://portal.azure.com/).
6. Go to the Azure App Service resource for the environment to which you would like to deploy the image. You may want to double-check with your team to ensure the environment is not in use by someone.
   <img width="1756" alt="Screenshot 2022-11-18 at 12 43 37 AM" src="https://user-images.githubusercontent.com/5898658/202659347-842bbfa9-d1ee-4862-bd8e-83bbb1cd1c2e.png">

7. Under the **Deployment** section, click **Deployment Center**.

   <img width="250" alt="Screenshot 2022-11-18 at 12 45 22 AM" src="https://user-images.githubusercontent.com/5898658/202659664-660c9836-76fc-4912-b889-412f5d5886a2.png">

8. In **Registry settings/Config**, replace the image tag at `services.chickadee.image` with the one you copied in step 4. You may want to choose a new **Configuration File** if it was changed since the last deployment (see [About Docker Compose Configuration Files](#about-docker-compose-configuration-files)).
   <img width="720" alt="Screenshot 2022-11-18 at 12 49 13 AM" src="https://user-images.githubusercontent.com/5898658/202660511-ca06acd7-4fff-48a2-9426-b35645c4a5c0.png">

9. Click **Save**. Your image is now being deployed. Allow 5-10 minutes for the deployment to complete.

   <img width="100" alt="Screenshot 2022-11-18 at 12 51 23 AM" src="https://user-images.githubusercontent.com/5898658/202660900-76acfbb4-3927-45da-9155-f8990879c1c6.png">

### About Docker Compose Configuration Files

A remote environment is provisioned as a multi-container app using a Docker Compose configuration. For your reference, a copy of the per environment configuration file is stored in `docker-compose.<env>.yml`.

Each configuration file defines services, as well as the Docker image and environment variables for each service. As of 21/11/2022, there are two services:

- `chickadee` (React front end and ASP.NET back end)
- `chickadee-db` (SQL Server database server)

Always keep a copy of the configuration file known to be working so that you can revert to it at any time by simply choosing the file in the Azure App Service Deployment Center.

---

# SuperAdmin and Profile View

## Layout File

The `Areas/Identity/Pages/Account/Manage/_Layout.cshtml` file is the starting point of all ASP.NET pages.

This file contains the main container and then rendering each of the other pages using ` @RenderBody()`.

## Profile Page View ASP.NET

Anything related to the user profile, including changing password, email, etc are in the `Areas/Identity/Pages/Account` directory.

Each page has a `.cshtml` file and a corresponding `.cshtml.cs` file.

<i>For the most part, in this directory, you shouldn't really need to go inside the `.cshtml.cs` file.</i> Simply editing the ```.cshtml``` should be enough. 

## SuperAdmin View ASP.NET

The controllers are in the `/Controllers` directory
The views are in the `/Views` directory.
The models are in the `/Models` directory.

This structure must be maintained. If the structure is modified, then the api mapping must also be changed to reflect the changes.

The controllers for the SuperAdmin are prefixed with "SA" followed by the page name.

For example: `SACompanyController`

For each controller there is also an accompanying model in the `/Models` directory.

To make changes the views, simply go to `/View/{ControllerName}` directory and select the corresponding .cshtml file you would like to edit. The page uses regular HTML but with the ability to use c# code.

The pages use bootstrap, so updating them should be fairly simple.

For more information regarding how boostrap works, you can visit the official [boostrap](https://getbootstrap.com/) website..

The [bootstrap-table](https://bootstrap-table.com/) is being being used for the index pages and is highly customizable as well.

<i>The SuperAdmin is **not** using the same API the front end is using. Therefore if the front end goes down, the SuperAdmin would still have access to the back end to make any changes.</i>

There is too much to explain regarding the MVC in ASP.NET that wouldn't be beneficial to include here, however Microsoft's [Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-6.0) is a great resource to refer to.

## Non-SA Back-End Controllers

The controllers are inside `/Controllers` folder. Inside each controller are CRUD-based functions that act as GET, POST, PUT/PATCH, and DELETE requests.

After doing `dot net watch run` and you are led to a localhost environment, you can add `/swagger` behind the URL to access swagger and all the API paths provisioned in the controllers. There, you can try out each call and test authorization as well after logging in as a `SuperAdmin`, `PropertyManager`, or `Tenant`.

The basic CRUD calls are implemented via `dotnet-aspnet-codegenerator`. Anything that goes beyond the basic template will be noted below.

## GET requests

Due to the searchable nature of `Property`, `Unit`, and `Ticket`, the GET requests for the above objects take in `sort`, `param`, and `query` field.

The possible values for `sort` are `asc` (ascending) or `desc` (descending).

`param` values are dependent on the object and will be further detailed below.

`query` values are basically search values. Therefore, you will see `stringComparison` happening inside `param` string values like `address` and `name` for `Property` object.

Also, as noticed by the column list in the front-end, some of the GET requests are built on top of another - For example, to get all units related to a specific property, you would have to pass in `/api/properties/<propertyId>/units` and then build on top of that if you want to get all tickets from that unit like `/api/properties/<propertyId>/units/<unitId>/tickets`.

## Property

The GET request for all properties depends on the user. If the user is a SuperAdmin, it will return all properties with selected fields. Otherwise, it will return properties related to the requesting user (For PM, properties that they are managing and for Tenants, properties that their unit situated in).

The `param` values for Property are `address`, `id`, `open_count` (Number of open tickets), `unit_count`, `tenants_count`, and `name`.

## Unit

The GET request for all properties depends on the user. If the user is a SuperAdmin, it will return all units with selected fields. Otherwise, it will return units related to the requesting user (For PM, units that they are managing and for Tenants, units that they reside in).

The `param` values for Unit are `id`, `number` (Unit Number - e.g. 101), and `type` (e.g. OneBedroom).

## POST requests

Except for the `Ticket` model, the models in the `/Models` directory all have their Ids created automatically via `Guid.NewGuid().ToString()` so one need not provision the POST call with the id inside the object that you pass through. The following are examples of JSON objects that you can pass to create objects for each data model.

## Company

`Authorized for SuperAdmin only`

```
{
    "name": "Company A",
    "address": "785 Evergreen Terrace",
    "phone": "778-777-7777",
    "email": "company@gmail.com"
}
```

## Message

`Anyone can send messages`

```
{
    "content": "content",
    "senderId": "4752bbeb-696a-4641-b91d-ce5f526ab16d",
    "ticketId": 1
}
```

Where senderId is the Id of the current user and ticketId is the Id of the ticket.

## Property

`Authorized for PropertyManager and SuperAdmin`

```
{
    "name": "The Orient managed by PM1",
    "address": "785 Evergreen Terrace"
}
```

## Property Manager

`Authorized for SuperAdmin only`

```
{
    "email": "james@jones.com",
    "firstName":"James",
    "lastName":"Jones",
    "profilePicture": null
}
```

## Tenants

`Authorized for PropertyManager and SuperAdmin`

`Requires propertyId and unitId beforehand`

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

## Units

`Authorized for SuperAdmin only`

`Requires propertyId`

```
propertyId: 1421253d-b524-419c-8cad-f7b616092409

{
    "unitNo": 101,
    "unitType": 0,
    "propertyId": "1421253d-b524-419c-8cad-f7b616092409"
}
```

You can also provide `propertyManagerId` as well

## Tickets

`Anyone can make tickets`

`Requires propertyId and unitId`

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

Do not need to fill out `createdOn` as it will be filled automatically as the object
is created.

Do not need to fill in the `createdById` as it will take the id of the current user

## TicketImages

`Only the creator of the ticket can put images`

`Requires ticketId`

```
createdById:  4752bbeb-696a-4641-b91d-ce5f526ab16d
{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "ticketId": 1,
    "createdById": "4752bbeb-696a-4641-b91d-ce5f526ab16d"
}
```

`createdById` can be attained by GET request for the specific ticket.

## UnitImages

`Authorized for PropertyManager and SuperAdmin`

`Requires propertyId and unitId`

```
propertyId:  d9087d78-fb59-474c-87fc-67174186be48
unitId:      fb904b58-b19f-463a-b6ff-c94e811165e2

{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "unitId": "fb904b58-b19f-463a-b6ff-c94e811165e2"
}
```

`data` is a `byte[]` type.

## UnitNotes

`Authorized for PropertyManager in charge of the unit`

```
{
    "message": "hey",
    "unitId": "03c253b1-54d1-4c25-afe9-63100dce6303"
}
```

## VerificationDocuments

`Authorized for Tenants`

```
{
    "data": "U3dhZ2dlciByb2Nrcw==",
    "documentType": 0
}
```

The datatype of `data` is `byte[]`.

The `tenantId` will be filled out with the current user's Id (Given that the user is a tenant who exists in the database, of course.).

## PATCH request

So far, there is only one PATCH request and it is for closing tickets. In order to PATCH the `status` field in `Ticket` object, you have to pass the following JSON body as a part of the PATCH request

```
[
    {
        "op": "replace",
        "path": "/status",
        "value": 1
    }
]
```

The `op` stands for operation of the PATCH which in this case is replacing the value inside the object.

The `path` refers to the field we are replacing the value for.

The `value` is 1 because the `status` field inside the Ticket object is an enum of 0 and 1 where 0 is `open` and 1 is `closed`.

## DELETE request

The DELETE requests should work with just the specific `id` provisioned for the desired objects.

## Navigation Menu

The navigation menu for the ASP.NET side is included in the `/Areas/Identity/Pages/` directory, inside `_ManageNav.cshtml`

FontAwesome is used for the navigation menu icons.

## Improvements

 - ASP.NET "partials" can be used to reduce the amount of duplicated code between the pages since most pages have similar content.
 - Depending on what the client wants, fields can be added or removed from each of the individual pages.
 - Update the html template that is used to send registration emails to users.
 - When creating a new ticket, add the ability to add images to the ticket. The backend has been setup, just need to connect it to react. Please refer to the ERD to see how images are related to tickets.
 - When displaying tickets, images should be displayed in the details page in a card (possibly inside a message)
 - Messaging/Commenting system should be implemented. The back end has been setup for this, however front end needs to be connected.