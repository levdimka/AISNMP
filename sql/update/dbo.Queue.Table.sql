USE [Medical]
GO
/****** Object:  Table [dbo].[Queue]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Queue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[idDoctor] [int] NOT NULL,
	[idPacient] [int] NOT NULL,
	[idVisit] [int] NULL,
	[idHome] [int] NULL,
	[Home_address] [nvarchar](50) NULL,
	[Closed] [datetime] NULL,
	[Note] [nvarchar](50) NULL,
 CONSTRAINT [PK_Queue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Doctor] FOREIGN KEY([idDoctor])
REFERENCES [dbo].[Doctor] ([id])
GO
ALTER TABLE [dbo].[Queue] CHECK CONSTRAINT [FK_Queue_Doctor]
GO
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Home] FOREIGN KEY([idHome])
REFERENCES [dbo].[Home] ([id])
GO
ALTER TABLE [dbo].[Queue] CHECK CONSTRAINT [FK_Queue_Home]
GO
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Pacient_] FOREIGN KEY([idPacient])
REFERENCES [dbo].[Pacient] ([id])
GO
ALTER TABLE [dbo].[Queue] CHECK CONSTRAINT [FK_Queue_Pacient_]
GO
ALTER TABLE [dbo].[Queue]  WITH CHECK ADD  CONSTRAINT [FK_Queue_Visit] FOREIGN KEY([idVisit])
REFERENCES [dbo].[Visit] ([id])
GO
ALTER TABLE [dbo].[Queue] CHECK CONSTRAINT [FK_Queue_Visit]
GO
