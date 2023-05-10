CREATE TABLE tbl_targets(
  iId INT PRIMARY KEY IDENTITY(1,1),
  nvComment NVARCHAR(MAX),
  iTargetId INT FOREIGN KEY REFERENCES tbl_code_targets(iTargetId) NOT NULL,
  iPersonId INT NOT NULL,
  iBranchId INT NOT NULL,
  dtTargetDate DATE
  CHECK (dtTargetDate > GETDATE()),
  iStatusId INT NOT NULL FOREIGN KEY REFERENCES Target_Status(iStatusId)

)