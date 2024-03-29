USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_GetTasks_GET]    Script Date: 7/3/2023 2:27:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[su_GetTasks_GET]
 @iUserId INT,
 @iPermissionLevelId INT
AS
BEGIN
	IF @iPermissionLevelId IN (1,4)
	BEGIN
		select *
		from tbl_su_Task
	END
	ELSE IF @iPermissionLevelId=2
	BEGIN
	DECLARE @branch INT
		SELECT @branch = [iBranchId]
		FROM tbl_sem_TeamPerson 
		WHERE [iPersonId] = @iUserId;

		SELECT task.*
		FROM tbl_su_Task as task left join tbl_targets as targets on task.iTargetId=targets.iTargetId
		WHERE targets.iBranchId = @branch
	END
END