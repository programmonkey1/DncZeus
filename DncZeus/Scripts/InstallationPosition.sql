if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InstallationPosition]') and OBJECTPROPERTY(id, N'IsInstallationPosition') = 1)
drop table [dbo].[InstallationPosition]
GO

CREATE TABLE [dbo].[InstallationPosition] (
	[IPID] [char] (20) NOT NULL ,	--��ʶ
	[AreaID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL,--�����ʶ
	[AreaName] [char](100) NOT NULL,--��������
	[UptownID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--С����ʶ
	[UptownName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--С������
	[UptownAddr] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--С����ַ
	[BuildID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--¥����ʶ
	[BuildName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--¥������
	[UnitID] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--��Ԫ��ʶ
	[UnitName] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--��Ԫ����
	[PayNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL ,	--�ɷѱ��
	[InstallationTime] [datetime2](7)  NULL,--��װʱ��
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT ''  NULL, 	--��ע
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

