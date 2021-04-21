if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InstallationPosition]') and OBJECTPROPERTY(id, N'IsInstallationPosition') = 1)
drop table [dbo].[InstallationPosition]
GO

CREATE TABLE [dbo].[InstallationPosition] (
	[IPID] [char] (20) NOT NULL ,	--标识
	[AreaID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL,--区域标识
	[AreaName] [char](100) NOT NULL,--区域名称
	[UptownID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--小区标识
	[UptownName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--小区名称
	[UptownAddr] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--小区地址
	[BuildID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--楼幢标识
	[BuildName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--楼幢名称
	[UnitID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--单元标识
	[UnitName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--单元名称
	[PayNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--缴费编号
	[InstallationTime] [datetime2](7)  NULL,--安装时间
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--备注
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InstallationPosition] ADD CONSTRAINT
	PK_InstallationPosition PRIMARY KEY CLUSTERED 
	(
	IPID
	) ON [PRIMARY]
GO

