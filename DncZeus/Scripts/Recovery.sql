if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Recovery]') and OBJECTPROPERTY(id, N'IsRecovery') = 1)
drop table [dbo].[Recovery]
GO

CREATE TABLE [dbo].[Recovery] (
	[RID] [int] NOT NULL ,	--标识
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--设备编号
	[TimeEnter] [datetime] DEFAULT 0 NOT NULL ,	--入库时间
	[Number] [int] NOT NULL,--数量
	[FaultType] [tinyint] DEFAULT 0 NOT NULL ,	--故障类型 0未知,1故障,2正常，3新品
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--备注
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

