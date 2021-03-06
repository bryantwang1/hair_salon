CREATE DATABASE [hair_salon]
GO
USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 12/9/2016 3:44:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](255) NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stylists]    Script Date: 12/9/2016 3:44:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phone] [varchar](255) NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [description], [stylist_id]) VALUES (6, N'Thomas', N'Tips well, comes in about once a week.', 5)
INSERT [dbo].[clients] ([id], [name], [description], [stylist_id]) VALUES (7, N'Frank', N'Nice guy.', 5)
INSERT [dbo].[clients] ([id], [name], [description], [stylist_id]) VALUES (8, N'Bob', N'Yet another client.', 5)
INSERT [dbo].[clients] ([id], [name], [description], [stylist_id]) VALUES (9, N'Cow', N'A cow.', 6)
INSERT [dbo].[clients] ([id], [name], [description], [stylist_id]) VALUES (10, N'Yukiko', N'Doesn''t speak English.', 6)
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([id], [name], [phone], [description]) VALUES (5, N'Alice', N'(555)555-5555', N'Works fast, positive feedback from clients.')
INSERT [dbo].[stylists] ([id], [name], [phone], [description]) VALUES (6, N'Kenny', N'(555)434-1093', N'Incredibly efficient.')
INSERT [dbo].[stylists] ([id], [name], [phone], [description]) VALUES (7, N'Priscilla', N'(555)302-4551', N'Never comes into work on time.')
SET IDENTITY_INSERT [dbo].[stylists] OFF
