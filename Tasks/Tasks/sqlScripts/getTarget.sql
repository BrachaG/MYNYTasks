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
		SELECT *
		FROM tbl_targets
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
