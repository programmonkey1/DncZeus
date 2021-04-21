if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECUTable]') and OBJECTPROPERTY(id, N'IsECUTable') = 1)
drop table [dbo].[ECUTable]
GO

CREATE TABLE [dbo].[ECUTable] (
	[ECUID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--���
	[ElectronicUnitNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--���ӵ�Ԫ���
	[CommunicationMode] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--ͨѶ��ʽ
	[AverageWorkingCurrent] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--ƽ����������
	[ProtectionLevel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--�����ȼ�
	[dateOfManufacture] [datetime2](7) NOT NULL,	-- ��������
	[ProgramVersion] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--����汾
	[IMSI] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--IMSI
	[IMEI] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--IMEI
	[ProductModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��Ʒ�ͺ�
	[SignalIntensity] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--�ź�ǿ��
	[MagneticInterferenceStatus] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--�Ÿ���״̬
	[VoltageState] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ѹ״̬
	[Voltage] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ѹ
	[BatteryModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--����ͺ�
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ע
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

