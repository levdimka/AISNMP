USE [Medical]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[Card_number] [int] NOT NULL,
	[Start_data] [datetime] NOT NULL,
	[Stop_data] [datetime] NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[Card_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Card] ([Card_number], [Start_data], [Stop_data]) VALUES (1221, CAST(N'2020-04-10T00:00:00.000' AS DateTime), CAST(N'2021-04-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Card] ([Card_number], [Start_data], [Stop_data]) VALUES (1234, CAST(N'2019-03-02T00:00:00.000' AS DateTime), CAST(N'2020-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Card] ([Card_number], [Start_data], [Stop_data]) VALUES (1267, CAST(N'2021-09-02T00:00:00.000' AS DateTime), CAST(N'2021-08-26T00:00:00.000' AS DateTime))
INSERT [dbo].[Card] ([Card_number], [Start_data], [Stop_data]) VALUES (5675, CAST(N'2022-02-03T00:00:00.000' AS DateTime), CAST(N'2022-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Card] ([Card_number], [Start_data], [Stop_data]) VALUES (123444, CAST(N'2022-05-20T17:15:28.137' AS DateTime), NULL)
GO
