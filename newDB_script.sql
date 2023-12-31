USE [master]
GO
/****** Object:  Database [newDB]    Script Date: 09/08/2023 16:08:26 ******/
CREATE DATABASE [newDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'newDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\newDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'newDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\newDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [newDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [newDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [newDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [newDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [newDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [newDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [newDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [newDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [newDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [newDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [newDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [newDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [newDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [newDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [newDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [newDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [newDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [newDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [newDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [newDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [newDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [newDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [newDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [newDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [newDB] SET RECOVERY FULL 
GO
ALTER DATABASE [newDB] SET  MULTI_USER 
GO
ALTER DATABASE [newDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [newDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [newDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [newDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [newDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [newDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'newDB', N'ON'
GO
ALTER DATABASE [newDB] SET QUERY_STORE = OFF
GO
USE [newDB]
GO
/****** Object:  Table [dbo].[TblMachine]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMachine](
	[MachineID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](255) NULL,
	[MachineStatus] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblUser]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TblMachine] ON 

INSERT [dbo].[TblMachine] ([MachineID], [MachineName], [MachineStatus]) VALUES (1, N'Machine1', 2)
SET IDENTITY_INSERT [dbo].[TblMachine] OFF
GO
SET IDENTITY_INSERT [dbo].[TblUser] ON 

INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (1, N'tttt@gmail.com', N'ttt123')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (2, N'johncena@gmail.com', N'cena123')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (3, N'hihi@gmail.com', N'hello123123')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (4, N'leavgue@gmail.com', N'lolololol12345')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (5, N'kelvin@gmail.com', N'Calvin123')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (8, N'hihi@gmail.com', N'asdasd123123')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (6, N'tttt@gmail.com', N'123132')
INSERT [dbo].[TblUser] ([ID], [Email], [Password]) VALUES (7, N'asd@asd', N'asd123123')
SET IDENTITY_INSERT [dbo].[TblUser] OFF
GO
/****** Object:  StoredProcedure [dbo].[addMachine]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addMachine]
	(@mName Varchar(255))
AS
BEGIN
	INSERT INTO TblMachine (MachineName, MachineStatus)
	VALUES (@mName, FLOOR(RAND()*(2-1+1))+1)
END
GO
/****** Object:  StoredProcedure [dbo].[checkUser]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[checkUser]
	(@userEmail Varchar(30), @userPassword Varchar(30))
AS
BEGIN
	SELECT COUNT(*) FROM [newDB].[dbo].[TblUser] 
	WHERE Email= @userEmail AND Password= @userPassword
END
GO
/****** Object:  StoredProcedure [dbo].[createUser]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createUser]
	(@userEmail Varchar(30), @userPassword Varchar(30))
AS
BEGIN
	INSERT INTO TblUser (Email, Password)
	VALUES (@userEmail, @userPassword)
END
GO
/****** Object:  StoredProcedure [dbo].[deleteUser]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[deleteUser]
	(@userID INT)
AS
BEGIN
	DELETE FROM TblUser WHERE ID = @userID;
END
GO
/****** Object:  StoredProcedure [dbo].[editUser]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[editUser]
	(@userEmail VARCHAR(255), @userPassword VARCHAR (255), @userID INT)
AS
BEGIN
	UPDATE TblUser
	SET Email = @userEmail,
	Password = @userPassword
	WHERE ID = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[getAllUsers]    Script Date: 09/08/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getAllUsers]
AS 
SELECT * FROM [newDB].[dbo].[TblUser]
GO
USE [master]
GO
ALTER DATABASE [newDB] SET  READ_WRITE 
GO
