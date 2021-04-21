if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Warehouse]') and OBJECTPROPERTY(id, N'IsWarehouse') = 1)
drop table [dbo].[Warehouse]
GO

CREATE TABLE [dbo].[Warehouse] (
	[WHID] [int] NOT NULL ,	--��ʶ
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--�豸���
	[TimeOut] [datetime] DEFAULT 0 NOT NULL ,	--����ʱ��
	[TimeEnter] [datetime] DEFAULT 0 NOT NULL ,	--���ʱ��	
	[StateType] [tinyint] DEFAULT 0 NOT NULL ,	--״̬���� 0δ֪,1δʹ��,2ʹ��
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--��ע
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Warehouse] ADD CONSTRAINT
	PK_Warehouse PRIMARY KEY CLUSTERED 
	(
	WHID
	) ON [PRIMARY]
GO

