create procedure su_GetTaskByTargetId
@iTargetId INT
AS
BEGIN
select *
from tbl_su_Task
where iTargetId=@iTargetId
END