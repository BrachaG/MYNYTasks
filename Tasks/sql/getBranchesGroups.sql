CREATE PROCEDURE su_GetBranchesGruops_SLCT
AS
BEGIN
    SELECT G.iGroupId, G.nvGroupName
    FROM dbo.tbl_su_Groups G;
    SELECT G.iGroupId, BG.iBranchId, Br.nvBranchName
    FROM dbo.tbl_su_Groups G
    INNER JOIN dbo.tbl_su_BranchesGroups BG ON G.iGroupId = BG.iGroupId
    INNER JOIN dbo.tbl_my_Branches Br ON BG.iBranchId = Br.iBranchId;
END

