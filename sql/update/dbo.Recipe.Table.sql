USE [Medical]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 24.05.2022 14:14:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCard_information] [int] NOT NULL,
	[Drug] [nvarchar](1000) NOT NULL,
	[Dosage] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [FK_Recipe_Card_information] FOREIGN KEY([idCard_information])
REFERENCES [dbo].[Card_information] ([id])
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [FK_Recipe_Card_information]
GO
