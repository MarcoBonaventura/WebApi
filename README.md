# WebApi backend in C# and .NET
front-end can be found here: https://github.com/MarcoBonaventura/web_app_Job_Management

## create the DB
Run `CREATE DATABASE JobPortalDB` to create the dabatase.

## create  tables

```CREATE TABLE Job_Piano 
(JobID INT IDENTITY(1,1) PRIMARY KEY, 
JobName VARCHAR(255), 
Lib INT, 
Macro VARCHAR(100), 
Suspended Lib VARCHAR(100), 
Friday2X Lib VARCHAR(100), ),
JobPage Lib VARCHAR(100),
Prty INT,
Descr Lib VARCHAR(100),
Params Lib VARCHAR(100))```

repeate the same for tables `Job_Italia` and `Job_Filiali`

```CREATE TABLE Macro
(MacroID INT IDENTITY(1,1) PRIMARY KEY,
MacroVal VARCHAR(10))```  

## Build
