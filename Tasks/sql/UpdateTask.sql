USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_UpdateTask_UPD]    Script Date: 6/5/2023 10:04:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[su_UpdateTask_UPD]
	@iPermissionLevelId INT,
	@iTaskId INT,
	@iStatusId INT = null,
	@nvComments NVARCHAR(max)=null
AS
BEGIN
IF @iPermissionLevelId IN (1,2,4)
    SET NOCOUNT ON;
    UPDATE tbl_su_Task
    SET
        iStatus = CASE WHEN @iStatusId IS NOT NULL THEN @iStatusId ELSE iStatus END,
        nvComments = CASE WHEN @nvComments IS NOT NULL THEN @nvComments ELSE nvComments END
    WHERE
        iTaskId = @iTaskId;
END