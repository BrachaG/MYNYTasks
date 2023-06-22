CREATE PROCEDURE su_CreateBranchesGroup_INS
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
    END
END
