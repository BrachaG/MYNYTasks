USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_InsertNewTask_INS]    Script Date: 5/18/2023 6:33:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[su_InsertNewTask_INS]
    @iUserId INT,
	@iDestinationId INT,
	@iType INT,
	@nvCategory NVARCHAR(50),
	@dtEndDate DATETIME,
	@iStatus INT,
	@iStudentId INT = null,
	@nvOrigin NVARCHAR(50),
	@nvComments NVARCHAR(MAX)= null
AS
BEGIN
	INSERT [dbo].[tbl_su_Task] ([iDestinationId], [iType], [nvCategory], [dtEndDate], [iStatus], [iStudentId], [nvOrigin], [nvComments] ) VALUES(@iDestinationId, @iType, @nvCategory, @dtEndDate, @iStatus, @iStudentId, @nvOrigin, @nvComments)
END