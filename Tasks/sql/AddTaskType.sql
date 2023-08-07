create procedure su_AddTaskType_INS
@nvTypeName  nvarchar(50),
@iPermissionLevelId INT
AS
BEGIN
	IF @iPermissionLevelId IN (1,4)
	INSERT [dbo].[tbl_su_TaskType] ([nvType])
	VALUES (@nvTypeName)
END
