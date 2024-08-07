USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_Get_Targets]    Script Date: 05/06/2023 11:30:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[su_Get_Targets]
	@Id int = null,
	@PermissionLevelId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@PermissionLevelId = 1 or @PermissionLevelId = 4)
	BEGIN
	    SELECT t.*,b.nvBranchName,u.nvUserName
		FROM tbl_targets t
		left join [dbo].[tbl_my_Branches] b on t.iBranchId=b.iBranchId
		left join [dbo].[tbl_sys_Users] u on t.iPersonId=u.nPratUserId
	END

	ELSE IF (@PermissionLevelId = 2)
	BEGIN
		DECLARE @branch INT;

		SELECT @branch = [iBranchId]
		FROM tbl_sem_TeamPerson 
		WHERE [iPersonId] =@Id

		SELECT t.*,b.nvBranchName,u.nvUserName
		FROM tbl_targets t
		left join [dbo].[tbl_my_Branches] b on t.iBranchId=b.iBranchId
		left join [dbo].[tbl_sys_Users] u on t.iPersonId=u.nPratUserId
		WHERE t.iBranchId = @branch 
		
	END
END
