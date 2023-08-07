create procedure su_AddTargetType_INS
@nvTargetName nvarchar(50),
@iUserId INT,
@iPermissionLevel INT
AS
BEGIN
if @iPermissionLevel in (1,4)
insert [dbo].[tbl_code_targets] ([nvTargetName],[iLastUpdateUserId],[dtLastUpdateDate],[istsRowStatus])
values (@nvTargetName,@iUserId,GETDATE(),1)
END