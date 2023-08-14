create procedure su_GetAllTaskTypes
AS
BEGIN
	select iTypeId, nvType
	from tbl_su_TaskType
END