using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Models.Response;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DncZeus.Api.Utils;
using DncZeus.Api.ViewModels.Rbac.DncWorkTask;
using DncZeus.Api.RequestPayload.Rbac.WorkTask;
using DncZeus.Api.RequestPayload.Rbac.DataList;
using System.Text.RegularExpressions;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class WorkTaskController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public WorkTaskController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(WorkTaskRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //var list = _dbContext.DncWorkTask.ToList();
                List<string> alldaylist = new List<string>();
                List<string> nextmonthdaylist = new List<string>();
                List<string> daylist = new List<string>();
                List<string> delaylist = new List<string>();
                DateTime day1, day2, day3, nextmonthday;
                string completionEndTime, tasktime;
                double intResult = 0;
                /*System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));*/
                DateTime nowday = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                //对日期做判断
                var timelist = _dbContext.DncWorkTask.ToList();

                for (int i = 0; i < timelist.ToArray().Length; i++)
                {
                    //datepicker取值为UTC时间转化为北京时间(北京时间=UTC时间+8小时 )

                    day1 = Convert.ToDateTime(timelist[i].CompletionFirstTime);
                    day3 = Convert.ToDateTime(timelist[i].CompletionFirstTime);
                    day2 = Convert.ToDateTime(timelist[i].CompletionEndTime);
                    completionEndTime = day1.ToString("yyyy-MM-dd") + " - " + day2.ToString("yyyy-MM-dd");
                    tasktime = timelist[i].TaskTime.ToString().Replace("T", " ");
                    //2021-04-08 - 2021-04-28
                    //首先保证 time1<=time2
                    if (timelist[i].IsFinished == 1)
                    {
                        timelist[i].CompletionTime = completionEndTime;
                    }
                    else if (timelist[i].IsFinished == 0 && (timelist[i].PlanList.Contains('0') || timelist[i].PlanList.Contains('1') || timelist[i].PlanList.Contains('2')))
                    {
                        timelist[i].CompletionTime = completionEndTime;
                        if (day1 <= nowday && nowday <= day2 || day1 > nowday)
                        {
                            daylist.Clear();
                            delaylist.Clear();
                            alldaylist.Clear();
                            while (day3 <= day2)
                            {
                                alldaylist.Add(day3.ToString("yyyy-MM-dd"));
                                day3 = day3.AddDays(1);
                            }
                            for (int alldays = 0; alldays < alldaylist.ToArray().Length; alldays++)
                            {
                                if (alldaylist[alldays].Substring(8, 2) == "01")
                                {
                                    timelist[i].No1 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "02")
                                {
                                    timelist[i].No2 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "03")
                                {
                                    timelist[i].No3 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "04")
                                {
                                    timelist[i].No4 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "05")
                                {
                                    timelist[i].No5 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "06")
                                {
                                    timelist[i].No6 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "07")
                                {
                                    timelist[i].No7 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "08")
                                {
                                    timelist[i].No8 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "09")
                                {
                                    timelist[i].No9 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "10")
                                {
                                    timelist[i].No10 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "11")
                                {
                                    timelist[i].No11 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "12")
                                {
                                    timelist[i].No12 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "13")
                                {
                                    timelist[i].No13 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "14")
                                {
                                    timelist[i].No14 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "15")
                                {
                                    timelist[i].No15 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "16")
                                {
                                    timelist[i].No16 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "17")
                                {
                                    timelist[i].No17 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "18")
                                {
                                    timelist[i].No18 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "19")
                                {
                                    timelist[i].No19 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "20")
                                {
                                    timelist[i].No20 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "21")
                                {
                                    timelist[i].No21 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "22")
                                {
                                    timelist[i].No22 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "23")
                                {
                                    timelist[i].No23 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "24")
                                {
                                    timelist[i].No24 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "25")
                                {
                                    timelist[i].No25 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "26")
                                {
                                    timelist[i].No26 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "27")
                                {
                                    timelist[i].No27 = 3;
                                }
                                int year = int.Parse(alldaylist[alldays].Substring(0, 4));
                                if ((year % 400 == 0 || year % 4 == 0 && year % 100 != 0) && alldaylist[alldays].Substring(5, 2) == "02")
                                {
                                    if (alldaylist[alldays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 3;
                                    }
                                }
                                else if (alldaylist[alldays].Substring(5, 2) == "02")
                                {
                                    if (alldaylist[alldays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 3;
                                    }
                                }
                                else if (alldaylist[alldays].Substring(5, 2) == "01" || alldaylist[alldays].Substring(5, 2) == "03" || alldaylist[alldays].Substring(5, 2) == "05" || alldaylist[alldays].Substring(5, 2) == "07" || alldaylist[alldays].Substring(5, 2) == "08" || alldaylist[alldays].Substring(5, 2) == "10" || alldaylist[alldays].Substring(5, 2) == "12")
                                {
                                    if (alldaylist[alldays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "30")
                                    {
                                        timelist[i].No30 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "31")
                                    {
                                        timelist[i].No31 = 3;
                                    }
                                }
                                else if (alldaylist[alldays].Substring(5, 2) == "04" || alldaylist[alldays].Substring(5, 2) == "06" || alldaylist[alldays].Substring(5, 2) == "09" || alldaylist[alldays].Substring(5, 2) == "11")
                                {
                                    if (alldaylist[alldays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 3;
                                    }
                                    if (alldaylist[alldays].Substring(8, 2) == "30")
                                    {
                                        timelist[i].No30 = 3;
                                    }
                                }
                            }

                            if (day1 <= nowday && nowday < day2 || day1 > nowday)
                            {
                                timelist[i].ProgressDeviation = "正在完成中";
                            }
                            else if (nowday == day2)
                            {
                                timelist[i].ProgressDeviation = "任务最后一天";
                            }
                            while (day1 <= nowday)
                            {
                                daylist.Add(day1.ToString("yyyy-MM-dd"));
                                day1 = day1.AddDays(1);
                            }
                            for (int days = 0; days < daylist.ToArray().Length; days++)
                            {
                                if (daylist[days].Substring(8, 2) == "01")
                                {
                                    timelist[i].No1 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "02")
                                {
                                    timelist[i].No2 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "03")
                                {
                                    timelist[i].No3 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "04")
                                {
                                    timelist[i].No4 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "05")
                                {
                                    timelist[i].No5 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "06")
                                {
                                    timelist[i].No6 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "07")
                                {
                                    timelist[i].No7 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "08")
                                {
                                    timelist[i].No8 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "09")
                                {
                                    timelist[i].No9 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "10")
                                {
                                    timelist[i].No10 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "11")
                                {
                                    timelist[i].No11 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "12")
                                {
                                    timelist[i].No12 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "13")
                                {
                                    timelist[i].No13 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "14")
                                {
                                    timelist[i].No14 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "15")
                                {
                                    timelist[i].No15 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "16")
                                {
                                    timelist[i].No16 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "17")
                                {
                                    timelist[i].No17 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "18")
                                {
                                    timelist[i].No18 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "19")
                                {
                                    timelist[i].No19 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "20")
                                {
                                    timelist[i].No20 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "21")
                                {
                                    timelist[i].No21 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "22")
                                {
                                    timelist[i].No22 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "23")
                                {
                                    timelist[i].No23 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "24")
                                {
                                    timelist[i].No24 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "25")
                                {
                                    timelist[i].No25 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "26")
                                {
                                    timelist[i].No26 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "27")
                                {
                                    timelist[i].No27 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "29")
                                {
                                    timelist[i].No29 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "30")
                                {
                                    timelist[i].No30 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "31")
                                {
                                    timelist[i].No31 = 1;
                                }
                            }
                            string sql = "UPDATE DncWorkTask SET PlanList = '" + timelist[i].PlanList + "',NO1 = '" + timelist[i].No1 + "',NO2 = '" + timelist[i].No2 + "',NO3 = '" + timelist[i].No3 + "',NO4 = '" + timelist[i].No4 + "',NO5 = '" + timelist[i].No5 + "',NO6 = '" + timelist[i].No6 + "',NO7 = '" + timelist[i].No7 + "',NO8 = '" + timelist[i].No8 + "',NO9 = '" + timelist[i].No9 + "',NO10 = '" + timelist[i].No10 + "',NO11 = '" + timelist[i].No11 + "',NO12 = '" + timelist[i].No12 + "',NO13 = '" + timelist[i].No13 + "',NO14 = '" + timelist[i].No14 + "',NO15 = '" + timelist[i].No15 + "',NO16 = '" + timelist[i].No16 + "',NO17 = '" + timelist[i].No17 + "',NO18 = '" + timelist[i].No18 + "',NO19 = '" + timelist[i].No19 + "',NO20 = '" + timelist[i].No20 + "',NO21 = '" + timelist[i].No21 + "',NO22 = '" + timelist[i].No22 + "',NO23 = '" + timelist[i].No23 + "',NO24 = '" + timelist[i].No24 + "',NO25 = '" + timelist[i].No25 + "',NO26 = '" + timelist[i].No26 + "',NO27 = '" + timelist[i].No27 + "',NO28 = '" + timelist[i].No28 + "',NO29 = '" + timelist[i].No29 + "',NO30 = '" + timelist[i].No30 + "',NO31 = '" + timelist[i].No31 + "',ProgressDeviation ='" + timelist[i].ProgressDeviation + "' WHERE id = '" + (i + 1) + "'";
                            _dbContext.Database.ExecuteSqlCommand(sql);
                        }

                        if (nowday > day2)
                        {
                            timelist[i].ProgressDeviation = "已逾期";
                            if (nowday > day2.AddDays(1 - day2.Day).AddMonths(1).AddDays(-1) && timelist[i].PlanList.Contains('0'))
                            {
                                string isinsertsql = "UPDATE DncWorkTask SET PlanList = '1' WHERE id = '" + (i + 1) + "'";
                                _dbContext.Database.ExecuteSqlCommand(isinsertsql);

                                string insertsql = "INSERT INTO [DncZeus].[dbo].[DncWorkTask] ([TaskTheme],[TaskContent],[WorkType] ,[TaskPerson] ,[Telephone] ,[TaskTime] ,[CompletionFirstTime] ,[CompletionEndTime] ,[CompletionTime] ,[TaskPlan] ,[PlanList] ,[NO1] ,[NO2] ,[NO3] ,[NO4] ,[NO5] ,[NO6] ,[NO7] ,[NO8] ,[NO9] ,[NO10] ,[NO11] ,[NO12] ,[NO13] ,[NO14] ,[NO15] ,[NO16] ,[NO17] ,[NO18] ,[NO19] ,[NO20] ,[NO21] ,[NO22] ,[NO23] ,[NO24] ,[NO25] ,[NO26] ,[NO27] ,[NO28] ,[NO29] ,[NO30] ,[NO31] ,[ProgressDeviation] ,[InformationNote] ,[ThirdPartyCooperation] ,[MattersNeedingAttention] ,[ProjectManager] ,[Publisher] ,[IsFinished] ,[Status] ,[IsDeleted],[Code]) VALUES('" + timelist[i].TaskTheme + "','" + timelist[i].TaskContent + "','" + timelist[i].WorkType + "','" + timelist[i].TaskPerson + "','" + timelist[i].Telephone + "','" + timelist[i].TaskTime + "','" + timelist[i].CompletionFirstTime + "','" + timelist[i].CompletionEndTime + "','" + timelist[i].CompletionTime + "','" + (int.Parse(timelist[i].TaskPlan.Substring(0, 1)) + 1) + "月" + "','2',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'" + timelist[i].ProgressDeviation + "','" + timelist[i].InformationNote + "','" + timelist[i].ThirdPartyCooperation + "','" + timelist[i].MattersNeedingAttention + "','" + timelist[i].ProjectManager + "','" + timelist[i].Publisher + "','" + timelist[i].IsFinished + "',0,0,'" + timelist[i].Code + "')";
                                _dbContext.Database.ExecuteSqlCommand(insertsql);

                                nextmonthday = day2.AddDays(1 - day2.Day).AddMonths(1);
                                while (nextmonthday <= nowday)
                                {
                                    nextmonthdaylist.Add(nextmonthday.ToString("yyyy-MM-dd"));
                                    nextmonthday = nextmonthday.AddDays(1);
                                }
                                for (int nextmonthdays = 0; nextmonthdays < nextmonthdaylist.ToArray().Length; nextmonthdays++)
                                {
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "01")
                                    {
                                        timelist[i].No1 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "02")
                                    {
                                        timelist[i].No2 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "03")
                                    {
                                        timelist[i].No3 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "04")
                                    {
                                        timelist[i].No4 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "05")
                                    {
                                        timelist[i].No5 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "06")
                                    {
                                        timelist[i].No6 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "07")
                                    {
                                        timelist[i].No7 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "08")
                                    {
                                        timelist[i].No8 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "09")
                                    {
                                        timelist[i].No9 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "10")
                                    {
                                        timelist[i].No10 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "11")
                                    {
                                        timelist[i].No11 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "12")
                                    {
                                        timelist[i].No12 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "13")
                                    {
                                        timelist[i].No13 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "14")
                                    {
                                        timelist[i].No14 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "15")
                                    {
                                        timelist[i].No15 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "16")
                                    {
                                        timelist[i].No16 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "17")
                                    {
                                        timelist[i].No17 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "18")
                                    {
                                        timelist[i].No18 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "19")
                                    {
                                        timelist[i].No19 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "20")
                                    {
                                        timelist[i].No20 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "21")
                                    {
                                        timelist[i].No21 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "22")
                                    {
                                        timelist[i].No22 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "23")
                                    {
                                        timelist[i].No23 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "24")
                                    {
                                        timelist[i].No24 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "25")
                                    {
                                        timelist[i].No25 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "26")
                                    {
                                        timelist[i].No26 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "27")
                                    {
                                        timelist[i].No27 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "30")
                                    {
                                        timelist[i].No30 = 2;
                                    }
                                    if (nextmonthdaylist[nextmonthdays].Substring(8, 2) == "31")
                                    {
                                        timelist[i].No31 = 2;
                                    }
                                }
                                string nextmonthdaysql = "UPDATE DncWorkTask SET NO1 = '" + timelist[i].No1 + "',NO2 = '" + timelist[i].No2 + "',NO3 = '" + timelist[i].No3 + "',NO4 = '" + timelist[i].No4 + "',NO5 = '" + timelist[i].No5 + "',NO6 = '" + timelist[i].No6 + "',NO7 = '" + timelist[i].No7 + "',NO8 = '" + timelist[i].No8 + "',NO9 = '" + timelist[i].No9 + "',NO10 = '" + timelist[i].No10 + "',NO11 = '" + timelist[i].No11 + "',NO12 = '" + timelist[i].No12 + "',NO13 = '" + timelist[i].No13 + "',NO14 = '" + timelist[i].No14 + "',NO15 = '" + timelist[i].No15 + "',NO16 = '" + timelist[i].No16 + "',NO17 = '" + timelist[i].No17 + "',NO18 = '" + timelist[i].No18 + "',NO19 = '" + timelist[i].No19 + "',NO20 = '" + timelist[i].No20 + "',NO21 = '" + timelist[i].No21 + "',NO22 = '" + timelist[i].No22 + "',NO23 = '" + timelist[i].No23 + "',NO24 = '" + timelist[i].No24 + "',NO25 = '" + timelist[i].No25 + "',NO26 = '" + timelist[i].No26 + "',NO27 = '" + timelist[i].No27 + "',NO28 = '" + timelist[i].No28 + "',NO29 = '" + timelist[i].No29 + "',NO30 = '" + timelist[i].No30 + "',NO31 = '" + timelist[i].No31 + "',ProgressDeviation ='" + timelist[i].ProgressDeviation + "' WHERE id = '" + (i + 2) + "'";
                                _dbContext.Database.ExecuteSqlCommand(nextmonthdaysql);

                                timelist[i].No1 = 0; timelist[i].No2 = 0; timelist[i].No3 = 0; timelist[i].No4 = 0; timelist[i].No5 = 0; timelist[i].No6 = 0; timelist[i].No7 = 0; timelist[i].No8 = 0; timelist[i].No9 = 0; timelist[i].No10 = 0; timelist[i].No11 = 0; timelist[i].No12 = 0; timelist[i].No13 = 0; timelist[i].No14 = 0; timelist[i].No15 = 0; timelist[i].No16 = 0; timelist[i].No17 = 0; timelist[i].No18 = 0; timelist[i].No19 = 0; timelist[i].No20 = 0; timelist[i].No21 = 0; timelist[i].No22 = 0; timelist[i].No23 = 0; timelist[i].No24 = 0; timelist[i].No25 = 0; timelist[i].No26 = 0; timelist[i].No27 = 0; timelist[i].No28 = 0; timelist[i].No29 = 0; timelist[i].No30 = 0; timelist[i].No31 = 0;
                            }
                            daylist.Clear();
                            delaylist.Clear();
                            nextmonthdaylist.Clear();
                            nextmonthday = day2.AddDays(1 - day2.Day).AddMonths(1);

                            while (day1 <= nowday)
                            {
                                daylist.Add(day1.ToString("yyyy-MM-dd"));
                                day1 = day1.AddDays(1);
                            }
                            while (day2 <= nowday)
                            {
                                delaylist.Add(day2.ToString("yyyy-MM-dd"));
                                day2 = day2.AddDays(1);
                            }

                            for (int days = 0; days < daylist.ToArray().Length; days++)
                            {
                                string firstmonth = daylist[1].Substring(5, 2);
                                string endmonth = daylist[days].Substring(5, 2);
                                if (firstmonth == endmonth && (timelist[i].PlanList.Contains("0") || timelist[i].PlanList.Contains("1")))
                                {
                                    if (daylist[days].Substring(8, 2) == "01")
                                    {
                                        timelist[i].No1 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "02")
                                    {
                                        timelist[i].No2 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "03")
                                    {
                                        timelist[i].No3 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "04")
                                    {
                                        timelist[i].No4 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "05")
                                    {
                                        timelist[i].No5 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "06")
                                    {
                                        timelist[i].No6 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "07")
                                    {
                                        timelist[i].No7 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "08")
                                    {
                                        timelist[i].No8 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "09")
                                    {
                                        timelist[i].No9 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "10")
                                    {
                                        timelist[i].No10 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "11")
                                    {
                                        timelist[i].No11 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "12")
                                    {
                                        timelist[i].No12 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "13")
                                    {
                                        timelist[i].No13 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "14")
                                    {
                                        timelist[i].No14 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "15")
                                    {
                                        timelist[i].No15 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "16")
                                    {
                                        timelist[i].No16 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "17")
                                    {
                                        timelist[i].No17 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "18")
                                    {
                                        timelist[i].No18 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "19")
                                    {
                                        timelist[i].No19 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "20")
                                    {
                                        timelist[i].No20 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "21")
                                    {
                                        timelist[i].No21 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "22")
                                    {
                                        timelist[i].No22 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "23")
                                    {
                                        timelist[i].No23 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "24")
                                    {
                                        timelist[i].No24 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "25")
                                    {
                                        timelist[i].No25 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "26")
                                    {
                                        timelist[i].No26 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "27")
                                    {
                                        timelist[i].No27 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "30")
                                    {
                                        timelist[i].No30 = 1;
                                    }
                                    if (daylist[days].Substring(8, 2) == "31")
                                    {
                                        timelist[i].No31 = 1;
                                    }
                                }
                            }
                            for (int delays = 1; delays < delaylist.ToArray().Length; delays++)
                            {
                                string firstmonth = delaylist[1].Substring(5, 2);
                                string endmonth = delaylist[delays].Substring(5, 2);
                                if (firstmonth == endmonth && (timelist[i].PlanList.Contains("0") || timelist[i].PlanList.Contains("1")))
                                {
                                    if (delaylist[delays].Substring(8, 2) == "01")
                                    {
                                        timelist[i].No1 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "02")
                                    {
                                        timelist[i].No2 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "03")
                                    {
                                        timelist[i].No3 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "04")
                                    {
                                        timelist[i].No4 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "05")
                                    {
                                        timelist[i].No5 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "06")
                                    {
                                        timelist[i].No6 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "07")
                                    {
                                        timelist[i].No7 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "08")
                                    {
                                        timelist[i].No8 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "09")
                                    {
                                        timelist[i].No9 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "10")
                                    {
                                        timelist[i].No10 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "11")
                                    {
                                        timelist[i].No11 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "12")
                                    {
                                        timelist[i].No12 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "13")
                                    {
                                        timelist[i].No13 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "14")
                                    {
                                        timelist[i].No14 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "15")
                                    {
                                        timelist[i].No15 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "16")
                                    {
                                        timelist[i].No16 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "17")
                                    {
                                        timelist[i].No17 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "18")
                                    {
                                        timelist[i].No18 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "19")
                                    {
                                        timelist[i].No19 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "20")
                                    {
                                        timelist[i].No20 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "21")
                                    {
                                        timelist[i].No21 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "22")
                                    {
                                        timelist[i].No22 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "23")
                                    {
                                        timelist[i].No23 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "24")
                                    {
                                        timelist[i].No24 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "25")
                                    {
                                        timelist[i].No25 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "26")
                                    {
                                        timelist[i].No26 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "27")
                                    {
                                        timelist[i].No27 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "28")
                                    {
                                        timelist[i].No28 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "29")
                                    {
                                        timelist[i].No29 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "30")
                                    {
                                        timelist[i].No30 = 2;
                                    }
                                    if (delaylist[delays].Substring(8, 2) == "31")
                                    {
                                        timelist[i].No31 = 2;
                                    }
                                }
                            }
                            if (nowday >= nextmonthday && timelist[i].PlanList.Contains("2"))
                            {
                                while (nextmonthday <= nowday)
                                {
                                    nextmonthdaylist.Add(nextmonthday.ToString("yyyy-MM-dd"));
                                    nextmonthday = nextmonthday.AddDays(1);
                                }
                                for (int monthdays = 0; monthdays < nextmonthdaylist.ToArray().Length; monthdays++)
                                {
                                    /*string firstmonth = nextmonthdaylist[1].Substring(5, 2);
                                    string endmonth = nextmonthdaylist[monthdays].Substring(5, 2);
                                    if (int.Parse(firstmonth) < int.Parse(endmonth) && !timelist[i].PlanList.Contains("2"))
                                    {*/
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "01")
                                        {
                                            timelist[i].No1 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "02")
                                        {
                                            timelist[i].No2 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "03")
                                        {
                                            timelist[i].No3 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "04")
                                        {
                                            timelist[i].No4 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "05")
                                        {
                                            timelist[i].No5 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "06")
                                        {
                                            timelist[i].No6 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "07")
                                        {
                                            timelist[i].No7 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "08")
                                        {
                                            timelist[i].No8 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "09")
                                        {
                                            timelist[i].No9 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "10")
                                        {
                                            timelist[i].No10 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "11")
                                        {
                                            timelist[i].No11 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "12")
                                        {
                                            timelist[i].No12 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "13")
                                        {
                                            timelist[i].No13 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "14")
                                        {
                                            timelist[i].No14 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "15")
                                        {
                                            timelist[i].No15 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "16")
                                        {
                                            timelist[i].No16 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "17")
                                        {
                                            timelist[i].No17 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "18")
                                        {
                                            timelist[i].No18 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "19")
                                        {
                                            timelist[i].No19 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "20")
                                        {
                                            timelist[i].No20 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "21")
                                        {
                                            timelist[i].No21 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "22")
                                        {
                                            timelist[i].No22 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "23")
                                        {
                                            timelist[i].No23 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "24")
                                        {
                                            timelist[i].No24 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "25")
                                        {
                                            timelist[i].No25 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "26")
                                        {
                                            timelist[i].No26 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "27")
                                        {
                                            timelist[i].No27 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "28")
                                        {
                                            timelist[i].No28 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "29")
                                        {
                                            timelist[i].No29 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "30")
                                        {
                                            timelist[i].No30 = 2;
                                        }
                                        if (nextmonthdaylist[monthdays].Substring(8, 2) == "31")
                                        {
                                            timelist[i].No31 = 2;
                                        }
                                    //}
                                }
                            }

                            string sql = "UPDATE DncWorkTask SET NO1 = '" + timelist[i].No1 + "',NO2 = '" + timelist[i].No2 + "',NO3 = '" + timelist[i].No3 + "',NO4 = '" + timelist[i].No4 + "',NO5 = '" + timelist[i].No5 + "',NO6 = '" + timelist[i].No6 + "',NO7 = '" + timelist[i].No7 + "',NO8 = '" + timelist[i].No8 + "',NO9 = '" + timelist[i].No9 + "',NO10 = '" + timelist[i].No10 + "',NO11 = '" + timelist[i].No11 + "',NO12 = '" + timelist[i].No12 + "',NO13 = '" + timelist[i].No13 + "',NO14 = '" + timelist[i].No14 + "',NO15 = '" + timelist[i].No15 + "',NO16 = '" + timelist[i].No16 + "',NO17 = '" + timelist[i].No17 + "',NO18 = '" + timelist[i].No18 + "',NO19 = '" + timelist[i].No19 + "',NO20 = '" + timelist[i].No20 + "',NO21 = '" + timelist[i].No21 + "',NO22 = '" + timelist[i].No22 + "',NO23 = '" + timelist[i].No23 + "',NO24 = '" + timelist[i].No24 + "',NO25 = '" + timelist[i].No25 + "',NO26 = '" + timelist[i].No26 + "',NO27 = '" + timelist[i].No27 + "',NO28 = '" + timelist[i].No28 + "',NO29 = '" + timelist[i].No29 + "',NO30 = '" + timelist[i].No30 + "',NO31 = '" + timelist[i].No31 + "',ProgressDeviation ='" + timelist[i].ProgressDeviation + "' WHERE id = '" + (i + 1) + "'";
                            _dbContext.Database.ExecuteSqlCommand(sql);
                            //}
                        }
                    }
                    else if (timelist[i].IsFinished == 0 && timelist[i].PlanList.Contains('3'))
                    {
                        timelist[i].ProgressDeviation = "正在完成中";
                        daylist.Clear();
                        alldaylist.Clear();
                        day1 = Convert.ToDateTime(timelist[i].CompletionFirstTime);
                        timelist[i].CompletionTime = completionEndTime;
                        while (day1 <= nowday)
                        {
                            daylist.Add(day1.ToString("yyyy-MM-dd"));
                            day1 = day1.AddDays(1);
                        }
                        while (day3 <= day2)
                        {
                            alldaylist.Add(day3.ToString("yyyy-MM-dd"));
                            day3 = day3.AddDays(1);
                        }
                        for (int alldays = 0; alldays < alldaylist.ToArray().Length; alldays++)
                        {
                            if (alldaylist[alldays].Substring(8, 2) == "01")
                            {
                                timelist[i].No1 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "02")
                            {
                                timelist[i].No2 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "03")
                            {
                                timelist[i].No3 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "04")
                            {
                                timelist[i].No4 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "05")
                            {
                                timelist[i].No5 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "06")
                            {
                                timelist[i].No6 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "07")
                            {
                                timelist[i].No7 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "08")
                            {
                                timelist[i].No8 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "09")
                            {
                                timelist[i].No9 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "10")
                            {
                                timelist[i].No10 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "11")
                            {
                                timelist[i].No11 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "12")
                            {
                                timelist[i].No12 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "13")
                            {
                                timelist[i].No13 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "14")
                            {
                                timelist[i].No14 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "15")
                            {
                                timelist[i].No15 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "16")
                            {
                                timelist[i].No16 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "17")
                            {
                                timelist[i].No17 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "18")
                            {
                                timelist[i].No18 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "19")
                            {
                                timelist[i].No19 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "20")
                            {
                                timelist[i].No20 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "21")
                            {
                                timelist[i].No21 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "22")
                            {
                                timelist[i].No22 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "23")
                            {
                                timelist[i].No23 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "24")
                            {
                                timelist[i].No24 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "25")
                            {
                                timelist[i].No25 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "26")
                            {
                                timelist[i].No26 = 3;
                            }
                            if (alldaylist[alldays].Substring(8, 2) == "27")
                            {
                                timelist[i].No27 = 3;
                            }
                            int year = int.Parse(alldaylist[alldays].Substring(0, 4));
                            if ((year % 400 == 0 || year % 4 == 0 && year % 100 != 0) && alldaylist[alldays].Substring(5, 2) == "02")
                            {
                                if (alldaylist[alldays].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "29")
                                {
                                    timelist[i].No29 = 3;
                                }
                            }
                            else if (alldaylist[alldays].Substring(5, 2) == "02")
                            {
                                if (alldaylist[alldays].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 3;
                                }
                            }
                            else if (alldaylist[alldays].Substring(5, 2) == "01" || alldaylist[alldays].Substring(5, 2) == "03" || alldaylist[alldays].Substring(5, 2) == "05" || alldaylist[alldays].Substring(5, 2) == "07" || alldaylist[alldays].Substring(5, 2) == "08" || alldaylist[alldays].Substring(5, 2) == "10" || alldaylist[alldays].Substring(5, 2) == "12")
                            {
                                if (alldaylist[alldays].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "29")
                                {
                                    timelist[i].No29 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "30")
                                {
                                    timelist[i].No30 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "31")
                                {
                                    timelist[i].No31 = 3;
                                }
                            }
                            else if (alldaylist[alldays].Substring(5, 2) == "04" || alldaylist[alldays].Substring(5, 2) == "06" || alldaylist[alldays].Substring(5, 2) == "09" || alldaylist[alldays].Substring(5, 2) == "11")
                            {
                                if (alldaylist[alldays].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "29")
                                {
                                    timelist[i].No29 = 3;
                                }
                                if (alldaylist[alldays].Substring(8, 2) == "30")
                                {
                                    timelist[i].No30 = 3;
                                }
                            }
                        }
                        for (int days = 0; days < daylist.ToArray().Length; days++)
                        {
                            string firstmonth = daylist[0].Substring(5, 2);
                            string endmonth = daylist[days].Substring(5, 2);
                            if (firstmonth == endmonth)
                            {
                                if (daylist[days].Substring(8, 2) == "01")
                                {
                                    timelist[i].No1 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "02")
                                {
                                    timelist[i].No2 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "03")
                                {
                                    timelist[i].No3 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "04")
                                {
                                    timelist[i].No4 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "05")
                                {
                                    timelist[i].No5 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "06")
                                {
                                    timelist[i].No6 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "07")
                                {
                                    timelist[i].No7 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "08")
                                {
                                    timelist[i].No8 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "09")
                                {
                                    timelist[i].No9 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "10")
                                {
                                    timelist[i].No10 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "11")
                                {
                                    timelist[i].No11 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "12")
                                {
                                    timelist[i].No12 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "13")
                                {
                                    timelist[i].No13 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "14")
                                {
                                    timelist[i].No14 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "15")
                                {
                                    timelist[i].No15 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "16")
                                {
                                    timelist[i].No16 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "17")
                                {
                                    timelist[i].No17 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "18")
                                {
                                    timelist[i].No18 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "19")
                                {
                                    timelist[i].No19 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "20")
                                {
                                    timelist[i].No20 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "21")
                                {
                                    timelist[i].No21 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "22")
                                {
                                    timelist[i].No22 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "23")
                                {
                                    timelist[i].No23 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "24")
                                {
                                    timelist[i].No24 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "25")
                                {
                                    timelist[i].No25 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "26")
                                {
                                    timelist[i].No26 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "27")
                                {
                                    timelist[i].No27 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "28")
                                {
                                    timelist[i].No28 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "29")
                                {
                                    timelist[i].No29 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "30")
                                {
                                    timelist[i].No30 = 1;
                                }
                                if (daylist[days].Substring(8, 2) == "31")
                                {
                                    timelist[i].No31 = 1;
                                }
                            }
                        }
                        string sql = "UPDATE DncWorkTask SET NO1 = '" + timelist[i].No1 + "',NO2 = '" + timelist[i].No2 + "',NO3 = '" + timelist[i].No3 + "',NO4 = '" + timelist[i].No4 + "',NO5 = '" + timelist[i].No5 + "',NO6 = '" + timelist[i].No6 + "',NO7 = '" + timelist[i].No7 + "',NO8 = '" + timelist[i].No8 + "',NO9 = '" + timelist[i].No9 + "',NO10 = '" + timelist[i].No10 + "',NO11 = '" + timelist[i].No11 + "',NO12 = '" + timelist[i].No12 + "',NO13 = '" + timelist[i].No13 + "',NO14 = '" + timelist[i].No14 + "',NO15 = '" + timelist[i].No15 + "',NO16 = '" + timelist[i].No16 + "',NO17 = '" + timelist[i].No17 + "',NO18 = '" + timelist[i].No18 + "',NO19 = '" + timelist[i].No19 + "',NO20 = '" + timelist[i].No20 + "',NO21 = '" + timelist[i].No21 + "',NO22 = '" + timelist[i].No22 + "',NO23 = '" + timelist[i].No23 + "',NO24 = '" + timelist[i].No24 + "',NO25 = '" + timelist[i].No25 + "',NO26 = '" + timelist[i].No26 + "',NO27 = '" + timelist[i].No27 + "',NO28 = '" + timelist[i].No28 + "',NO29 = '" + timelist[i].No29 + "',NO30 = '" + timelist[i].No30 + "',NO31 = '" + timelist[i].No31 + "',ProgressDeviation ='" + timelist[i].ProgressDeviation + "' WHERE id = '" + (i + 1) + "'";
                        _dbContext.Database.ExecuteSqlCommand(sql);
                    }
                }

                var query = _dbContext.DncWorkTask.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.TaskContent.Contains(payload.Kw.Trim()) || x.TaskPerson.Contains(payload.Kw.Trim()));
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == payload.IsDeleted);
                }
                if (payload.Status > CommonEnum.Status.All)
                {
                    query = query.Where(x => x.Status == payload.Status);
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                //获取list中的值对DncWorkTask赋值
                var data = list.Select(_mapper.Map<DncWorkTask, WorkTaskJsonModel>);
                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/worktask/find_list_by_kw/{kw}")]
        public IActionResult FindByKeyword(string kw)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            if (string.IsNullOrEmpty(kw))
            {
                response.SetFailed("没有查询到数据");
                return Ok(response);
            }
            using (_dbContext)
            {

                var query = _dbContext.DncWorkTask.Where(x => x.TaskContent.Contains(kw));

                var list = query.ToList();
                var data = list.Select(x => new { x.Code, x.TaskContent, x.TaskTime });

                response.SetData(data);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="model">主题视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Create(WorkTaskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.TaskTheme.Trim().Length <= 0)
            {
                response.SetFailed("请输入主题");
                return Ok(response);
            }
            if (model.CompletionFirstTime.Length <= 0 || model.CompletionEndTime.Length <= 0)
            {
                response.SetFailed("请选择日期");
                return Ok(response);
            }
            if (model.TaskContent.Trim().Length <= 0)
            {
                response.SetFailed("请输入任务内容");
                return Ok(response);
            }
            if (model.WorkType == null)
            {
                response.SetFailed("请选择任务重要程度");
                return Ok(response);
            }
            if (model.TaskPerson.Length <= 0)
            {
                response.SetFailed("请输入任务人");
                return Ok(response);
            }
            if (model.Telephone.Length <= 0)
            {
                response.SetFailed("请输入正确的联系电话");
                return Ok(response);
            }
            //电信手机号码正则
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex regexDX = new Regex(dianxin);
            //联通手机号码正则
            string liantong = @"^1[34578][01256]\d{8}";
            Regex regexLT = new Regex(dianxin);
            //移动手机号码正则
            string yidong = @"^(1[012345678]\d{8}|1[345678][012356789]\d{8})$";
            Regex regexYD = new Regex(dianxin);
            if (regexDX.IsMatch(model.Telephone) || regexLT.IsMatch(model.Telephone) || regexYD.IsMatch(model.Telephone))
            { }
            else
            {
                response.SetFailed("请输入正确的联系电话");
                return Ok(response);
            }
            if (model.ProjectManager.Length <= 0)
            {
                response.SetFailed("请输入经理");
                return Ok(response);
            }
            if (model.Publisher.Length <= 0)
            {
                response.SetFailed("请输入发布人");
                return Ok(response);
            }
            string firsttime, endtime;
            DateTime firstdatetime, enddatetime;

            using (_dbContext)
            {
                if (Convert.ToDateTime(model.CompletionFirstTime) > Convert.ToDateTime(model.CompletionEndTime))
                {
                    response.SetFailed("日期录入错误");
                    return Ok(response);
                }
                if (_dbContext.DncWorkTask.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("该任务已存在");
                    return Ok(response);
                }
                string onlycode = RandomHelper.GetRandomizer(8, true, false, true, true);
                List<WorkTasktimeModel> timelist = new List<WorkTasktimeModel>();
                int monthtime = int.Parse(Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd").Substring(5, 2)) - int.Parse(Convert.ToDateTime(model.CompletionFirstTime).ToString("yyyy-MM-dd").Substring(5, 2));
                if (monthtime >= 1)
                {
                    if (monthtime == 1)
                    {
                        for (int i = 0; i < monthtime; i++)
                        {
                            if (i == 0)
                            {
                                firstdatetime = Convert.ToDateTime(model.CompletionFirstTime);
                                firsttime = firstdatetime.ToString("yyyy-MM-dd");
                                endtime = firstdatetime.AddDays(1 - firstdatetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                                entity.TaskPlan = int.Parse(firstdatetime.ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                                entity.CompletionFirstTime = firsttime;
                                entity.CompletionEndTime = endtime;
                                entity.Code = onlycode;
                                entity.TaskTime = DateTime.Now.ToString();
                                entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                                entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                                entity.PlanList = 3 + "";
                                _dbContext.DncWorkTask.Add(entity);
                                _dbContext.SaveChanges();
                                response.SetSuccess();
                            }
                            else if (i == monthtime - 1)
                            {
                                enddatetime = Convert.ToDateTime(model.CompletionEndTime);
                                firsttime = enddatetime.AddDays(1 - enddatetime.Day).ToString("yyyy-MM-dd");
                                endtime = enddatetime.ToString("yyyy-MM-dd");
                                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                                entity.CompletionFirstTime = firsttime;
                                entity.CompletionEndTime = endtime;
                                entity.TaskPlan = int.Parse(enddatetime.ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                                entity.Code = onlycode;
                                entity.TaskTime = DateTime.Now.ToString();
                                entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                                entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                                entity.PlanList = 0 + "";
                                _dbContext.DncWorkTask.Add(entity);
                                _dbContext.SaveChanges();
                                response.SetSuccess();
                            }
                        }
                        return Ok(response);
                    }
                    if (monthtime > 1)
                    {
                        List<int> monthlist = new List<int>();
                        int months = int.Parse(Convert.ToDateTime(model.CompletionFirstTime).ToString("yyyy-MM-dd").Substring(5, 2));

                        while (months <= int.Parse(Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd").Substring(5, 2)))
                        {
                            monthlist.Add(months);
                            months = (months + 1);
                        }
                        for (int i = 0; i < monthtime; i++)
                        {
                            if (i == 0)
                            {
                                firstdatetime = Convert.ToDateTime(model.CompletionFirstTime);
                                firsttime = firstdatetime.ToString("yyyy-MM-dd");
                                endtime = firstdatetime.AddDays(1 - firstdatetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                                entity.TaskPlan = int.Parse(firstdatetime.ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                                entity.CompletionFirstTime = firsttime;
                                entity.CompletionEndTime = endtime;
                                entity.Code = onlycode;
                                entity.TaskTime = DateTime.Now.ToString();
                                entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                                entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                                entity.PlanList = 3 + "";
                                _dbContext.DncWorkTask.Add(entity);
                                _dbContext.SaveChanges();
                                response.SetSuccess();
                            }
                            if (i > 0 && i < monthtime)
                            {
                                firstdatetime = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + monthlist[i] + "-01");
                                firsttime = firstdatetime.AddDays(1 - firstdatetime.Day).ToString("yyyy-MM-dd");
                                endtime = firstdatetime.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                                entity.CompletionFirstTime = firsttime;
                                entity.CompletionEndTime = endtime;
                                entity.TaskPlan = int.Parse(firstdatetime.ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                                entity.Code = onlycode;
                                entity.TaskTime = DateTime.Now.ToString();
                                entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                                entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                                entity.PlanList = 3 + "";
                                _dbContext.DncWorkTask.Add(entity);
                                _dbContext.SaveChanges();
                                response.SetSuccess();
                            }
                            if (i == monthtime - 1)
                            {
                                enddatetime = Convert.ToDateTime(model.CompletionEndTime);
                                firsttime = enddatetime.AddDays(1 - enddatetime.Day).ToString("yyyy-MM-dd");
                                endtime = enddatetime.ToString("yyyy-MM-dd");
                                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                                entity.CompletionFirstTime = firsttime;
                                entity.CompletionEndTime = endtime;
                                entity.TaskPlan = int.Parse(enddatetime.ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                                entity.Code = onlycode;
                                entity.TaskTime = DateTime.Now.ToString();
                                entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                                entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                                entity.PlanList = 0 + "";
                                _dbContext.DncWorkTask.Add(entity);
                                _dbContext.SaveChanges();
                                response.SetSuccess();
                            }
                        }
                        return Ok(response);
                    }
                }
                else if (int.Parse(Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd").Substring(5, 2)) - int.Parse(Convert.ToDateTime(model.CompletionFirstTime).ToString("yyyy-MM-dd").Substring(5, 2)) == 0)
                {
                    var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                    entity.CompletionFirstTime = Convert.ToDateTime(model.CompletionFirstTime).ToString("yyyy-MM-dd");
                    entity.CompletionEndTime = Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd");
                    entity.Code = onlycode;
                    entity.TaskPlan = int.Parse(Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd").Substring(5, 2)) + "月";
                    entity.TaskTime = DateTime.Now.ToString();
                    entity.ThirdPartyCooperation = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.ThirdPartyCooperation;
                    entity.MattersNeedingAttention = DateTime.Now.ToString("yyyy-MM-dd") + " " + model.MattersNeedingAttention;
                    entity.PlanList = 0 + "";
                    _dbContext.DncWorkTask.Add(entity);
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                    return Ok(response);
                }
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑任务主题
        /// </summary>
        /// <param name="Code">主题惟一编码</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Edit/5
        public ActionResult Edit(string code)
        {
            using (_dbContext)
            {
                if (_dbContext.DncWorkTask.Count(x => x.Code == code) > 1)
                {
                    var response = ResponseModelFactory.CreateInstance;
                    for (int i = 0; i < _dbContext.DncWorkTask.Count(x => x.Code == code); i++)
                    {
                        var entitylist = _dbContext.DncWorkTask.Where(x => x.Code == code);
                        var entity = entitylist.ToList<DncWorkTask>()[i];
                        response.SetData(_mapper.Map<DncWorkTask, WorkTaskCreateViewModel>(entity));
                    }
                    return Ok(response);
                }
                else
                {
                    var entity = _dbContext.DncWorkTask.FirstOrDefault(x => x.Code == code);
                    var response = ResponseModelFactory.CreateInstance;
                    response.SetData(_mapper.Map<DncWorkTask, WorkTaskCreateViewModel>(entity));
                    return Ok(response);
                }
            }
        }

        /// <summary>
        /// 保存编辑后的主题信息
        /// </summary>
        /// <param name="model">主题视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Edit(WorkTaskCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.DncWorkTask.Count(x => x.Code == model.Code) == 0)
                {
                    response.SetFailed("主题不存在");
                    return Ok(response);
                }
                if (_dbContext.DncWorkTask.Count(x => x.Code == model.Code) > 1)
                {
                    for (int i = 0; i < _dbContext.DncWorkTask.Count(x => x.Code == model.Code); i++)
                    {
                        List<DncWorkTask> entitylist = _dbContext.DncWorkTask.Where(x => x.Code == model.Code).ToList();
                        var entity = entitylist[i];
                        entity.TaskTheme = model.TaskTheme;
                        entity.TaskContent = model.TaskContent;
                        entity.WorkType = model.WorkType;
                        entity.TaskPerson = model.TaskPerson;
                        entity.Telephone = model.Telephone;
                        entity.TaskTime = DateTime.Now.ToString();
                        entity.CompletionTime = model.CompletionTime;
                        //entity.TaskPlan = model.TaskPlan;
                        entity.ProgressDeviation = model.ProgressDeviation;
                        entity.InformationNote = model.InformationNote;
                        entity.ThirdPartyCooperation += "\r\n" + (DateTime.Now.ToString("yyyy-MM-dd") + ":" + model.ThirdPartyCooperation);
                        entity.MattersNeedingAttention += "\r\n" + (DateTime.Now.ToString("yyyy-MM-dd") + ":" + model.MattersNeedingAttention);
                        entity.ProjectManager = model.ProjectManager;
                        entity.Publisher = model.Publisher;
                        entity.IsDeleted = model.IsDeleted;
                        entity.Status = model.Status;
                        entity.Code = model.Code;
                        entity.IsFinished = 1;
                        _dbContext.SaveChanges();
                    }
                    return Ok(response);
                }
                else
                {
                    var entity = _dbContext.DncWorkTask.FirstOrDefault(x => x.Code == model.Code);

                    entity.TaskTheme = model.TaskTheme;
                    entity.TaskContent = model.TaskContent;
                    entity.WorkType = model.WorkType;
                    entity.TaskPerson = model.TaskPerson;
                    entity.Telephone = model.Telephone;
                    entity.TaskTime = DateTime.Now.ToString();
                    entity.CompletionTime = model.CompletionTime;
                    entity.TaskPlan = model.TaskPlan;
                    entity.ProgressDeviation = model.ProgressDeviation;
                    entity.InformationNote = model.InformationNote;
                    entity.ThirdPartyCooperation += "\r\n" + (DateTime.Now.ToString("yyyy-MM-dd") + ":" + model.ThirdPartyCooperation);
                    entity.MattersNeedingAttention += "\r\n" + (DateTime.Now.ToString("yyyy-MM-dd") + ":" + model.MattersNeedingAttention);
                    entity.ProjectManager = model.ProjectManager;
                    entity.Publisher = model.Publisher;
                    entity.IsDeleted = model.IsDeleted;
                    entity.Status = model.Status;
                    entity.Code = model.Code;
                    entity.IsFinished = 1;
                    _dbContext.SaveChanges();
                    return Ok(response);
                }
            }
        }
        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="model">任务完成实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Delete/5
        public ActionResult Finished(WorkTaskCreateViewModel model)
        {

            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.DncWorkTask.Count(x => x.Code == model.Code) == 0)
                {
                    response.SetFailed("主题不存在");
                    return Ok(response);
                }
                var entity = _dbContext.DncWorkTask.FirstOrDefault(x => x.Code == model.Code);

                entity.ProgressDeviation = model.ProgressDeviation;
                entity.IsFinished = 1;

                _dbContext.SaveChanges();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="ids">主题ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Delete/5
        public ActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复主题
        /// </summary>
        /// <param name="ids">主题ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
            var response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
            return Ok(response);
        }


        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"> </param>
        /// <param name="ids">主题ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    if (ConfigurationManager.AppSettings.IsTrialVersion)
                    {
                        response.SetIsTrial();
                        return Ok(response);
                    }
                    response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
                    break;
                case "forbidden":
                    if (ConfigurationManager.AppSettings.IsTrialVersion)
                    {
                        response.SetIsTrial();
                        return Ok(response);
                    }
                    response = UpdateStatus(UserStatus.Forbidden, ids);
                    break;
                case "normal":
                    response = UpdateStatus(UserStatus.Normal, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        #region 私有方法

        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">主题ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE DncWorkTask SET IsDeleted=@IsDeleted WHERE id IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">主题状态</param>
        /// <param name="ids">主题ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(UserStatus status, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE DncWorkTask SET Status=@Status WHERE id IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@Status", (int)status));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion

    }
}
