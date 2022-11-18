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

8. In **Registry settings/Config**, replace the image tag at `services.chickadee.image` with the one you copied in step 4. You may want to choose a new **Configuration File** if it was changed since the last deployment.
   <img width="720" alt="Screenshot 2022-11-18 at 12 49 13 AM" src="https://user-images.githubusercontent.com/5898658/202660511-ca06acd7-4fff-48a2-9426-b35645c4a5c0.png">

9. Click **Save**. Your image is now being deployed. Allow 5-10 minutes for the deployment to complete.

   <img width="100" alt="Screenshot 2022-11-18 at 12 51 23 AM" src="https://user-images.githubusercontent.com/5898658/202660900-76acfbb4-3927-45da-9155-f8990879c1c6.png">

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

