USE [DncZeus]
GO
/****** Object:  Table [dbo].[DncUser]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncUser](
	[Guid] [uniqueidentifier] NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Password] [nvarchar](255) NULL,
	[Avatar] [nvarchar](255) NULL,
	[UserType] [int] NOT NULL,
	[IsLocked] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedByUserGuid] [uniqueidentifier] NOT NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedByUserGuid] [uniqueidentifier] NULL,
	[ModifiedByUserName] [nvarchar](max) NULL,
	[Description] [nvarchar](800) NULL,
 CONSTRAINT [PK_DncUser] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DncRole]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncRole](
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedByUserGuid] [uniqueidentifier] NOT NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedByUserGuid] [uniqueidentifier] NULL,
	[ModifiedByUserName] [nvarchar](max) NULL,
	[IsSuperAdministrator] [bit] NOT NULL,
	[IsBuiltin] [bit] NOT NULL,
 CONSTRAINT [PK_DncRole] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Warehouse](
	[WHID] [int] NOT NULL,
	[EUNumber] [char](20) NOT NULL,
	[TimeOut] [datetime] NOT NULL,
	[TimeEnter] [datetime] NOT NULL,
	[StateType] [tinyint] NOT NULL,
	[Remarks] [char](100) NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_Warehouse] PRIMARY KEY CLUSTERED 
(
	[WHID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Recovery]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Recovery](
	[RID] [int] NOT NULL,
	[EUNumber] [char](20) NOT NULL,
	[TimeEnter] [datetime] NOT NULL,
	[Number] [int] NOT NULL,
	[FaultType] [tinyint] NOT NULL,
	[Remarks] [char](100) NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_Recovery] PRIMARY KEY CLUSTERED 
(
	[RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DncMenu]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncMenu](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[Icon] [nvarchar](128) NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[ParentName] [nvarchar](max) NULL,
	[Level] [int] NOT NULL,
	[Description] [nvarchar](800) NULL,
	[Sort] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
	[IsDefaultRouter] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedByUserGuid] [uniqueidentifier] NOT NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedByUserGuid] [uniqueidentifier] NULL,
	[ModifiedByUserName] [nvarchar](max) NULL,
	[Component] [nvarchar](255) NULL,
	[HideInMenu] [int] NULL,
	[NotCache] [int] NULL,
	[BeforeCloseFun] [nvarchar](255) NULL,
 CONSTRAINT [PK_DncMenu] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DncIcon]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncIcon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Size] [nvarchar](20) NULL,
	[Color] [nvarchar](50) NULL,
	[Custom] [nvarchar](60) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedByUserGuid] [uniqueidentifier] NOT NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedByUserGuid] [uniqueidentifier] NULL,
	[ModifiedByUserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_DncIcon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceInformation]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceInformation](
	[DIID] [char](20) NOT NULL,
	[EUNumber] [char](20) NOT NULL,
	[ProductModel] [char](20) NOT NULL,
	[ECUID] [char](20) NOT NULL,
	[ElectronicUnitNumber] [char](20) NOT NULL,
	[BTID] [char](20) NOT NULL,
	[BatchNumber] [char](20) NOT NULL,
	[DateOfManufacture] [datetime2](7) NOT NULL,
	[Remarks] [char](100) NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_DeviceInformation] PRIMARY KEY CLUSTERED 
(
	[DIID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[data]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[data](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](128) NOT NULL,
	[dbid] [smallint] NULL,
	[sid] [varbinary](85) NULL,
	[mode] [smallint] NULL,
	[status] [int] NULL,
	[status2] [int] NULL,
	[crdate] [datetime] NOT NULL,
	[reserved] [datetime] NULL,
	[category] [int] NULL,
	[cmptlevel] [tinyint] NOT NULL,
	[filename] [nvarchar](260) NULL,
	[version] [smallint] NULL,
 CONSTRAINT [PK_data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BaseTable]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BaseTable](
	[BTID] [char](20) NOT NULL,
	[BatchNumber] [char](60) NOT NULL,
	[BatchCount] [int] NULL,
	[SteelSealNumberRange] [char](60) NULL,
	[ParameterSpecification] [char](60) NULL,
	[InstallationMethod] [char](20) NULL,
	[CommonTraffic] [char](20) NULL,
	[CommonFlowRatio] [char](20) NULL,
	[InitialFlow] [char](20) NULL,
	[MinimumReading] [char](20) NULL,
	[MaximumDegree] [char](20) NULL,
	[MaximumWorkingPressure] [char](20) NULL,
	[MaximumOperatingTemperature] [char](20) NULL,
	[WorkingEnvironmentTemperature] [char](20) NULL,
	[WorkingEnvironmentHumidity] [char](20) NULL,
	[ExecutiveStandard] [char](50) NULL,
	[DateOfManufacture] [datetime2](7) NULL,
	[Remarks] [char](100) NULL,
	[Status] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_BaseTable] PRIMARY KEY CLUSTERED 
(
	[BTID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AfterView]    Script Date: 03/12/2021 13:09:11 ******/
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
/****** Object:  Table [dbo].[AfterStaff]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AfterStaff](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceName] [nvarchar](50) NULL,
	[CityName] [varchar](50) NULL,
	[AreaName] [varchar](50) NULL,
	[Salesman] [varchar](50) NULL,
	[AfterSalesStaff] [varchar](50) NULL,
	[Customer] [varchar](50) NULL,
	[Telephone1] [varchar](50) NULL,
	[Telephone2] [varchar](50) NULL,
	[Telephone3] [varchar](50) NULL,
	[Status] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_AfterStaff] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeterView]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeterView](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SaleRoomName] [varchar](50) NULL,
	[AreaID] [int] NOT NULL,
	[AreaName] [varchar](50) NULL,
	[GprsCodeA] [int] NOT NULL,
	[GprsCodeb] [int] NOT NULL,
	[GprsCodec] [int] NOT NULL,
	[UptownID] [int] NOT NULL,
	[UptownName] [varchar](50) NULL,
	[BuildID] [int] NOT NULL,
	[BuildName] [varchar](50) NULL,
	[UnitID] [int] NOT NULL,
	[UnitName] [varchar](50) NULL,
	[UserID] [int] NOT NULL,
	[UserName] [varchar](60) NULL,
	[UserAddr] [varchar](60) NULL,
	[RoomNumber] [varchar](20) NULL,
	[MeterID] [int] NOT NULL,
	[MeterAddr] [char](14) NOT NULL,
	[SYS] [numeric](12, 2) NOT NULL,
	[BYS] [numeric](12, 2) NOT NULL,
	[SYRQ] [datetime] NOT NULL,
	[BYRQ] [datetime] NOT NULL,
	[LRRQ] [datetime] NOT NULL,
	[CBWC] [tinyint] NOT NULL,
	[DataName] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeterView] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaintainerInformation]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaintainerInformation](
	[MIID] [int] NOT NULL,
	[AreaName] [char](100) NOT NULL,
	[Post] [char](50) NOT NULL,
	[Telephone] [char](50) NOT NULL,
	[Remarks] [char](100) NOT NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_MaintainerInformation] PRIMARY KEY CLUSTERED 
(
	[MIID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InstallationPosition]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstallationPosition](
	[IPID] [nvarchar](20) NOT NULL,
	[AreaID] [nvarchar](20) NULL,
	[AreaName] [nvarchar](100) NOT NULL,
	[UptownID] [nvarchar](20) NULL,
	[UptownName] [nvarchar](50) NULL,
	[UptownAddr] [nvarchar](50) NULL,
	[BuildID] [nvarchar](20) NULL,
	[BuildName] [nvarchar](50) NULL,
	[UnitID] [nvarchar](20) NULL,
	[UnitName] [nvarchar](50) NULL,
	[PayNumber] [nvarchar](50) NULL,
	[InstallationTime] [datetime2](7) NULL,
	[Remarks] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_InstallationPosition] PRIMARY KEY CLUSTERED 
(
	[IPID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentInstallation]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentInstallation](
	[EIID] [nvarchar](20) NOT NULL,
	[DIID] [nvarchar](50) NOT NULL,
	[EUNumber] [nvarchar](50) NOT NULL,
	[IPID] [nvarchar](50) NOT NULL,
	[PayNumber] [nvarchar](50) NULL,
	[InstallationTime] [datetime2](7) NULL,
	[Remarks] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
 CONSTRAINT [PK_EquipmentInstallation] PRIMARY KEY CLUSTERED 
(
	[EIID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ECUTable]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ECUTable](
	[ECUID] [char](20) NOT NULL,
	[ElectronicUnitNumber] [char](20) NOT NULL,
	[CommunicationMode] [char](20) NULL,
	[AverageWorkingCurrent] [char](20) NULL,
	[ProtectionLevel] [char](20) NULL,
	[dateOfManufacture] [datetime2](7) NOT NULL,
	[ProgramVersion] [char](20) NULL,
	[IMSI] [char](20) NULL,
	[IMEI] [char](20) NULL,
	[ProductModel] [char](20) NULL,
	[SignalIntensity] [char](20) NULL,
	[MagneticInterferenceStatus] [char](20) NULL,
	[VoltageState] [char](20) NULL,
	[Voltage] [char](20) NULL,
	[BatteryModel] [char](20) NULL,
	[Remarks] [char](100) NULL,
	[Status] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_ECUTable] PRIMARY KEY CLUSTERED 
(
	[ECUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DncUserRoleMapping]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncUserRoleMapping](
	[UserGuid] [uniqueidentifier] NOT NULL,
	[RoleCode] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DncUserRoleMapping] PRIMARY KEY CLUSTERED 
(
	[UserGuid] ASC,
	[RoleCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[QueryStatistics]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[QueryStatistics] 
	AS
BEGIN
    delete from dbo.AfterView
    insert into AfterView([DataName],[SaleRoomName],[UptownNumber],[MeterCount],[CBWC2Count],[SevenDP],[OneMP],[TwoMP]
      ,[ThreeMP],[CBWC5Number],[ToDS],[OneDS],[TwoDS],[ThreeDS],[FourDS],[FiveDS],[SixDS],[SevenDS],[OneWS]
      ,[TwoWS],[ThreeWS],[OneMS],[TwoMS],[ThreeMs],[CBWC0Count],[CBWC1Count],[CBWC3Count],[CBWC4Count])

	SELECT DataName, SaleRoomName ,
    count(distinct UptownID) as UptownNumber,
    count(MeterID) as MeterCount,
    COUNT(case when CBWC=2 then CBWC end) as CBWC2Count,
--LTRIM(STR(COUNT(case when CBWC=2 then CBWC end)*100.0/count(MeterID),6,3))+'%' as 抄表率,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ  end)*100.0/count(isnull(MeterID,0))) as SevenDP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end)*100.0/count(isnull(MeterID,0))) as OneMP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end)*100.0/count(isnull(MeterID,0))) as TwoMP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end)*100.0/count(isnull(MeterID,0))) as ThreeMP,
    COUNT(case when CBWC=5 then CBWC end) as CBWC5Number,
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())=0 then BYRQ end) as 'ToDS',--one two three four five six seven eight nine
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<0 then BYRQ end) as 'OneDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<1 then BYRQ end) as 'TwoDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<2 then BYRQ end) as 'ThreeDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<3 then BYRQ end) as 'FourDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<4 then BYRQ end) as 'FiveDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<5 then BYRQ end) as 'SixDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<6 then BYRQ end) as 'SevenDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ end) as 'OneWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=14 then BYRQ end) as 'TwoWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=21 then BYRQ end) as 'ThreeWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end) as 'OneMS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end) as 'TwoMS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end) as 'ThreeMs',
    COUNT(case when CBWC=0 then CBWC end) as CBWC0Count,
    COUNT(case when CBWC=1 then CBWC end) as CBWC1Count,
    COUNT(case when CBWC=3 then CBWC end) as CBWC3Count,
    COUNT(case when CBWC=4 then CBWC end) as CBWC4Count
   
    FROM MeterView 
    GROUP BY DataName, SaleRoomName
--HAVING COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=120 then BYRQ end)>0
    order by ToDS desc
    select * from dbo.AfterView order by ToDS desc
END
GO
/****** Object:  StoredProcedure [dbo].[QueryMeterView]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[QueryMeterView]   
	
AS
BEGIN
    declare @name nvarchar(50)
    declare @te nvarchar(50)
    declare @MeterView nvarchar(1000)
    declare dataTemp  cursor for select name from data where name like '%GprsData%'
    open dataTemp
	delete from MeterView
    fetch next from dataTemp into @name
    while @@FETCH_STATUS=0--这里计数会多一个,
    begin
		 set @te= @name		
		 set @MeterView='insert into MeterView( SaleRoomName,AreaID, AreaName,GprsCodeA,GprsCodeb,GprsCodec, UptownID, UptownName, BuildID, BuildName, UnitID, UnitName, UserID, UserName, UserAddr, RoomNumber, MeterID, MeterAddr, SYS, BYS, SYRQ, BYRQ, LRRQ, CBWC,DataName)
		 select SaleRoomName,AreaID, AreaName,GprsCodeA,GprsCodeb,GprsCodec, UptownID, UptownName, BuildID, BuildName, UnitID, UnitName, UserID, UserName, UserAddr, RoomNumber, MeterID, MeterAddr, SYS, BYS, SYRQ, BYRQ, LRRQ, CBWC, '''+@te+''' as DataName
		 FROM  OPENDATASOURCE (''SQLOLEDB'', ''Data Source=222.186.212.106,6788;User ID=sa;Password=BGT%VFR$CDE#'' ).'+@te+'.dbo.MeterView '
		 exec( @MeterView)   
		 FETCH NEXT FROM  dataTemp INTO @name
	end
	select * from MeterView
    close dataTemp
    DEALLOCATE dataTemp
END
GO
/****** Object:  Table [dbo].[DncPermission]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncPermission](
	[Code] [nvarchar](20) NOT NULL,
	[MenuGuid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ActionCode] [nvarchar](80) NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CreatedByUserGuid] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedByUserName] [nvarchar](max) NULL,
	[ModifiedOn] [datetime2](7) NULL,
	[ModifiedByUserGuid] [uniqueidentifier] NULL,
	[ModifiedByUserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_DncPermission] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DncRolePermissionMapping]    Script Date: 03/12/2021 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DncRolePermissionMapping](
	[RoleCode] [nvarchar](50) NOT NULL,
	[PermissionCode] [nvarchar](20) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DncRolePermissionMapping] PRIMARY KEY CLUSTERED 
(
	[RoleCode] ASC,
	[PermissionCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__BaseTable__BTID__147C05D0]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__BTID__147C05D0]  DEFAULT ('') FOR [BTID]
GO
/****** Object:  Default [DF__BaseTable__Batch__15702A09]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Batch__15702A09]  DEFAULT ('') FOR [BatchNumber]
GO
/****** Object:  Default [DF__BaseTable__Steel__16644E42]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Steel__16644E42]  DEFAULT ('') FOR [SteelSealNumberRange]
GO
/****** Object:  Default [DF__BaseTable__Param__1758727B]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Param__1758727B]  DEFAULT ('') FOR [ParameterSpecification]
GO
/****** Object:  Default [DF__BaseTable__Insta__184C96B4]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Insta__184C96B4]  DEFAULT ('') FOR [InstallationMethod]
GO
/****** Object:  Default [DF__BaseTable__Commo__1940BAED]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Commo__1940BAED]  DEFAULT ('') FOR [CommonTraffic]
GO
/****** Object:  Default [DF__BaseTable__Commo__1A34DF26]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Commo__1A34DF26]  DEFAULT ('') FOR [CommonFlowRatio]
GO
/****** Object:  Default [DF__BaseTable__Initi__1B29035F]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Initi__1B29035F]  DEFAULT ('') FOR [InitialFlow]
GO
/****** Object:  Default [DF__BaseTable__Minim__1C1D2798]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Minim__1C1D2798]  DEFAULT ('') FOR [MinimumReading]
GO
/****** Object:  Default [DF__BaseTable__Maxim__1D114BD1]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Maxim__1D114BD1]  DEFAULT ('') FOR [MaximumDegree]
GO
/****** Object:  Default [DF__BaseTable__Maxim__1E05700A]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Maxim__1E05700A]  DEFAULT ('') FOR [MaximumWorkingPressure]
GO
/****** Object:  Default [DF__BaseTable__Maxim__1EF99443]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Maxim__1EF99443]  DEFAULT ('') FOR [MaximumOperatingTemperature]
GO
/****** Object:  Default [DF__BaseTable__Worki__1FEDB87C]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Worki__1FEDB87C]  DEFAULT ('') FOR [WorkingEnvironmentTemperature]
GO
/****** Object:  Default [DF__BaseTable__Worki__20E1DCB5]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Worki__20E1DCB5]  DEFAULT ('') FOR [WorkingEnvironmentHumidity]
GO
/****** Object:  Default [DF__BaseTable__Execu__21D600EE]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Execu__21D600EE]  DEFAULT ('') FOR [ExecutiveStandard]
GO
/****** Object:  Default [DF__BaseTable__Remar__22CA2527]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[BaseTable] ADD  CONSTRAINT [DF__BaseTable__Remar__22CA2527]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__DeviceInfo__DIID__4F9CCB9E]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInfo__DIID__4F9CCB9E]  DEFAULT ('') FOR [DIID]
GO
/****** Object:  Default [DF__DeviceInf__EUNum__5090EFD7]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__EUNum__5090EFD7]  DEFAULT ('') FOR [EUNumber]
GO
/****** Object:  Default [DF__DeviceInf__Produ__51851410]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__Produ__51851410]  DEFAULT ('') FOR [ProductModel]
GO
/****** Object:  Default [DF__DeviceInf__ECUID__52793849]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__ECUID__52793849]  DEFAULT ('') FOR [ECUID]
GO
/****** Object:  Default [DF__DeviceInf__Elect__536D5C82]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__Elect__536D5C82]  DEFAULT ('') FOR [ElectronicUnitNumber]
GO
/****** Object:  Default [DF__DeviceInfo__BTID__546180BB]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInfo__BTID__546180BB]  DEFAULT ('') FOR [BTID]
GO
/****** Object:  Default [DF__DeviceInf__Batch__5555A4F4]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__Batch__5555A4F4]  DEFAULT ('') FOR [BatchNumber]
GO
/****** Object:  Default [DF__DeviceInf__DateO__5649C92D]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__DateO__5649C92D]  DEFAULT ('') FOR [DateOfManufacture]
GO
/****** Object:  Default [DF__DeviceInf__Remar__573DED66]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DeviceInformation] ADD  CONSTRAINT [DF__DeviceInf__Remar__573DED66]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__ECUTable__ECUID__04459E07]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [ECUID]
GO
/****** Object:  Default [DF__ECUTable__Electr__0539C240]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [ElectronicUnitNumber]
GO
/****** Object:  Default [DF__ECUTable__Commun__062DE679]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [CommunicationMode]
GO
/****** Object:  Default [DF__ECUTable__Averag__07220AB2]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [AverageWorkingCurrent]
GO
/****** Object:  Default [DF__ECUTable__Protec__08162EEB]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [ProtectionLevel]
GO
/****** Object:  Default [DF__ECUTable__Progra__090A5324]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [ProgramVersion]
GO
/****** Object:  Default [DF__ECUTable__IMSI__09FE775D]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [IMSI]
GO
/****** Object:  Default [DF__ECUTable__IMEI__0AF29B96]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [IMEI]
GO
/****** Object:  Default [DF__ECUTable__Produc__0BE6BFCF]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [ProductModel]
GO
/****** Object:  Default [DF__ECUTable__Signal__0CDAE408]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [SignalIntensity]
GO
/****** Object:  Default [DF__ECUTable__Magnet__0DCF0841]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [MagneticInterferenceStatus]
GO
/****** Object:  Default [DF__ECUTable__Voltag__0EC32C7A]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [VoltageState]
GO
/****** Object:  Default [DF__ECUTable__Voltag__0FB750B3]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [Voltage]
GO
/****** Object:  Default [DF__ECUTable__Batter__10AB74EC]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [BatteryModel]
GO
/****** Object:  Default [DF__ECUTable__Remark__119F9925]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[ECUTable] ADD  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__Equipment__EUNum__06ED0088]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[EquipmentInstallation] ADD  CONSTRAINT [DF__Equipment__EUNum__06ED0088]  DEFAULT ('') FOR [EUNumber]
GO
/****** Object:  Default [DF__Equipment__PayNu__07E124C1]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[EquipmentInstallation] ADD  CONSTRAINT [DF__Equipment__PayNu__07E124C1]  DEFAULT ('') FOR [PayNumber]
GO
/****** Object:  Default [DF__Equipment__Remar__08D548FA]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[EquipmentInstallation] ADD  CONSTRAINT [DF__Equipment__Remar__08D548FA]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__Installat__AreaI__0BB1B5A5]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__AreaI__0BB1B5A5]  DEFAULT ('') FOR [AreaID]
GO
/****** Object:  Default [DF__Installat__Uptow__0CA5D9DE]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Uptow__0CA5D9DE]  DEFAULT ('') FOR [UptownID]
GO
/****** Object:  Default [DF__Installat__Uptow__0D99FE17]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Uptow__0D99FE17]  DEFAULT ('') FOR [UptownName]
GO
/****** Object:  Default [DF__Installat__Uptow__0E8E2250]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Uptow__0E8E2250]  DEFAULT ('') FOR [UptownAddr]
GO
/****** Object:  Default [DF__Installat__Build__0F824689]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Build__0F824689]  DEFAULT ('') FOR [BuildID]
GO
/****** Object:  Default [DF__Installat__Build__10766AC2]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Build__10766AC2]  DEFAULT ('') FOR [BuildName]
GO
/****** Object:  Default [DF__Installat__UnitI__116A8EFB]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__UnitI__116A8EFB]  DEFAULT ('') FOR [UnitID]
GO
/****** Object:  Default [DF__Installat__UnitN__125EB334]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__UnitN__125EB334]  DEFAULT ('') FOR [UnitName]
GO
/****** Object:  Default [DF__Installat__PayNu__1352D76D]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__PayNu__1352D76D]  DEFAULT ('') FOR [PayNumber]
GO
/****** Object:  Default [DF__Installat__Remar__1446FBA6]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[InstallationPosition] ADD  CONSTRAINT [DF__Installat__Remar__1446FBA6]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__Maintainer__Post__6CA31EA0]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[MaintainerInformation] ADD  DEFAULT ('') FOR [Post]
GO
/****** Object:  Default [DF__Maintaine__Telep__6D9742D9]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[MaintainerInformation] ADD  DEFAULT ('') FOR [Telephone]
GO
/****** Object:  Default [DF__Maintaine__Remar__6E8B6712]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[MaintainerInformation] ADD  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__Recovery__EUNumb__1881A0DE]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Recovery] ADD  DEFAULT ('') FOR [EUNumber]
GO
/****** Object:  Default [DF__Recovery__TimeEn__1975C517]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Recovery] ADD  DEFAULT ((0)) FOR [TimeEnter]
GO
/****** Object:  Default [DF__Recovery__FaultT__1A69E950]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Recovery] ADD  DEFAULT ((0)) FOR [FaultType]
GO
/****** Object:  Default [DF__Recovery__Remark__1B5E0D89]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Recovery] ADD  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__Warehouse__EUNum__1E3A7A34]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Warehouse] ADD  DEFAULT ('') FOR [EUNumber]
GO
/****** Object:  Default [DF__Warehouse__TimeO__1F2E9E6D]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Warehouse] ADD  DEFAULT ((0)) FOR [TimeOut]
GO
/****** Object:  Default [DF__Warehouse__TimeE__2022C2A6]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Warehouse] ADD  DEFAULT ((0)) FOR [TimeEnter]
GO
/****** Object:  Default [DF__Warehouse__State__2116E6DF]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Warehouse] ADD  DEFAULT ((0)) FOR [StateType]
GO
/****** Object:  Default [DF__Warehouse__Remar__220B0B18]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[Warehouse] ADD  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  ForeignKey [FK_DncPermission_DncMenu_MenuGuid]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DncPermission]  WITH CHECK ADD  CONSTRAINT [FK_DncPermission_DncMenu_MenuGuid] FOREIGN KEY([MenuGuid])
REFERENCES [dbo].[DncMenu] ([Guid])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DncPermission] CHECK CONSTRAINT [FK_DncPermission_DncMenu_MenuGuid]
GO
/****** Object:  ForeignKey [FK_DncRolePermissionMapping_DncPermission_PermissionCode]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DncRolePermissionMapping]  WITH CHECK ADD  CONSTRAINT [FK_DncRolePermissionMapping_DncPermission_PermissionCode] FOREIGN KEY([PermissionCode])
REFERENCES [dbo].[DncPermission] ([Code])
GO
ALTER TABLE [dbo].[DncRolePermissionMapping] CHECK CONSTRAINT [FK_DncRolePermissionMapping_DncPermission_PermissionCode]
GO
/****** Object:  ForeignKey [FK_DncRolePermissionMapping_DncRole_RoleCode]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DncRolePermissionMapping]  WITH CHECK ADD  CONSTRAINT [FK_DncRolePermissionMapping_DncRole_RoleCode] FOREIGN KEY([RoleCode])
REFERENCES [dbo].[DncRole] ([Code])
GO
ALTER TABLE [dbo].[DncRolePermissionMapping] CHECK CONSTRAINT [FK_DncRolePermissionMapping_DncRole_RoleCode]
GO
/****** Object:  ForeignKey [FK_DncUserRoleMapping_DncRole_RoleCode]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DncUserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_DncUserRoleMapping_DncRole_RoleCode] FOREIGN KEY([RoleCode])
REFERENCES [dbo].[DncRole] ([Code])
GO
ALTER TABLE [dbo].[DncUserRoleMapping] CHECK CONSTRAINT [FK_DncUserRoleMapping_DncRole_RoleCode]
GO
/****** Object:  ForeignKey [FK_DncUserRoleMapping_DncUser_UserGuid]    Script Date: 03/12/2021 13:09:11 ******/
ALTER TABLE [dbo].[DncUserRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_DncUserRoleMapping_DncUser_UserGuid] FOREIGN KEY([UserGuid])
REFERENCES [dbo].[DncUser] ([Guid])
GO
ALTER TABLE [dbo].[DncUserRoleMapping] CHECK CONSTRAINT [FK_DncUserRoleMapping_DncUser_UserGuid]
GO
