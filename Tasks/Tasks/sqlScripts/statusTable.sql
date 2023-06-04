CREATE TABLE Target_Status (
     iTargetStatusId INT PRIMARY KEY NOT NULL,
     nvStatusName NVARCHAR(50) NOT NULL,
	 iLastUpdateUserId INT NOT NULL,
     dtLastUpdateDate DATETIME NOT NULL,
     iSysRowStatus BIT NOT NULL
)
