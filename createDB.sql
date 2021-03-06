USE [Solmvc]
GO
/****** Object:  Table [dbo].[Problems]    Script Date: 29.10.2015 1:30:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Problems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](550) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Performers] [nvarchar](1050) NULL,
	[Status] [nvarchar](50) NULL,
	[Laboriousness] [int] NULL,
	[ActualExecutiontime] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskHierarchy]    Script Date: 29.10.2015 1:30:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskHierarchy](
	[ParentTask] [int] NOT NULL,
	[ChildTask] [int] NOT NULL,
 CONSTRAINT [PK_TaskHierarchy] PRIMARY KEY CLUSTERED 
(
	[ParentTask] ASC,
	[ChildTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Problems] ADD  CONSTRAINT [DF_Tasks_StartDate]  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[TaskHierarchy]  WITH CHECK ADD  CONSTRAINT [FK_TaskHierarchy_Tasks] FOREIGN KEY([ChildTask])
REFERENCES [dbo].[Problems] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskHierarchy] CHECK CONSTRAINT [FK_TaskHierarchy_Tasks]
GO
ALTER TABLE [dbo].[TaskHierarchy]  WITH CHECK ADD  CONSTRAINT [FK_TaskHierarchy_Tasks1] FOREIGN KEY([ParentTask])
REFERENCES [dbo].[Problems] ([Id])
GO
ALTER TABLE [dbo].[TaskHierarchy] CHECK CONSTRAINT [FK_TaskHierarchy_Tasks1]
GO
