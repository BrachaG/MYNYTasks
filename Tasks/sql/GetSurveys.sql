USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_GetSurveys_SLCT]    Script Date: 6/29/2023 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[su_GetSurveys_SLCT]
@iPermissionLevelId int,
@iBranchId int
AS
if @iPermissionLevelId in (1,4)
	select  iSurveyId, nvSurveyName,dtEndSurveyDate,  nvDescription, iReprisalType, iSumReprisal,nvFinalMessage, nvLink,iStatusType,
	bFirstName, bLastName, bTz,bBranchId, bEmail, bPhone, bGender, t2.iQuestionCount, t3.iRespondentsCount
	from dbo.tbl_su_Survey t1
	cross apply(select count(q1.iQuestionId) iQuestionCount
				from tbl_su_Questions q1
				inner join tbl_su_Pages q2 on q2.iPageId=q1.iPageId
				where q2.iSurveyId=t1.iSurveyId)t2
            
				cross apply(select count(iSurveyStudentId) iRespondentsCount
				from dbo.tbl_su_SurveyStudent 
				where iSurveyId=t1.iSurveyId)t3
            
	where iStatusType=1 and iSysRowStatus=1
	order by dtEndSurveyDate desc
else if @iPermissionLevelId=2
	select  iSurveyId, nvSurveyName,dtEndSurveyDate,  nvDescription, iReprisalType, iSumReprisal,nvFinalMessage, nvLink,iStatusType,
	bFirstName, bLastName, bTz,bBranchId, bEmail, bPhone, bGender, t2.iQuestionCount, t3.iRespondentsCount
	from dbo.tbl_su_Survey t1
	cross apply(select count(q1.iQuestionId) iQuestionCount
				from tbl_su_Questions q1
				inner join tbl_su_Pages q2 on q2.iPageId=q1.iPageId
				where q2.iSurveyId=t1.iSurveyId)t2
            
				cross apply(select count(iSurveyStudentId) iRespondentsCount
				from dbo.tbl_su_SurveyStudent 
				where iSurveyId=t1.iSurveyId and iBranchId=@iBranchId)t3
            
	where iStatusType=1 and iSysRowStatus=1
	order by dtEndSurveyDate desc