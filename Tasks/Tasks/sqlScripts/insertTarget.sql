USE [NefeshYehudi]
GO

ALTER PROCEDURE Insert_Target 
	-- Add the parameters for the stored procedure here
	@Comment nvarchar(MAX),
	@TargetId int,
	@PersonId int
AS
BEGIN
	DECLARE @BranchId INT
	SELECT @BranchId = [iBranchId] 
	FROM [dbo].[tbl_sem_TeamPerson]
	WHERE ipersonId = @PersonId	
	INSERT INTO tbl_targets ([nvComment], [iTargetId], [iPersonId], [iBranchId])
	VALUES (@Comment, @TargetId, @PersonId, @BranchId)
END
GO
