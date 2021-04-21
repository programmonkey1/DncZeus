if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BaseTable]') and OBJECTPROPERTY(id, N'IsBaseTable') = 1)
drop table [dbo].[BaseTable]
GO

CREATE TABLE [dbo].[BaseTable] (
	[BTID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--编号
	[BatchNumber] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--批次编号
	[BaseCount] [int] NOT NULL,--生成数量
	[SteelSealNumberRange] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--钢印编号
	[ParameterSpecification] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--参数规格
	[InstallationMethod] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--安装方式
	[CommonTraffic] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--常用流量
	[CommonFlowRatio] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--常用流量比
	[InitialFlow] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--始动流量
	[MinimumReading] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL, 	--最小读数
	[MaximumDegree] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--最大读数
	[MaximumWorkingPressure] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--最大工作压力
	[MaximumOperatingTemperature] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--最大工作温度
	[WorkingEnvironmentTemperature] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--工作环境温度
	[WorkingEnvironmentHumidity] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--工作环境湿度
	[ExecutiveStandard] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--执行标准
	[DateOfManufacture] [datetime2](7)   NULL, 	--生产日期
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--备注
	[Status] [int]  NULL,
	[IsDeleted] [int]  NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BaseTable] ADD CONSTRAINT
	PK_BaseTable PRIMARY KEY CLUSTERED 
	(
	BTID
	) ON [PRIMARY]
GO

