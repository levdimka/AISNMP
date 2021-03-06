USE [Medical]
GO
/****** Object:  Table [dbo].[Pacient]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sourname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Date_of_birth] [date] NOT NULL,
	[Adress] [nvarchar](50) NOT NULL,
	[Number_of_telephone] [bigint] NOT NULL,
	[Card_number] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Pacient_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Pacient] ON 

INSERT [dbo].[Pacient] ([id], [Name], [Sourname], [Patronymic], [Email], [Date_of_birth], [Adress], [Number_of_telephone], [Card_number], [Password]) VALUES (13, N'Дмитро', N'Кочетко', N'Сергійович', N'koch123@gmail.com', CAST(N'2000-05-04' AS Date), N'Космонавтів', 380976543456, 1234, N'1234')
INSERT [dbo].[Pacient] ([id], [Name], [Sourname], [Patronymic], [Email], [Date_of_birth], [Adress], [Number_of_telephone], [Card_number], [Password]) VALUES (14, N'Едуард', N'Левченко', N'Олексійович', N'eduard@gmail.com', CAST(N'1975-06-07' AS Date), N'Бульвар вечірній', 380987646789, 1267, N'1234')
INSERT [dbo].[Pacient] ([id], [Name], [Sourname], [Patronymic], [Email], [Date_of_birth], [Adress], [Number_of_telephone], [Card_number], [Password]) VALUES (15, N'Оксана', N'Логінова', N'Степаніївна', N'heart@3443', CAST(N'1986-04-03' AS Date), N'Зарічна 24', 380765478976, 5675, N'1234')
SET IDENTITY_INSERT [dbo].[Pacient] OFF
GO
ALTER TABLE [dbo].[Pacient]  WITH CHECK ADD  CONSTRAINT [FK_Pacient_Card] FOREIGN KEY([Card_number])
REFERENCES [dbo].[Card] ([Card_number])
GO
ALTER TABLE [dbo].[Pacient] CHECK CONSTRAINT [FK_Pacient_Card]
GO
