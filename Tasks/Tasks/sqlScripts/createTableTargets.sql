USE[NefeshYehudi]
CREATE TABLE tbl_code_targets (
  iTargetId INT PRIMARY KEY IDENTITY(1,1),
  nvTargetName NVARCHAR(50),
  iLastUpdateUserId INT,
  dtLastUpdateDate DATETIME,
  istsRowStatus BIT
);

CREATE TABLE tbl_targets(
  iId INT PRIMARY KEY IDENTITY(1,1),
  nvComment NVARCHAR(MAX),
  iTargetId INT FOREIGN KEY REFERENCES tbl_code_targets(iTargetId),
  iPersonId INT,
  iBranchId INT
);

