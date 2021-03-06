USE [Medical]
GO
/****** Object:  Table [dbo].[Card_information]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card_information](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Card_number] [int] NOT NULL,
	[Date_of_receipt] [datetime] NOT NULL,
	[Medical_board] [bit] NULL,
	[Complaints] [nvarchar](1000) NULL,
	[Diagnosis] [nvarchar](1000) NULL,
	[Recipe] [nvarchar](1000) NULL,
	[idDoctor] [int] NOT NULL,
	[Closed] [datetime] NULL,
 CONSTRAINT [PK_Card_information] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Card_information]  WITH CHECK ADD  CONSTRAINT [FK_Card_information_Card] FOREIGN KEY([Card_number])
REFERENCES [dbo].[Card] ([Card_number])
GO
ALTER TABLE [dbo].[Card_information] CHECK CONSTRAINT [FK_Card_information_Card]
GO
ALTER TABLE [dbo].[Card_information]  WITH CHECK ADD  CONSTRAINT [FK_Card_information_Doctor] FOREIGN KEY([idDoctor])
REFERENCES [dbo].[Doctor] ([id])
GO
ALTER TABLE [dbo].[Card_information] CHECK CONSTRAINT [FK_Card_information_Doctor]
GO
