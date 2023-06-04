USE[NefeshYehudi]
CREATE TABLE tbl_code_Targets (
  itypeTargetId INT PRIMARY KEY IDENTITY(1,1),
  nvTargetName NVARCHAR(50),
  iLastUpdateUserId INT,
  dtLastUpdateDate DATETIME,
  istsRowStatus BIT
);



