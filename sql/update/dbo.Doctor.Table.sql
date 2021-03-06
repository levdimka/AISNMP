USE [Medical]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 03.06.2022 16:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Num_document] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sourname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Number_of_telephone] [bigint] NOT NULL,
	[Date_of_birth] [date] NOT NULL,
	[IdSpecialization] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Doctor_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (1, 101, N'Глеб', N'Ольшанськ', N'Демидович', N'dima26@gmail.com', 380976574783, CAST(N'1969-04-25' AS Date), 2, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (2, 102, N'Олена', N'Стефанюк', N'Олексіївна', N'dima26@gmail.com', 380838282828, CAST(N'2021-07-15' AS Date), 1, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (3, 103, N'Дмитро', N'Овчиников', N'Александрович', N'lev2000.dima@gmail.com', 976374760, CAST(N'2022-02-08' AS Date), 1, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (4, 104, N'Дмитро', N'Логинов', N'Васильувич', N'Lev75.eduard@gmail.com', 1234567, CAST(N'2021-10-07' AS Date), 4, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (5, 105, N'Иван', N'Потап', N'Иванович', N'dima26@gmail.com', 56756565, CAST(N'2022-03-03' AS Date), 2, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (6, 106, N'Александр', N'Жуков', N'Васильевич', N'dima26@gmail.com', 34232323, CAST(N'2021-08-05' AS Date), 4, N'1234')
INSERT [dbo].[Doctor] ([id], [Num_document], [Name], [Sourname], [Patronymic], [Email], [Number_of_telephone], [Date_of_birth], [IdSpecialization], [Password]) VALUES (7, 107, N'Эдуард', N'Важинский', N'Васильевич', N'dima26@gmail.com', 132131231, CAST(N'2021-07-29' AS Date), 3, N'1234')
SET IDENTITY_INSERT [dbo].[Doctor] OFF
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK_Doctor__Specialization_] FOREIGN KEY([IdSpecialization])
REFERENCES [dbo].[Specialization] ([id])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK_Doctor__Specialization_]
GO
