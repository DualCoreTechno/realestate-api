﻿# Project Title
	- RealtorEmsAPIs

# Project Description
	- This project perform insert, update, delete and display actions of page items in MSSQL server database.

# Please follow these steps for run this web api application.

- First open application in latest version of visual studio like VS2022.

- Change connection string in appsettings.Development.json file under appsettings.json file as per your server configuration.


- For create database and tables in MSSQL server select Data project in Default project drop-down in package manager console run below commands.
	
##Commands

	- Package Manager Console :- add-migration MigrationName
		- CLI :- dotnet ef migrations add MigrationName
	
	- Package Manager Console :- Update-Database
		- CLI :- dotnet ef database update


- After migration created succesfully perfrom all the test cases which are exists in TodoAPI.Test project.

- Now you can execute web api.