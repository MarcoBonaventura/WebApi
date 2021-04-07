# WebApi back-end in C# and .NET
front-end can be found here: [Web App Job Management](https://github.com/MarcoBonaventura/web_app_Job_Management)		

Please notice this is a working-progress project, further functionalities will be implemented soon.  

## create the DB
Run 
```sql
CREATE DATABASE JobPortalDB
```

## create  tables

```sql
CREATE TABLE Job_Piano 
(JobID INT IDENTITY(1,1) PRIMARY KEY, 
JobName VARCHAR(255), 
Lib INT, 
Macro VARCHAR(100), 
Suspended Lib VARCHAR(100), 
Friday2X Lib VARCHAR(100), ),
JobPage Lib VARCHAR(100),
Prty INT,
Descr Lib VARCHAR(100),
Params Lib VARCHAR(100))
```

repeat the same for tables `Job_Italia` and `Job_Filiali`

```sql
CREATE TABLE Macro
(MacroID INT IDENTITY(1,1) PRIMARY KEY,
MacroVal VARCHAR(10))
```  


## Stored Procedures
just some stored procedures used by webapi, for full source open a request please.

```sql
USE [JobPortalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[getJobsList]( @PassedTableName as NVarchar(255) ) AS
BEGIN
    DECLARE @ActualTableName AS NVarchar(255)

    SELECT @ActualTableName = QUOTENAME( TABLE_NAME )
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = @PassedTableName

    DECLARE @sql AS NVARCHAR(MAX)
    SELECT @sql = 'SELECT JobID, Prty, Macro, JobName, Lib, Suspended, Friday2X, Descr, Params, JobPage FROM' + @ActualTableName + ' ORDER BY Prty;'

    EXEC(@SQL)
END
```

```sql
USE [JobPortalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[insertJobPiano]( @JobName as NVarchar(255), 
					@Macro as NVarchar(255), 
					@Lib as int,
					@Friday2X as NVarchar(255),
					@Suspended as NVarchar(255),
					@Descr as NVarchar(255),
					@Params as NVarchar(255),
					@JobPage as NVarchar(255),
					@Prty as int)
AS
BEGIN
	DECLARE @newID int;
	SELECT @newID = MAX(JobID) +1 FROM dbo.Job_Piano
	UPDATE dbo.Job_Piano SET Prty +=1 WHERE Prty >= @Prty 
    	INSERT INTO dbo.Job_Piano (JobID, JobName, Macro, Lib, Friday2X, Suspended, Descr, Params, JobPage, Prty) 
	VALUES(@newID, @JobName, @Macro, @Lib, @Friday2X, @Suspended, @Descr, @Params, @JobPage, @Prty)
END
```

```sql
USE [JobPortalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[updateJobPiano](	@JobID as int, 
					@JobName as NVarchar(255), 
					@Macro as NVarchar(255), 
					@Lib as int, 
					@Friday2X as NVarchar(255),
					@Suspended as NVarchar(255),
					@Descr as NVarchar(255),
					@Params as NVarchar(255),
					@JobPage as NVarchar(255),
					@Prty as int)
AS
BEGIN
	UPDATE dbo.Job_Piano 
	SET  JobName = @JobName, Macro = @Macro, Lib = @Lib, Friday2X = @Friday2X, Suspended = @Suspended, Descr = @Descr, Params = @Params, JobPage = @JobPage, Prty = @Prty 
	WHERE JobID = @JobID;
END
```

```sql
USE [JobPortalDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[deleteMultiJobPiano](@JobID as nvarchar(max), @JobPrty as nvarchar(max)) 
AS
BEGIN
	DECLARE @newPrty int;
	SET @newPrty = (SELECT Prty FROM dbo.Job_Piano WHERE JobID = @JobID)
	DELETE FROM dbo.Job_Piano WHERE JobID IN (@JobID)
	UPDATE dbo.Job_Piano SET Prty -=1 WHERE Prty > @newPrty
END
```
