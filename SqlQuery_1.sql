Create Database BasketBallData1
GO

USE [BasketBallData1]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 22/11/2025 18:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [nvarchar](100) NULL,
	[CountryName] [nvarchar](300) NULL,
	[Team1Name] [nvarchar](300) NULL,
	[Team2Name] [nvarchar](300) NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
