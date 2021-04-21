if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BaseTable]') and OBJECTPROPERTY(id, N'IsBaseTable') = 1)
drop table [dbo].[BaseTable]
GO

CREATE TABLE [dbo].[BaseTable] (
	[BTID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--���
	[BatchNumber] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--���α��
	[BaseCount] [int] NOT NULL,--��������
	[SteelSealNumberRange] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--��ӡ���
	[ParameterSpecification] [char] (60) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--�������
	[InstallationMethod] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--��װ��ʽ
	[CommonTraffic] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--��������
	[CommonFlowRatio] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--����������
	[InitialFlow] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL ,	--ʼ������
	[MinimumReading] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL, 	--��С����
	[MaximumDegree] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--������
	[MaximumWorkingPressure] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--�����ѹ��
	[MaximumOperatingTemperature] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--������¶�
	[WorkingEnvironmentTemperature] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--���������¶�
	[WorkingEnvironmentHumidity] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��������ʪ��
	[ExecutiveStandard] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--ִ�б�׼
	[DateOfManufacture] [datetime2](7)   NULL, 	--��������
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ע
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

