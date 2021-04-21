if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Recovery]') and OBJECTPROPERTY(id, N'IsRecovery') = 1)
drop table [dbo].[Recovery]
GO

CREATE TABLE [dbo].[Recovery] (
	[RID] [int] NOT NULL ,	--��ʶ
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--�豸���
	[TimeEnter] [datetime] DEFAULT 0 NOT NULL ,	--���ʱ��
	[Number] [int] NOT NULL,--����
	[FaultType] [tinyint] DEFAULT 0 NOT NULL ,	--�������� 0δ֪,1����,2������3��Ʒ
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--��ע
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Recovery] ADD CONSTRAINT
	PK_Recovery PRIMARY KEY CLUSTERED 
	(
	RID
	) ON [PRIMARY]
GO

