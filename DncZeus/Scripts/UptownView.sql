USE [DncZeus]
GO
/****** Object:  StoredProcedure [dbo].[QueryUPStatistics]   Script Date: 03/15/2021 15:45:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[QueryUPStatistics] 
	AS
BEGIN
    delete from dbo.UptownView
    insert into UptownView([DataName],[SaleRoomName],[Uptownname],[UptownNumber],[MeterCount],[CBWC2Count],[SevenDP],[OneMP],[TwoMP]
      ,[ThreeMP],[CBWC5Number],[ToDS],[OneDS],[TwoDS],[ThreeDS],[FourDS],[FiveDS],[SixDS],[SevenDS],[OneWS]
      ,[TwoWS],[ThreeWS],[OneMS],[TwoMS],[ThreeMs],[CBWC0Count],[CBWC1Count],[CBWC3Count],[CBWC4Count])

	SELECT DataName, SaleRoomName ,Uptownname,
    count(distinct UptownID) as UptownNumber,
    count(MeterID) as MeterCount,
    COUNT(case when CBWC=2 then CBWC end) as CBWC2Count,
--LTRIM(STR(COUNT(case when CBWC=2 then CBWC end)*100.0/count(MeterID),6,3))+'%' as ³­±íÂÊ,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ  end)*100.0/count(isnull(MeterID,0))) as SevenDP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end)*100.0/count(isnull(MeterID,0))) as OneMP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end)*100.0/count(isnull(MeterID,0))) as TwoMP,
    CONVERT(decimal,COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end)*100.0/count(isnull(MeterID,0))) as ThreeMP,
    COUNT(case when CBWC=5 then CBWC end) as CBWC5Number,
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())=0 then BYRQ end) as 'ToDS',--one two three four five six seven eight nine
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<0 then BYRQ end) as 'OneDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<1 then BYRQ end) as 'TwoDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<2 then BYRQ end) as 'ThreeDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<3 then BYRQ end) as 'FourDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<4 then BYRQ end) as 'FiveDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<5 then BYRQ end) as 'SixDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<6 then BYRQ end) as 'SevenDS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=7 then BYRQ end) as 'OneWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=14 then BYRQ end) as 'TwoWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=21 then BYRQ end) as 'ThreeWS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=30 then BYRQ end) as 'OneMS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=60 then BYRQ end) as 'TwoMS',
    COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=90 then BYRQ end) as 'ThreeMs',
    COUNT(case when CBWC=0 then CBWC end) as CBWC0Count,
    COUNT(case when CBWC=1 then CBWC end) as CBWC1Count,
    COUNT(case when CBWC=3 then CBWC end) as CBWC3Count,
    COUNT(case when CBWC=4 then CBWC end) as CBWC4Count
   
    FROM MeterView 
    GROUP BY DataName, SaleRoomName,Uptownname
--HAVING COUNT(case when datediff(dd,convert(varchar(100),BYRQ,23),getdate())<=120 then BYRQ end)>0
    order by SaleRoomName desc
    

    select * from dbo.UptownView order by SaleRoomName desc
END
  


