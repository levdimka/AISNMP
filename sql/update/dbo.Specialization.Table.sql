USE [Medical]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Post_Specialization] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Specialization_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Specialization] ON 

INSERT [dbo].[Specialization] ([id], [Post_Specialization]) VALUES (1, N'Лор')
INSERT [dbo].[Specialization] ([id], [Post_Specialization]) VALUES (2, N'Окуліст')
INSERT [dbo].[Specialization] ([id], [Post_Specialization]) VALUES (3, N'Хірург')
INSERT [dbo].[Specialization] ([id], [Post_Specialization]) VALUES (4, N'Терапевт')
SET IDENTITY_INSERT [dbo].[Specialization] OFF
GO
