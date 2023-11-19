# angular-app-demo

## Description

## Instructions

 1. Database
    
    This project's database is created and managed using EntityFramework.
    This means that we will create all the tables and fill in the seeded data automatically using the different migrations set on the back-end side. For now we only need to create an empty DB named "CEZ_AngularDemoDB" on your local server.
    A simple "CREATE DATABASE CEZ_AngularDemoDB;" will suffice (a sql script file is also provided, for ease of use).
 3. Back-end
     - Open the project "CEZ.AngularDemo.sln" in Visual Studio.
     - Set up "CEZ.AngularDemo.WebAPI" as the startup project and restore NuGet packages if necessary.
     - Open "appsettings.json" and change the connection string to point to your local DB if necessary.
     - Go to Tools > NuGet Package Manager > Packet Manager Console. Run "update-database".
     - The database should now have all the tables and seeded initial data.
     - You can now run the project. A new browser window with a Swagger landing page should open (https://localhost:7202/swagger/index.html).
 4. Front-end
     - Open the folder "angular-demo" with VSCode (or similar).
     - Open the console (in VSCode, Ctrl+Ñ).
     - Run "npm i".
     - To start up the project, run "ng serve" or "npm run serve". The project should be accesible at http://localhost:4200/
