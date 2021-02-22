<div>
	<a href="https://www.alza.cz" target="_blank">
		<img src="https://cdn.alza.cz/Styles/images/svg/alza_cz.svg" width=250>
	</a>
</div>

# Alza test
#### implemented by Ondrej Piza

This repository is created for job evalutaion purposes only and therefore should not be shared, forked or copied in any way.

## Prerequisities
- **.NET CORE 3.1** (https://dotnet.microsoft.com/download)
- **MSSQL** running instance with **AlzaTest** database (https://www.microsoft.com/cs-cz/sql-server/sql-server-downloads)
- **Windows authentication** is used for connecting to database, so make sure current user has required privileges to access specified database
- After MSSQL is started, check address (IP, DNS, localhost) with appropriate port and update connection strings in **appsettings.Development.json** and **appsettings.Production.json** 

## How to run project
- **Visual Studio** - open solution and run Products.API project
- **Dotnet CLI** - navigate to the Products.API folder and execute `dotnet run` command

## How to run tests
- **Visual Studio** - open solution, go for Products.UnitTests project and call Run tests option
- **Dotnet CLI** - either on solution level (eg. root folder with sln file) or on Product.UnitTests level run `dotnet test` which will run either all tests in solution or all tests in specified test project

## Documentation
- After **Product.API** project is running, there is **Swagger documentation** available under URL** http://localhost:5000/swagger**

> That app base url is default value, it can be changed if neccessary in project properties

## Demo data
- As part of the very first startup, database structure is created and demo data of 26 products are inserted. This can be changed before first run in the **InitialMigration** class.

## Project structure
- **Products.API** - the only runable project containing bussiness logic and controllers
- **Products.Domain** - containing domain specific objects, can be used for sharing DTO classes
- **Products.UnitTests** - tests for Products domain, eg. Products.API and Products.Domain

- **AlzaTest.Utils** - Extensions and DTO classes, which should be domain agnostic, therefore widely reusable