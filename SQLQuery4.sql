USE [newDB]
GO
/****** Object:  StoredProcedure [dbo].[checkUser]    Script Date: 07/08/2023 10:31:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[checkUser]
	(@userEmail Varchar(30), @userPassword Varchar(30))
AS
BEGIN
	SELECT COUNT(*) FROM [newDB].[dbo].[TblUser] 
	WHERE Email= @userEmail AND Password= @userPassword
END