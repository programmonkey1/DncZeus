if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MaintainerInformation]') and OBJECTPROPERTY(id, N'IsMaintainerInformation') = 1)
drop table [dbo].[MaintainerInformation]
GO

CREATE TABLE [dbo].[MaintainerInformation] (
	[MIID] [int] NOT NULL ,	--标识
	[AreaName] [char](100) NOT NULL,--区域
	[Post] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--职务
	[Telephone] [char] (50) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL ,	--电话
	[Remarks] [char] (100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL, 	--备注
	[Status] [int] NOT NULL,
	[IsDeleted] [int] NOT NULL

) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MaintainerInformation] ADD CONSTRAINT
	PK_MaintainerInformation PRIMARY KEY CLUSTERED 
	(
	MIID
	) ON [PRIMARY]
GO

