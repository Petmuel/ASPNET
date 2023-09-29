CREATE PROCEDURE getAllUsers
AS 
SELECT * FROM TblUser

EXEC getAllMachine

CREATE PROCEDURE checkUser
	(@userEmail Varchar(30), @userPassword Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblUser
	WHERE Email= @userEmail AND Password= @userPassword
END

CREATE PROCEDURE checkEmail
	(@userEmail Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblUser
	WHERE Email= @userEmail
END

use [newDB]
GO
CREATE PROCEDURE [dbo].[checkPassword]
	(@userPassword Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblUser
	WHERE Password= @userPassword
END

EXEC [newDB].[dbo].[checkPassword] 'ttt@gmail.com'
EXEC checkUser 'okok', '123123'
CREATE PROCEDURE createUser
	(@userEmail Varchar(30), @userPassword Varchar(30))
AS
BEGIN
	INSERT INTO TblUser (Email, Password)
	VALUES (@userEmail, @userPassword)
END

EXEC createUser 'okok@gmail.com', '123123'

SELECT @@ROWCOUNT

CREATE TABLE [dbo].[TblMachine] (
    [MachineID] INT NOT NULL IDENTITY(1, 1),
    [MachineName]			VARCHAR (255) NULL,
    [MachineStatus]			INT NOT NULL,
);

CREATE TABLE [dbo].[TblUser] (
    [ID] INT NOT NULL IDENTITY(1, 1),
    [Email]			VARCHAR (255) NOT NULL,
    [Password]		VARCHAR (255) NOT NULL,
);

CREATE PROCEDURE addMachine
	(@mName Varchar(255))
AS
BEGIN
	INSERT INTO TblMachine (MachineName, MachineStatus)
	VALUES (@mName, FLOOR(RAND()*(2-1+1))+1)
END

EXEC addMachine 'Machine1'

SELECT * FROM TblMachine

TRUNCATE TABLE tblUser

CREATE PROCEDURE editUser
	(@userEmail VARCHAR(255), @userPassword VARCHAR (255), @userID INT)
AS
BEGIN
	UPDATE TblUser
	SET Email = @userEmail,
	Password = @userPassword
	WHERE ID = @userID
END

CREATE PROCEDURE deleteUser
	(@userID INT)
AS
BEGIN
	DELETE FROM TblUser WHERE ID = @userID;
END

CREATE PROCEDURE checkEmail
	(@userEmail Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblUser 
	WHERE Email= @userEmail
END

CREATE PROCEDURE checkEmail
	(@userEmail Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblUser 
	WHERE Email= @userEmail
END

CREATE PROCEDURE checkMachine
	(@mName Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblMachine 
	WHERE MachineName= @mName
END

CREATE PROCEDURE checkMachinePassword
	(@mPassword Varchar(30))
AS 
BEGIN
	SELECT COUNT(*) FROM TblMachine 
	WHERE Machine_Password= @mPassword
END

checkMachinePassword 'MID29123'

CREATE PROCEDURE getAllMachine
AS 
SELECT * FROM TblMachine

EXEC getAllMachine

CREATE PROCEDURE deleteMachine
	(@mId INT)
AS
BEGIN
	DELETE FROM TblMachine WHERE MachineID = @mId;
END

CREATE PROCEDURE editMachine
	(@mName VARCHAR(255), @mId INT)
AS
BEGIN
	UPDATE TblMachine
	SET MachineName = @mName
	WHERE MachineID = @mId
END

USE [newDB]
GO
CREATE PROCEDURE [dbo].[getAuditLogs]
AS 
BEGIN
	SELECT event_time, 
	action_id, 
	server_principal_name, 
	client_ip, 
	additional_information 
	FROM sys.fn_get_audit_file('C:\Audit\*', NULL, NULL);
END

USE [newDB]
GO
CREATE PROCEDURE [dbo].[updateMachineStatus]
	(@mStatus INT, @mId VARCHAR(30))
AS
BEGIN
	UPDATE TblMachine
	SET MachineStatus = @mStatus 
	WHERE MachineID = @mId
END

CREATE PROCEDURE [dbo].[getMachineStatus]
	(@mId INT)
AS
BEGIN
	SELECT MachineStatus FROM TblMachine
	WHERE MachineID = @mId 
END

EXEC getAllMachine
EXEC getMachineStatus '7'

CREATE PROCEDURE [dbo].[checkUpdatedMachineStatus]
	(@mId INT, @mStatus INT)
AS
BEGIN
	SELECT * FROM TblMachine
	WHERE MachineID = @mId AND MachineStatus = @mStatus
END

EXEC checkUpdatedMachineStatus '3', '2','00:00:02'

CREATE TABLE [dbo].[TblAuditLogs] (
    [EventTime] DATETIME,
    [SequenceNumber] INT,
    [ActionID] INT,
    [ServerName] NVARCHAR(128),
    [DatabaseName] NVARCHAR(128),
    [ObjectName] NVARCHAR(128),
    [SchemaName] NVARCHAR(128),
    [AuditName] NVARCHAR(128),
    [Statement] NVARCHAR(MAX),
    [AdditionalInfo] XML
);

select * from [newDB].[dbo].[TblAuditLogs]

DROP Table TblAuditLogs

USE [newDB]
GO
CREATE PROCEDURE [dbo].[getAuditLogs]
AS 
BEGIN
	SELECT event_time, 
	action_id, 
	server_principal_name, 
	client_ip, 
	additional_information 
	FROM sys.fn_get_audit_file('C:\Audit\*', NULL, NULL);
END

ALTER TABLE TblMachine ADD Machine_Password VARCHAR(50) 
Select * from TblMachine Where MachineID = '29';
UPDATE TblMachine
SET Machine_password = 'MID31123'
WHERE MachineID = '31';
UPDATE TblMachine
SET Machine_password = 'MID26123'
WHERE MachineID = '26';
UPDATE TblMachine
SET Machine_password = 'MID30123'
WHERE MachineID = '30';

USE[newDB]
GO
CREATE PROCEDURE [dbo].[updateMachineStatus]
    (@mStatus INT, @mId VARCHAR(30), @result INT OUTPUT)
AS
BEGIN
    SET @result = 0; -- Initialize result to indicate failure

    -- Check if the machine with @mId exists
    IF EXISTS (SELECT 1 FROM TblMachine WHERE MachineID = @mId)
    BEGIN
        -- Machine exists, proceed with the update
        UPDATE TblMachine
        SET MachineStatus = @mStatus 
        WHERE MachineID = @mId;

        IF @@ROWCOUNT > 0
        BEGIN
            -- Rows were updated, set the result to indicate success
            SET @result = 1;
        END
    END
    ELSE
    BEGIN
        -- Machine with @mId does not exist, set the result to indicate failure
        SET @result = 0;
    END
END
