USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[Insert_Target]    Script Date: 10/05/2023 20:43:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Insert_Target] 
	-- Add the parameters for the stored procedure here
	@Comment nvarchar(MAX),
	@TargetId int,
	@PersonId int,
	@TargetDate date
AS
BEGIN
	DECLARE @BranchId INT
	SELECT @BranchId = [iBranchId] 
	FROM [dbo].[tbl_sem_TeamPerson]
	WHERE ipersonId = @PersonId	
	INSERT INTO tbl_targets ([nvComment], [iTargetId], [iPersonId], 
	[iBranchId],[dtTargetDate] ,[iStatusId])
	VALUES (@Comment, @TargetId, @PersonId,@BranchId,@TargetDate,1)
END
