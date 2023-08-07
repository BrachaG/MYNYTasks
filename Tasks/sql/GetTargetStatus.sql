create procedure su_GetTagetStatus
AS
BEGIN
select iTargetStatusId,nvTargetStatusName
from [dbo].[Target_Status]
END