USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_InsertNewTask_INS]    Script Date: 7/3/2023 2:35:01 PM ******/
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
	@iStudentId INT = null,
	@nvOrigin NVARCHAR(50),
	@nvComments NVARCHAR(MAX) = null
AS
BEGIN
	IF @iPermissionLevelId IN (1,2,4)
	DECLARE @date DateTime
		SELECT @date = dtTargetDate
		FROM tbl_targets 
		WHERE iTargetId = @iTargetId
	IF @dtEndDate BETWEEN GETDATE() AND @date AND @dtEndDate <= DATEADD(DAY, 30, GETDATE())

	BEGIN
		INSERT [dbo].[tbl_su_Task] ([iTargetId], [iType], [nvCategory], [dtEndDate], [iStatus], [iStudentId], [nvOrigin], [nvComments])
		VALUES (@iTargetId, @iType, @nvCategory, @dtEndDate, 1, @iStudentId, @nvOrigin, @nvComments)
	END
END
