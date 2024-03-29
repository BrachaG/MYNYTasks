USE [NefeshYehudi]
GO
/****** Object:  StoredProcedure [dbo].[su_GetResultsForSurvey_SLCT]    Script Date: 6/29/2023 11:56:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[su_GetResultsForSurvey_SLCT]--14
 @iSurveyId int =null,
 @iPermissionLevelId int=null,
 @iBranchId int =null
AS
if @iPermissionLevelId in (1,4)
	begin
	select dtCreateDate,
			nvLastName+' '+ nvFirstName nvFullName, 
			case when iGenderId=1 then 'זכר' else 'נקבה' end nvGender,
			nvMobileNumber, 
			nvEmail,
			t5.nvProgramName ,
			nvBranchName,
			iStudentId,
			t1.iBranchId 
	 from dbo.tbl_su_SurveyStudent t1
	 inner join tbl_my_Persons t2 on t1.iStudentId = t2.iPersonId
	 inner join tbl_my_Branches t3 on t3.iBranchId = t1.iBranchId
	 inner join tbl_my_CustomerInProgram t4 on t4.iPersonId=t1.iStudentId and t1.iBranchId=t4.iBranchId
	 inner join tbl_my_Program t5 on t5.iProgramId = t4.iProgramId
	  where iSurveyId=@iSurveyId  and t1.iSysRowStatus=1
	 --@iSurveyId
	select nvQuestionText, iQuestionId, t3.nvQuestionTypeName
	from dbo.tbl_su_Questions t1
	inner join dbo.tbl_su_Pages t2 on t1.iPageId=t2.iPageId left join tbl_code_QuestionType t3 on t1.iQuestionType=t3.iQuestionTypeId
		where iSurveyId = @iSurveyId
	 order by iNumQuestion, iQuestionId

	  select  iStudentId, iBranchId ,left(nvAnswer,len(nvAnswer)-1)nvAnswer, iQuestionId
	  from dbo.tbl_su_SurveyStudent t1
	  inner join dbo.tbl_su_Pages t2  on t1.iSurveyId=t2.iSurveyId
	  inner join dbo.tbl_su_Questions t3 on t3.iPageId=t2.iPageId
	  CROSS APPLY (SELECT isnull(q2.nvAnswerName,q1.nvAnswerText)+ ', ' 
				   FROM tbl_su_AnswerFromStudent q1
				   left join tbl_su_Answers q2 on q2.iAnswerId=q1.iAnswerId
				   WHERE   q1.iQuestionId=t3.iQuestionId and q1.iSurveyStudentId=t1.iSurveyStudentId 

				   order by q1.iAnswerId
				   FOR XML PATH(''))t4 ( nvAnswer )
	  where nvAnswer IS NOT NULL and t1.iSurveyId= @iSurveyId and t1.iSysRowStatus=1--@iSurveyId 
	  order by iNumQuestion, iQuestionId
	--options
	select t3.iQuestionId,iAnswerId, nvAnswerName 
	from dbo.tbl_su_Answers t4
	inner join dbo.tbl_su_Questions t3 on t4.iQuestionId = t3.iQuestionId
	inner join dbo.tbl_su_Pages t2 on t2.iPageId = t3.iPageId
	where t2.iSurveyId = @iSurveyId and t4.iSysRowStatus=1
	order by iAnswerId
	end
else if @iPermissionLevelId=2
	begin
			select dtCreateDate,
				nvLastName+' '+ nvFirstName nvFullName, 
				case when iGenderId=1 then 'זכר' else 'נקבה' end nvGender,
				nvMobileNumber, 
				nvEmail,
				t5.nvProgramName ,
				nvBranchName,
				iStudentId,
				t1.iBranchId 
		 from dbo.tbl_su_SurveyStudent t1
		 inner join tbl_my_Persons t2 on t1.iStudentId = t2.iPersonId
		 inner join tbl_my_Branches t3 on t3.iBranchId = t1.iBranchId
		 inner join tbl_my_CustomerInProgram t4 on t4.iPersonId=t1.iStudentId and t1.iBranchId=t4.iBranchId
		 inner join tbl_my_Program t5 on t5.iProgramId = t4.iProgramId
		  where iSurveyId=@iSurveyId  and t1.iSysRowStatus=1 and t1.iBranchId=@iBranchId
		 --@iSurveyId
		select nvQuestionText, iQuestionId, t3.nvQuestionTypeName
		from dbo.tbl_su_Questions t1
		inner join dbo.tbl_su_Pages t2 on t1.iPageId=t2.iPageId left join tbl_code_QuestionType t3 on t1.iQuestionType=t3.iQuestionTypeId
			where iSurveyId = @iSurveyId
		 order by iNumQuestion, iQuestionId

		  select  iStudentId, iBranchId ,left(nvAnswer,len(nvAnswer)-1)nvAnswer, iQuestionId
		  from dbo.tbl_su_SurveyStudent t1
		  inner join dbo.tbl_su_Pages t2  on t1.iSurveyId=t2.iSurveyId
		  inner join dbo.tbl_su_Questions t3 on t3.iPageId=t2.iPageId
		  CROSS APPLY (SELECT isnull(q2.nvAnswerName,q1.nvAnswerText)+ ', ' 
					   FROM tbl_su_AnswerFromStudent q1
					   left join tbl_su_Answers q2 on q2.iAnswerId=q1.iAnswerId
					   WHERE   q1.iQuestionId=t3.iQuestionId and q1.iSurveyStudentId=t1.iSurveyStudentId 

					   order by q1.iAnswerId
					   FOR XML PATH(''))t4 ( nvAnswer )
		  where nvAnswer IS NOT NULL and t1.iSurveyId= @iSurveyId and t1.iSysRowStatus=1 and iBranchId=@iBranchId--@iSurveyId 
		  order by iNumQuestion, iQuestionId

		--options
		select t3.iQuestionId,iAnswerId, nvAnswerName 
		from dbo.tbl_su_Answers t4
		inner join dbo.tbl_su_Questions t3 on t4.iQuestionId = t3.iQuestionId
		inner join dbo.tbl_su_Pages t2 on t2.iPageId = t3.iPageId
		where t2.iSurveyId = @iSurveyId and t4.iSysRowStatus=1
		order by iAnswerId
	end