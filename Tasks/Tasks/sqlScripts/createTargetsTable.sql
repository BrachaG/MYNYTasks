CREATE TABLE tbl_Targets(
  iTargetId INT PRIMARY KEY IDENTITY(1,1),
  nvComment NVARCHAR(MAX),
  iTypeTargetId INT FOREIGN KEY REFERENCES tbl_code_Targets(iTypeTargetId) NOT NULL,
  iPersonId INT NOT NULL,
  iCreatorId INT NOT NULL,
  dtCreation DATETIME NOT NULL,
  iBranchId INT NOT NULL,
  dtTargetDate DATETIME
  CHECK (dtTargetDate > GETDATE()),
  iTargetStatusId INT NOT NULL FOREIGN KEY REFERENCES Target_Status(iTargetStatusId)

)