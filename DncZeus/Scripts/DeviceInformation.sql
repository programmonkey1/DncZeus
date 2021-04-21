if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeviceInformation]') and OBJECTPROPERTY(id, N'IsDeviceInformation') = 1)
drop table [dbo].[DeviceInformation]
GO

CREATE TABLE [dbo].[DeviceInformation] (
	[DIID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL ,	--  标识
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--设备编号
	[ProductModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--产品型号
	[ECUID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL  ,	--电子单元编号
	[ElectronicUnitNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--电子单元编号
	[BTID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL  ,	--基表编号
	[BatchNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--基表批次编号
	[DateOfManufacture] [datetime2](7) NULL ,	--生产日期
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--备注
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DeviceInformation] ADD CONSTRAINT
	PK_DeviceInformation PRIMARY KEY CLUSTERED 
	(
	DIID
	) ON [PRIMARY]
GO

