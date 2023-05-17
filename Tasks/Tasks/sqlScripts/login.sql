USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[PRG_sys_User_SLCT]    Script Date: 17/05/2023 11:35:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PRG_sys_UserLogin_SLCT]
	@nvUserName nvarchar(50) ,
	@nvPassword nvarchar(50),
	@iSysRowStatus int =1,
	@nPratUserId int=null
AS

BEGIN

SET NOCOUNT ON

  if @nPratUserId is null 
  begin
	select @nPratUserId=t1.nPratUserId
	from tbl_sys_Users t1 
		
	WHERE	(t1.nvUserName = @nvUserName AND @nvUserName<>'') and
			(t1.nvPassword = @nvPassword AND @nvPassword<>'') and
			(t1.iSysRowStatus = @iSysRowStatus or @iSysRowStatus is null )
		
  end


SELECT	t1.nPratUserId,
		t1.nvUserName,	
		t1.iPermissionLevelId,
		t1.iContinueContactPermissionId,
		t1.iBranchId,
		iActivityPermissionId,
		t1.nPratUserId as iUserId
FROM	tbl_sys_Users t1 
left join tbl_my_Branches t2 on t2.iBranchId=t1.iBranchId
	
WHERE	(t1.iSysRowStatus = @iSysRowStatus or @iSysRowStatus is null )
		and (nPratUserId=@nPratUserId )

select t2.iBranchId,t2.nvBranchName from
 tbl_sys_Users_Branches t1
inner join 
tbl_my_Branches t2 on t2.iBranchId=t1.iBranchId and iUserId=@nPratUserId
where t1.iSysRowStatus=1 and t2.iSysRowStatus=1

union
select t4.iBranchId,t4.nvBranchName
from 
 tbl_sys_Users t3
inner join 
tbl_my_Branches t4 on t3.iBranchId=t4.iBranchId and t3.nPratUserId=@nPratUserId

where t3.iSysRowStatus=1 and t4.iSysRowStatus=1

END

