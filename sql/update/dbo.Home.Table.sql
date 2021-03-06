USE [Medical]
GO
/****** Object:  Table [dbo].[Home]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Home](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Period] [nvarchar](20) NOT NULL,
	[Start_time] [time](7) NOT NULL,
	[Stop_time] [time](7) NOT NULL,
 CONSTRAINT [PK_Home] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Home] ON 

INSERT [dbo].[Home] ([id], [Period], [Start_time], [Stop_time]) VALUES (1, N'14.00-14.29', CAST(N'14:00:00' AS Time), CAST(N'14:29:00' AS Time))
INSERT [dbo].[Home] ([id], [Period], [Start_time], [Stop_time]) VALUES (2, N'14.30-14.59', CAST(N'14:30:00' AS Time), CAST(N'14:59:00' AS Time))
INSERT [dbo].[Home] ([id], [Period], [Start_time], [Stop_time]) VALUES (3, N'15.00-15.29', CAST(N'15:00:00' AS Time), CAST(N'15:29:00' AS Time))
INSERT [dbo].[Home] ([id], [Period], [Start_time], [Stop_time]) VALUES (4, N'15.30-15.59', CAST(N'15:30:00' AS Time), CAST(N'15:59:00' AS Time))
SET IDENTITY_INSERT [dbo].[Home] OFF
GO
