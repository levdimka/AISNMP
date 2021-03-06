USE [Medical]
GO
/****** Object:  Table [dbo].[Visit]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Period] [nvarchar](20) NOT NULL,
	[Start_time] [time](7) NOT NULL,
	[Stop_time] [time](7) NOT NULL,
 CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Visit] ON 

INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (1, N'8:00 - 8:29', CAST(N'08:00:00' AS Time), CAST(N'08:29:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (2, N'8:30 - 8:59', CAST(N'08:30:00' AS Time), CAST(N'08:59:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (4, N'09.00-09.29', CAST(N'09:00:00' AS Time), CAST(N'09:29:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (5, N'09.30-09.59', CAST(N'09:30:00' AS Time), CAST(N'09:59:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (6, N'10.00-10.29', CAST(N'10:00:00' AS Time), CAST(N'10:29:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (7, N'10.30-10.59', CAST(N'10:30:00' AS Time), CAST(N'10:59:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (8, N'11.00-11.29', CAST(N'11:00:00' AS Time), CAST(N'11:29:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (9, N'11.30-11.59', CAST(N'11:30:00' AS Time), CAST(N'11:59:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (10, N'12.00-12.29', CAST(N'12:00:00' AS Time), CAST(N'12:29:00' AS Time))
INSERT [dbo].[Visit] ([id], [Period], [Start_time], [Stop_time]) VALUES (11, N'12.30-12.59', CAST(N'12:30:00' AS Time), CAST(N'12:59:00' AS Time))
SET IDENTITY_INSERT [dbo].[Visit] OFF
GO
