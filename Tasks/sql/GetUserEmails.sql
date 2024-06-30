create procedure su_GetUserEmails_SLCT
@Ids [dbo].[PersonIds] READONLY
AS
BEGIN
select nvUserMail
from [dbo].[tbl_sys_Users] s
right join @Ids i on s.nPratUserId=i.Id
END