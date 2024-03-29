USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_CreateBranchesGroup_INS]    Script Date: 7/25/2023 6:57:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[su_CreateBranchesGroup_INS]
    @iPermissionId INT,
    @GroupName NVARCHAR(50),
    @Branches dbo.BranchesIds READONLY
AS
BEGIN
    IF @iPermissionId IN (1, 4)
   BEGIN
        DECLARE @GroupId TABLE (GroupId INT);

        INSERT INTO dbo.tbl_su_Groups (nvGroupName)
        OUTPUT inserted.iGroupId INTO @GroupId
        VALUES (@GroupName); 

        INSERT INTO dbo.tbl_su_BranchesGroups (iBranchId, iGroupId)
        SELECT [BranchesId], GroupId
        FROM @Branches
        CROSS JOIN @GroupId;
		DELETE FROM  @GroupId
    END
END
