if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeviceInformation]') and OBJECTPROPERTY(id, N'IsDeviceInformation') = 1)
drop table [dbo].[DeviceInformation]
GO

CREATE TABLE [dbo].[DeviceInformation] (
	[DIID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL ,	--  ��ʶ
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--�豸���
	[ProductModel] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--��Ʒ�ͺ�
	[ECUID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL  ,	--���ӵ�Ԫ���
	[ElectronicUnitNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--���ӵ�Ԫ���
	[BTID] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NOT NULL  ,	--������
	[BatchNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--�������α��
	[DateOfManufacture] [datetime2](7) NULL ,	--��������
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--��ע
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

