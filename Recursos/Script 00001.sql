/* Create login for Bizagi Process Admin*/
USE [master]
GO

/*change the following Variables with the user, Password and Database name before execution*/
DECLARE @UserName VARCHAR(120) = 'Developer'        --Write  User name to conecct the database
DECLARE @UserPW NVARCHAR(120) = 'D3v3l0p3%'     --Type Password for the user
DECLARE @DBName VARCHAR(120) = ''        --Specify database name to assign user

EXEC('CREATE LOGIN [' + @UserName + '] WITH PASSWORD=N''' + @UserPW + ''', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF')

EXEC('ALTER LOGIN [' + @UserName + '] ENABLE')

EXEC('CREATE USER [' + @UserName + '] FOR LOGIN [' + @UserName + ']')

EXEC('GRANT BACKUP DATABASE TO [' + @UserName + ']')

EXEC('GRANT CREATE DATABASE TO [' + @UserName + ']')

EXEC('GRANT ALTER SERVER STATE TO [' + @UserName + ']')


set @DBName = 'SysBD_00_Master';

EXEC('
USE ' + @DBName + '

CREATE USER [' + @UserName + '] FROM LOGIN [' + @UserName + ']

ALTER ROLE [db_owner] ADD MEMBER [' + @UserName + ']

ALTER ROLE [db_datareader] ADD MEMBER [' + @UserName + ']

ALTER ROLE [db_datawriter] ADD MEMBER [' + @UserName + ']

');

set @DBName = 'SysBD_000_01Enterprise';

EXEC('
USE ' + @DBName + '

CREATE USER [' + @UserName + '] FROM LOGIN [' + @UserName + ']

ALTER ROLE [db_owner] ADD MEMBER [' + @UserName + ']

ALTER ROLE [db_datareader] ADD MEMBER [' + @UserName + ']

ALTER ROLE [db_datawriter] ADD MEMBER [' + @UserName + ']

');