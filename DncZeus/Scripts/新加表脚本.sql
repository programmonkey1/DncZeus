
/****** Object:  Table [dbo].[MeterView]    Script Date: 02/23/2021 09:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeterView](
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
	[DataName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[data]    Script Date: 02/23/2021 09:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[data](
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
	[version] [smallint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[QueryStatistics]    Script Date: 02/23/2021 09:54:27 ******/
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
	SELECT DataName, SaleRoomName ,
    count(distinct UptownID) as 楼幢总数,
    count(MeterID) as 水表总数,
    COUNT(case when CBWC=2 then CBWC end) as 通讯成功数量,
--LTRIM(STR(COUNT(case when CBWC=2 then CBWC end)*100.0/count(MeterID),6,3))+'%' as 抄表率,
    LTRIM(STR(COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ end)*100.0/count(MeterID),6,3))+'%' as 七天内抄表率,
    LTRIM(STR(COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end)*100.0/count(MeterID),6,3))+'%' as 前一个月抄表率,
    LTRIM(STR(COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end)*100.0/count(MeterID),6,3))+'%' as 前两个个月抄表率,
    LTRIM(STR(COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end)*100.0/count(MeterID),6,3))+'%' as 前三个个月抄表率,
    COUNT(case when CBWC=5 then CBWC end) as 通讯失败数量,
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())=0 then BYRQ end) as '0天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<0 then BYRQ end) as '1天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<1 then BYRQ end) as '2天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<2 then BYRQ end) as '3天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<3 then BYRQ end) as '4天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<4 then BYRQ end) as '5天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<5 then BYRQ end) as '6天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<6 then BYRQ end) as '7天抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ end) as '1周抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=14 then BYRQ end) as '2周抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=21 then BYRQ end) as '3周抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end) as '1个月抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end) as '2个月抄件成功',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end) as '3个月抄件成功',
    COUNT(case when CBWC=0 then CBWC end) as 未知数量,
    COUNT(case when CBWC=1 then CBWC end) as 正在通讯总数,
    COUNT(case when CBWC=3 then CBWC end) as 占线数量,
    COUNT(case when CBWC=4 then CBWC end) as 不在线数量
    FROM MeterView 
    GROUP BY DataName, SaleRoomName
--HAVING COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=120 then BYRQ end)>0
    order by SaleRoomName desc
END
GO
/****** Object:  StoredProcedure [dbo].[QueryMeterView]    Script Date: 02/23/2021 09:54:27 ******/
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
