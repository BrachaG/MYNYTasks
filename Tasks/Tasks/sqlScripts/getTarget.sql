USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[Get_Targets]    Script Date: 08/05/2023 12:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Targets]
	@Id int = null,
	@Status int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@Status = 1)
	BEGIN
		SELECT *
		FROM tbl_targets
	END

	ELSE IF (@Status = 2)
	BEGIN
		
		DECLARE @branch INT;

		SELECT @branch = [iBranchId]
		FROM tbl_sem_TeamPerson 
		WHERE [iPersonId] = @Id;

		SELECT*
		FROM tbl_targets
		WHERE iBranchId = @branch
	END
	ELSE
	SELECT*
	FROM tbl_targets
	WHERE [iPersonId] = @Id; 
END
