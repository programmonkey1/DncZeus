
USE [DncZeus]
GO

/****** Object:  Table [dbo].[AfterView]    Script Date: 02/26/2021 22:47:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AfterView](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DataName] [nvarchar](50) NULL,
	[SaleRoomName] [varchar](50) NULL,
	[UptownNumber] [int] NULL,
	[MeterCount] [int] NULL,
	[CBWC2Count] [int] NULL,
	[SevenDP] [decimal](18, 0) NULL,
	[OneMP] [decimal](18, 0) NULL,
	[TwoMP] [decimal](18, 0) NULL,
	[ThreeMP] [decimal](18, 0) NULL,
	[CBWC5Number] [int] NULL,
	[ToDS] [int] NULL,
	[OneDS] [int] NULL,
	[TwoDS] [int] NULL,
	[ThreeDS] [int] NULL,
	[FourDS] [int] NULL,
	[FiveDS] [int] NULL,
	[SixDS] [int] NULL,
	[SevenDS] [int] NULL,
	[OneWS] [int] NULL,
	[TwoWS] [int] NULL,
	[ThreeWS] [int] NULL,
	[OneMS] [int] NULL,
	[TwoMS] [int] NULL,
	[ThreeMs] [int] NULL,
	[CBWC0Count] [int] NULL,
	[CBWC1Count] [int] NULL,
	[CBWC3Count] [int] NULL,
	[CBWC4Count] [int] NULL,
 CONSTRAINT [PK_Afterview] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


