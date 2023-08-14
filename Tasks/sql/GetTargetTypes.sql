create procedure su_GetTargetType
AS
BEGIN
select itypeTargetId, nvTargetName
from [dbo].[tbl_code_targets]
END
