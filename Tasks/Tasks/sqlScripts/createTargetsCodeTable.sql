USE[NefeshYehudi]
CREATE TABLE tbl_code_Targets (
  iTypeTargetId INT PRIMARY KEY IDENTITY(1,1),
  nvTypeTargetName NVARCHAR(50),
  iLastUpdateUserId INT,
  dtLastUpdateDate DATETIME,
  istsRowStatus BIT
);



