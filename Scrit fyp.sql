USE [master]
GO
/****** Object:  Database [FYP_MS]    Script Date: 1/14/2024 12:21:25 AM ******/
CREATE DATABASE [FYP_MS] ON  PRIMARY 
( NAME = N'FYP_MS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FYP_MS.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FYP_MS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FYP_MS_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FYP_MS] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FYP_MS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FYP_MS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FYP_MS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FYP_MS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FYP_MS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FYP_MS] SET ARITHABORT OFF 
GO
ALTER DATABASE [FYP_MS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FYP_MS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FYP_MS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FYP_MS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FYP_MS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FYP_MS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FYP_MS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FYP_MS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FYP_MS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FYP_MS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FYP_MS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FYP_MS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FYP_MS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FYP_MS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FYP_MS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FYP_MS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FYP_MS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FYP_MS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FYP_MS] SET  MULTI_USER 
GO
ALTER DATABASE [FYP_MS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FYP_MS] SET DB_CHAINING OFF 
GO
USE [FYP_MS]
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[MeetingId] [int] IDENTITY(1,1) NOT NULL,
	[MeetingType] [nvarchar](50) NOT NULL,
	[ScheduledDate] [datetime] NOT NULL,
	[SupervisorID] [int] NOT NULL,
	[StudentID] [int] NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[MeetingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[MessageText] [nvarchar](1000) NOT NULL,
	[MessageDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectDetails]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDetails](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Skills] [nvarchar](500) NULL,
	[Domain] [nvarchar](500) NULL,
	[Tools] [nvarchar](500) NULL,
	[StudentsAllowed] [nvarchar](50) NULL,
	[SupervisorID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectDetail] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectDocuments]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDocuments](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DocumentName] [nvarchar](500) NOT NULL,
	[DocumentPath] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_ProjectDocuments] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectList]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectList](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[ProjectDescription] [nvarchar](max) NULL,
	[TotalMarks] [int] NULL,
	[StudentsAllowed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
	[SupervisorID] [int] NOT NULL,
	[Score] [int] NOT NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentProjectRequest]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentProjectRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[UserId] [int] NULL,
	[RequestTime] [datetime] NULL,
	[IsApproved] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AssignedTo] [int] NOT NULL,
	[TaskName] [nvarchar](500) NOT NULL,
	[TaskDescription] [nvarchar](500) NOT NULL,
	[DueDate] [datetime] NULL,
	[TaskStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timetables]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timetables](
	[TimetableId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timetable] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Timetables] PRIMARY KEY CLUSTERED 
(
	[TimetableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](500) NULL,
	[LastName] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[UserTypeId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CustomId] [nvarchar](100) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 1/14/2024 12:21:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Meetings] ON 

INSERT [dbo].[Meetings] ([MeetingId], [MeetingType], [ScheduledDate], [SupervisorID], [StudentID]) VALUES (5, N'Online', CAST(N'2023-10-09T00:00:00.000' AS DateTime), 24, 20)
INSERT [dbo].[Meetings] ([MeetingId], [MeetingType], [ScheduledDate], [SupervisorID], [StudentID]) VALUES (6, N'Online', CAST(N'2023-10-09T00:00:00.000' AS DateTime), 24, 20)
SET IDENTITY_INSERT [dbo].[Meetings] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageText], [MessageDate]) VALUES (18, 24, 20, N'Project Assigned.', CAST(N'2023-10-08T21:23:06.640' AS DateTime))
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageText], [MessageDate]) VALUES (19, 24, 20, N'Your result for FYPMS has been uploaded, please check your result dashboard.', CAST(N'2023-10-08T21:24:07.237' AS DateTime))
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageText], [MessageDate]) VALUES (20, 20, 24, N'student 1 requested for the FYPMS project.', CAST(N'2023-10-08T21:25:10.437' AS DateTime))
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageText], [MessageDate]) VALUES (21, 20, 24, N'student 1 requested for the 123 project.', CAST(N'2023-10-08T21:51:16.343' AS DateTime))
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectDetails] ON 

INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (7, N'FYPMS', N'.Net', N'Programming', N'Web Application', N'Visual Studio', N'7', 24, CAST(N'2023-10-08T21:22:08.317' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (8, N'123', N'124', N'1', N'Networking', N'0', N'0', 24, CAST(N'2023-10-08T21:49:17.340' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (9, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:37:54.167' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (10, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:37:57.130' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (11, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:37:58.157' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (12, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:38:23.920' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (13, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:38:24.987' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (14, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:41:09.240' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (15, N'FYP management', N'.Net', N'programming', N'Web Application', N'VS', N'10', 24, CAST(N'2024-01-08T16:43:08.330' AS DateTime))
INSERT [dbo].[ProjectDetails] ([ProjectId], [Title], [Description], [Skills], [Domain], [Tools], [StudentsAllowed], [SupervisorID], [CreatedDate]) VALUES (16, N'Fyp', N'dotnet', N'programming', N'Web Application', N'VS', N'30', 24, CAST(N'2024-01-08T16:52:43.033' AS DateTime))
SET IDENTITY_INSERT [dbo].[ProjectDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectDocuments] ON 

INSERT [dbo].[ProjectDocuments] ([DocumentId], [ProjectId], [DocumentName], [DocumentPath]) VALUES (6, 7, N'50100404417122 (2).pdf', N'D:\Users\dell\Source\Repos\FYPM\Uploads\Bio Data.pdf')
INSERT [dbo].[ProjectDocuments] ([DocumentId], [ProjectId], [DocumentName], [DocumentPath]) VALUES (7, 8, N'50100404417122 (2).pdf', N'D:\Users\dell\Source\Repos\FYPM\Uploads\Bio Data.pdf')
INSERT [dbo].[ProjectDocuments] ([DocumentId], [ProjectId], [DocumentName], [DocumentPath]) VALUES (8, 16, N'Bio Data.pdf', N'D:\Users\dell\Source\Repos\FYPM\Uploads\Bio Data.pdf')
SET IDENTITY_INSERT [dbo].[ProjectDocuments] OFF
GO
SET IDENTITY_INSERT [dbo].[Results] ON 

INSERT [dbo].[Results] ([ResultId], [TaskID], [StudentID], [SupervisorID], [Score]) VALUES (5, 12, 20, 24, 20)
SET IDENTITY_INSERT [dbo].[Results] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (12, 7, 20, N'FYPMS', N'.Net', CAST(N'2023-10-16T00:00:00.000' AS DateTime), N'Completed')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (13, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (14, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (15, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (16, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (17, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
INSERT [dbo].[Tasks] ([TaskId], [ProjectId], [AssignedTo], [TaskName], [TaskDescription], [DueDate], [TaskStatus]) VALUES (18, 8, 20, N'123', N'124', CAST(N'2024-01-09T00:00:00.000' AS DateTime), N'Not Started')
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Timetables] ON 

INSERT [dbo].[Timetables] ([TimetableId], [UserId], [Timetable]) VALUES (4, 20, N'10/18/2023|Tuesday|07:55:00|OOPs|')
INSERT [dbo].[Timetables] ([TimetableId], [UserId], [Timetable]) VALUES (5, 24, N'10/10/2023|Monday|09:24:00|Event Scheduling|')
SET IDENTITY_INSERT [dbo].[Timetables] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [Password], [UserTypeId], [CreatedDate], [CustomId]) VALUES (20, N'student', N'1', N'student1@gmail.com', N'9Dec????', 3, CAST(N'2023-10-08T11:55:16.560' AS DateTime), N'SD10001')
INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [Password], [UserTypeId], [CreatedDate], [CustomId]) VALUES (22, N'admin', N'1', N'admin1@gmail.com', N'9Dec????', 1, CAST(N'2023-10-08T19:56:04.970' AS DateTime), NULL)
INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [Password], [UserTypeId], [CreatedDate], [CustomId]) VALUES (23, N'Supervisor', N'2', N'supervisor2@gmail.com', N'9Dec????', 2, CAST(N'2023-10-08T19:57:49.183' AS DateTime), N'SV10001')
INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [Password], [UserTypeId], [CreatedDate], [CustomId]) VALUES (24, N'Supervisor', N'one', N'supone@gmail.com', N'9Dec????', 2, CAST(N'2023-10-08T21:19:21.113' AS DateTime), N'SV10002')
INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Email], [Password], [UserTypeId], [CreatedDate], [CustomId]) VALUES (25, N'admin', N'one', N'adminone@gmail.com', N'9Dec????', 1, CAST(N'2023-10-08T21:20:22.190' AS DateTime), N'AD10001')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([UserTypeId], [TypeName]) VALUES (1, N'Admin')
INSERT [dbo].[UserTypes] ([UserTypeId], [TypeName]) VALUES (2, N'Supervisor')
INSERT [dbo].[UserTypes] ([UserTypeId], [TypeName]) VALUES (3, N'Student')
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
GO
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Users_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Users_Student]
GO
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Users_Supervisor] FOREIGN KEY([SupervisorID])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Users_Supervisor]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_Receiver] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_Receiver]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_Sender] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_Sender]
GO
ALTER TABLE [dbo].[ProjectDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDetail_Users] FOREIGN KEY([SupervisorID])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectDetails] CHECK CONSTRAINT [FK_ProjectDetail_Users]
GO
ALTER TABLE [dbo].[ProjectDocuments]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocuments_ProjectDetails] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectDetails] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectDocuments] CHECK CONSTRAINT [FK_ProjectDocuments_ProjectDetails]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tasks]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Users_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Users_Student]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Users_Supervisor] FOREIGN KEY([SupervisorID])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Users_Supervisor]
GO
ALTER TABLE [dbo].[StudentProjectRequest]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectDetails] ([ProjectId])
GO
ALTER TABLE [dbo].[StudentProjectRequest]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_ProjectDetails] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectDetails] ([ProjectId])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_ProjectDetails]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users] FOREIGN KEY([AssignedTo])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users]
GO
ALTER TABLE [dbo].[Timetables]  WITH CHECK ADD  CONSTRAINT [FK_Timetables_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Timetables] CHECK CONSTRAINT [FK_Timetables_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([UserTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes]
GO
USE [master]
GO
ALTER DATABASE [FYP_MS] SET  READ_WRITE 
GO
