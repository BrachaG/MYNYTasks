USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_InsertNewTask_INS]    Script Date: 6/5/2023 10:10:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[su_InsertNewTask_INS]
	@iPermissionLevelId INT,
	@iTargetId INT,
	@iType INT,
	@nvCategory NVARCHAR(50),
	@dtEndDate DATETIME,
	@iStatus INT,
	@iStudentId INT = null,
	@nvOrigin NVARCHAR(50),
	@nvComments NVARCHAR(MAX) = null
AS
BEGIN
	IF @iPermissionLevelId IN (1,2,4)
	DECLARE @date DateTime
		SELECT @date = dtTargetDate
		FROM tbl_targets 
		WHERE iId = @iTargetId
	IF @dtEndDate BETWEEN GETDATE() AND @date AND @dtEndDate <= DATEADD(DAY, 30, GETDATE())

	BEGIN
		INSERT [dbo].[tbl_su_Task] ([iTargetId], [iType], [nvCategory], [dtEndDate], [iStatus], [iStudentId], [nvOrigin], [nvComments])
		VALUES (@iTargetId, @iType, @nvCategory, @dtEndDate, @iStatus, @iStudentId, @nvOrigin, @nvComments)
	END
END
