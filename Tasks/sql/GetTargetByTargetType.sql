USE [NefeshYehudi1]
GO
/****** Object:  StoredProcedure [dbo].[su_Get_Targets]    Script Date: 17/08/2023 19:30:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[su_Get_Targets]
	@Id int = null,
	@PermissionLevelId int = null,
	@TargetType int =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@PermissionLevelId = 1 or @PermissionLevelId = 4)
	BEGIN
		SELECT *
		FROM tbl_targets
		where iTypeTargetId=@TargetType
	END

	ELSE IF (@PermissionLevelId = 2)
	BEGIN
		
		DECLARE @branch INT;

		SELECT @branch = [iBranchId]
		FROM tbl_sem_TeamPerson 
		WHERE [iPersonId] = @Id;

		SELECT*
		FROM tbl_targets
		WHERE iBranchId = @branch 
	END
END
