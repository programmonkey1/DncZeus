if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Warehouse]') and OBJECTPROPERTY(id, N'IsWarehouse') = 1)
drop table [dbo].[Warehouse]
GO

CREATE TABLE [dbo].[Warehouse] (
	[WHID] [int] NOT NULL ,	--标识
	[EUNumber] [char] (20) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--设备编号
	[TimeOut] [datetime] DEFAULT 0 NOT NULL ,	--出库时间
	[TimeEnter] [datetime] DEFAULT 0 NOT NULL ,	--入库时间	
	[StateType] [tinyint] DEFAULT 0 NOT NULL ,	--状态类型 0未知,1未使用,2使用
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--备注
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

