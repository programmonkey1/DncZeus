if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EquipmentInstallation]') and OBJECTPROPERTY(id, N'IsEquipmentInstallation') = 1)
drop table [dbo].[EquipmentInstallation]
GO

CREATE TABLE [dbo].[EquipmentInstallation] (
    [EIID] [char] (20) NOT NULL ,	--��ʶ
	[DIID] [char] (20) NOT NULL ,	--�豸��ʶ
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--�豸���
	[IPID] [char] (20) NOT NULL,	--��װλ�ñ�ʶ
	[PayNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--�ɷѱ��
	[InstallationTime] [datetime2](7)  NULL,--��װʱ��
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ע
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
