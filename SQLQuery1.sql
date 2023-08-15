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

CREATE PROCEDURE [dbo].[updateMachineStatus]
	(@mStatus INT, @mId INT, @delay TIME)
AS
BEGIN
	WAITFOR DELAY @delay;
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