if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECUTable]') and OBJECTPROPERTY(id, N'IsECUTable') = 1)
drop table [dbo].[ECUTable]
GO

CREATE TABLE [dbo].[ECUTable] (
	[ECUID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--编号
	[ElectronicUnitNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--电子单元编号
	[CommunicationMode] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--通讯方式
	[AverageWorkingCurrent] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--平均工作电流
	[ProtectionLevel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--防护等级
	[dateOfManufacture] [datetime2](7) NOT NULL,	-- 生产日期
	[ProgramVersion] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--程序版本
	[IMSI] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--IMSI
	[IMEI] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--IMEI
	[ProductModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--产品型号
	[SignalIntensity] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--信号强度
	[MagneticInterferenceStatus] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--磁干扰状态
	[VoltageState] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--电压状态
	[Voltage] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--电压
	[BatteryModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--电池型号
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--备注
	[Status] [int]  NULL,
	[IsDeleted] [int] NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ECUTable] ADD CONSTRAINT
	PK_ECUTable PRIMARY KEY CLUSTERED 
	(
	ECUID
	) ON [PRIMARY]
GO

