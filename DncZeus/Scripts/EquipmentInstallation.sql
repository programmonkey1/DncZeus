if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EquipmentInstallation]') and OBJECTPROPERTY(id, N'IsEquipmentInstallation') = 1)
drop table [dbo].[EquipmentInstallation]
GO

CREATE TABLE [dbo].[EquipmentInstallation] (
    [EIID] [char] (20) NOT NULL ,	--标识
	[DIID] [char] (20) NOT NULL ,	--设备标识
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--设备编号
	[IPID] [char] (20) NOT NULL,	--安装位置标识
	[PayNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--缴费编号
	[InstallationTime] [datetime2](7)  NULL,--安装时间
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--备注
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EquipmentInstallation] ADD CONSTRAINT
	PK_EquipmentInstallation PRIMARY KEY CLUSTERED 
	(
	EIID
	) ON [PRIMARY]
GO
