USE [MYNY_QANew]
GO

/****** Object:  StoredProcedure [dbo].[su_GetStudentForTask_SLCT]    Script Date: 19/09/2023 15:51:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






 CREATE PROCEDURE [dbo].[su_GetStudentForTask_SLCT]
 @iPermissionLevelId int=null,
 @iBranchId int =null,
 @iUserId int =null
AS
IF @iPermissionLevelId IN (1,4)
	BEGIN
	SELECT p.nvFirstName+' ' +p.nvLastName as FullName, 
cb.[iPersonId]
      ,cb.[iBranchId]
	  ,b.[nvBranchName]
  FROM [MYNY_QANew].[dbo].[tbl_my_CustomerInBranch] as cb
  JOIN [tbl_my_Branches] as b on cb.iBranchId = b.[iBranchId]
  JOIN [tbl_my_Persons] as p on p.iPersonId =cb.iPersonId
  where cb.[iBranchId]=@iBranchId 
	END
	ELSE IF @iPermissionLevelId=2
	BEGIN
	DECLARE @gender INT;


SELECT @gender =[iGenderId]
FROM [MYNY_QANew].[dbo].[tbl_my_Persons]
where iPersonId = @iUserId

			SELECT p.nvFirstName+' ' +p.nvLastName as FullName, 
cb.[iPersonId]
      ,cb.[iBranchId]
	  ,b.[nvBranchName]
  FROM [MYNY_QANew].[dbo].[tbl_my_CustomerInBranch] as cb
  JOIN [tbl_my_Branches] as b on cb.iBranchId = b.[iBranchId]
  JOIN [tbl_my_Persons] as p on p.iPersonId =cb.iPersonId
  where cb.[iBranchId]=@iBranchId and p.iGenderId= @gender
	end
GO

