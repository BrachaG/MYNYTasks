create procedure su_AddStatus_INS
@iUserId INT,
@iPermissionId INT,
@nvStatusName nvarchar(50)
AS
BEGIN
if @iPermissionId in (1,4)
insert [dbo].[Target_Status] ([nvTargetStatusName] ,[iLastUpdateUserId],[dtLastUpdateDate],[iSysRowStatus])
values (@nvStatusName,@iUserId,GETDATE(),1)      
END