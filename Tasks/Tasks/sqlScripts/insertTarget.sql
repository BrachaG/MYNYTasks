USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[Insert_Target]    Script Date: 22/05/2023 10:45:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[su_Insert_Target] 
	-- Add the parameters for the stored procedure here
	@Comment nvarchar(MAX),
	@TargetId int,
	@Ids [dbo].[PersonIds] READONLY,
	@TargetDate date
AS
BEGIN
	INSERT INTO tbl_targets ([nvComment], [iTargetId], [iPersonId], 
	[iBranchId],[dtTargetDate] ,[iStatusId])

	SELECT @Comment, @TargetId,i.Id ,p.[iBranchId],@TargetDate,1
	from @Ids i
	left join [dbo].[tbl_sem_TeamPerson] p
	on p.ipersonId= i.Id

END
