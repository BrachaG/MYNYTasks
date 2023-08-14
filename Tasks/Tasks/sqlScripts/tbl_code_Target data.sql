USE [NefeshYehudi]
GO
SET IDENTITY_INSERT [dbo].[tbl_code_Targets] ON 

INSERT [dbo].[tbl_code_Targets] ([iTypeTargetId], [nvTypeTargetName], [iLastUpdateUserId], [dtLastUpdateDate], [istsRowStatus]) VALUES (1, N'שמירת שבת', 2, CAST(N'2023-06-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[tbl_code_Targets] ([iTypeTargetId], [nvTypeTargetName], [iLastUpdateUserId], [dtLastUpdateDate], [istsRowStatus]) VALUES (2, N'צדקה', 2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[tbl_code_Targets] ([iTypeTargetId], [nvTypeTargetName], [iLastUpdateUserId], [dtLastUpdateDate], [istsRowStatus]) VALUES (3, N'תפילה', 2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tbl_code_Targets] OFF
GO
