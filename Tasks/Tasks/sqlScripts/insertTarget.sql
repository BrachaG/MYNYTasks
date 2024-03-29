USE [NefeshYehudi]
GO
/****** Object: StoredProcedure [dbo].[su_Insert_Target] Script Date: 05/06/2023 11:16:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[su_Insert_Target] 
	-- Add the parameters for the stored procedure here
	@Comment nvarchar(MAX),
	@typeTargetId int,
	@Ids [dbo].[PersonIds] READONLY,
	@BranchId int,
	@TargetDate date,
	@CreatorId int

AS
BEGIN
	INSERT INTO tbl_targets ([nvComment], [iTypeTargetId], [iPersonId], [iCreatorId], [dtCreation], [iBranchId], [dtTargetDate], [iTargetStatusId])
	SELECT @Comment, @typeTargetId, i.Id, @CreatorId, GETDATE(), @BranchId, @TargetDate, 1
	FROM @Ids i

END
