# angular-app-demo

## Description

Simple Angular13 project to showcase the technology. It features a simple Users CRUD page, with soft deletion. Changes to Users are logged into the History page.

Backend was made using .Net 6 and Entity Framework (code-first) to manage the database. It features a Mediator structure, where received queries are managed by MediatR and divided between Commands and Queries, separating responsibilities. Data input validation is done using FluentValidation.

## Screenshots

![image](https://github.com/Cezeta-hub/angular-app-demo/assets/73889711/969cc526-2e26-420f-abff-87a42768088d)

![image](https://github.com/Cezeta-hub/angular-app-demo/assets/73889711/cffdf8b7-cc1d-4e93-84eb-725096c0b31b)

![image](https://github.com/Cezeta-hub/angular-app-demo/assets/73889711/5a79f05b-25a7-4007-bb2e-27dfcb15c25e)

![image](https://github.com/Cezeta-hub/angular-app-demo/assets/73889711/76a65e51-d7dc-4342-8ec8-f7cf6bf30c2d)

## Instructions

 1. Database
    
    This project's database is created and managed using **EntityFramework**.
    
    This means that we will create all the tables and fill in the seeded data automatically using the different migrations set on the back-end side. For now we only need to create an empty DB named `CEZ_AngularDemoDB` on your local server.
    
    A simple `CREATE DATABASE CEZ_AngularDemoDB;` will suffice (a sql script file is also provided, for ease of use).
 3. Back-end
     - Open the project _"CEZ.AngularDemo.sln"_ in Visual Studio.
     - Set up _"CEZ.AngularDemo.WebAPI"_ as the startup project and restore NuGet packages if necessary.
     - Open _"appsettings.json"_ and change the connection string to point to your local DB if necessary.
     - Go to Tools > NuGet Package Manager > Packet Manager Console. Run `update-database`.
     - The database should now have all the tables and seeded initial data.
     - You can now run the project. A new browser window with a Swagger landing page should open (https://localhost:7202/swagger/index.html).
 4. Front-end
     - Open the folder _"angular-demo"_ with VSCode (or similar).
     - Open the console (in VSCode, Ctrl+Ñ).
     - Run `npm i`.
     - To start up the project, run `ng serve` or `npm run serve`. The project should be accesible at http://localhost:4200/
