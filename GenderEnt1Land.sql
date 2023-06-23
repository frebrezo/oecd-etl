USE [Oecd]
GO

/****** Object:  Table [dbo].[GenderEnt1Land]    Script Date: 10/25/2022 2:16:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GenderEnt1Land](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [nvarchar](50) NOT NULL,
	[CountryName] [nvarchar](100) NOT NULL,
	[IndicatorId] [nvarchar](50) NOT NULL,
	[IndicatorName] [nvarchar](100) NOT NULL,
	[SexId] [nvarchar](50) NOT NULL,
	[SexName] [nvarchar](100) NOT NULL,
	[AgeId] [nvarchar](50) NOT NULL,
	[AgeName] [nvarchar](100) NOT NULL,
	[TimeFormatId] [nvarchar](50) NOT NULL,
	[TimeFormatName] [nvarchar](100) NOT NULL,
	[UnitId] [nvarchar](50) NOT NULL,
	[UnitName] [nvarchar](100) NOT NULL,
	[UnitMultiplierId] [nvarchar](50) NOT NULL,
	[UnitMultiplierName] [nvarchar](100) NOT NULL,
	[ReferencePeriodId] [nvarchar](50) NULL,
	[ReferencePeriodName] [nvarchar](100) NULL,
	[Year] [int] NOT NULL,
	[Value] [decimal](18, 0) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_GenderEnt1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


