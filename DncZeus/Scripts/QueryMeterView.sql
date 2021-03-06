USE [DncZeus]
GO
/****** Object:  StoredProcedure [dbo].[QueryMeterView]    Script Date: 03/18/2021 09:14:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[QueryMeterView]   
	
AS
BEGIN
    declare @name nvarchar(50)
    declare @te nvarchar(50)
    declare @MeterView nvarchar(1000)
    declare dataTemp  cursor for select name from data where name like '%GprsData%'
    open dataTemp
	delete from MeterView
    fetch next from dataTemp into @name
    while @@FETCH_STATUS=0--这里计数会多一个,
    begin
         print @name
		 set @te= @name		
		 set @MeterView='insert into MeterView( SaleRoomName,AreaID, AreaName,GprsCodeA,GprsCodeb,GprsCodec, UptownID, UptownName, BuildID, BuildName, UnitID, UnitName, UserID, UserName, UserAddr, RoomNumber, MeterID, MeterAddr, SYS, BYS, SYRQ, BYRQ, LRRQ, CBWC,DataName)
		 select AreaSaleRoomName,AreaID, AreaName,GprsCodeA,GprsCodeb,GprsCodec, UptownID, UptownName, BuildID, BuildName, UnitID, UnitName, UserID, UserName, UserAddr, RoomNumber, MeterID, MeterAddr, SYS, BYS, SYRQ, BYRQ, LRRQ, CBWC, '''+@te+''' as DataName
		 FROM  OPENDATASOURCE (''SQLOLEDB'', ''Data Source=222.186.212.106,6788;User ID=sa;Password=BGT%VFR$CDE#'' ).'+@te+'.dbo.MeterView '
		 exec( @MeterView)   
		 FETCH NEXT FROM  dataTemp INTO @name
	end
	
	exec dbo.QueryStatistics
	exec dbo.QueryUPStatistics
	
	insert into StatisticsReport( Numberofcustomers, Numberofdistricts,Quantityofwatermeters,Numberofsuccesses, Numberoffailures,Offlinequantity, Numberofnewfiles,Timeofday)
	SELECT COUNT(SaleRoomName) as Numberofcustomers,sum(UptownNumber) as Numberofdistricts,
    SUM(MeterCount) as Numberofsuccesses,SUM(CBWC2Count) as Quantityofwatermeters,SUM(CBWC5Number) as Numberoffailures,
    SUM(CBWC4Count) as Offlinequantity,SUM(CBWC0Count) as Numberofnewfiles,GETDATE() as Timeofday
  FROM [AfterView]
    close dataTemp
    DEALLOCATE dataTemp
END
