USE [NefeshYehudi]
GO
INSERT [dbo].[Target_Status] ([iTargetStatusId], [nvTargetStatusName], [iLastUpdateUserId], [dtLastUpdateDate], [iSysRowStatus]) VALUES (1, N'חדש', 1, CAST(N'2023-05-03T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Target_Status] ([iTargetStatusId], [nvTargetStatusName], [iLastUpdateUserId], [dtLastUpdateDate], [iSysRowStatus]) VALUES (2, N'בטיפול', 1, CAST(N'2023-02-02T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Target_Status] ([iTargetStatusId], [nvTargetStatusName], [iLastUpdateUserId], [dtLastUpdateDate], [iSysRowStatus]) VALUES (3, N'הושג', 2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Target_Status] ([iTargetStatusId], [nvTargetStatusName], [iLastUpdateUserId], [dtLastUpdateDate], [iSysRowStatus]) VALUES (4, N'הושג חלקית', 2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Target_Status] ([iTargetStatusId], [nvTargetStatusName], [iLastUpdateUserId], [dtLastUpdateDate], [iSysRowStatus]) VALUES (5, N'לא הושג', 2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1)
GO
