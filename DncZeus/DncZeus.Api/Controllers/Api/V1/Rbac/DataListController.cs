using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using DncZeus.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DncZeus.Api.Controllers.api.v1
{
    /// <summary>
    /// getlist
    /// </summary>
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    public class DataListController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        public DataListController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult List()
        {
            using (_dbContext)
            {
                _dbContext.Database.ExecuteSqlCommand("DELETE FROM DataList");
                var planlist = _dbContext.DncWorkTask.ToList();
                List<string> daylist = new List<string>();
                List<string> delaylist = new List<string>();
                DateTime nowday = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                for (int cleari = 0; cleari < planlist.ToArray().Length; cleari++)
                {
                    //首先保证 time1<=time2
                    DateTime day1 = Convert.ToDateTime(planlist[cleari].CompletionTime.Substring(0, 10));
                    DateTime day2 = Convert.ToDateTime(planlist[cleari].CompletionTime.Substring(13, 10));
                    string month = null;
                    if (day1 <= day2)
                    {
                        if (int.Parse(day1.ToString("yyyy-MM-dd").Substring(5, 2)) < int.Parse(day2.ToString("yyyy-MM-dd").Substring(5, 2)))
                        {
                            month = day1.ToString("yyyy-MM-dd").Substring(0, 7) + " - " + day2.ToString("yyyy-MM-dd").Substring(0, 7);                          
                        }
                        else if (int.Parse(day1.ToString("yyyy-MM-dd").Substring(5, 2)) == int.Parse(day2.ToString("yyyy-MM-dd").Substring(5, 2)))
                        {
                            month = day2.ToString("yyyy-MM-dd").Substring(0, 7);                           
                        }
                    }
                    DataListCreateViewModel model = new DataListCreateViewModel();
                    var entity = _mapper.Map<DataListCreateViewModel, DataList>(model);
                    entity.Id = cleari + 1;
                    entity.Month = month;
                    entity.No1 = 0;
                    entity.No2 = 0;
                    entity.No3 = 0;
                    entity.No4 = 0;
                    entity.No5 = 0;
                    entity.No6 = 0;
                    entity.No7 = 0;
                    entity.No8 = 0;
                    entity.No9 = 0;
                    entity.No10 = 0;
                    entity.No11 = 0;
                    entity.No12 = 0;
                    entity.No13 = 0;
                    entity.No14 = 0;
                    entity.No15 = 0;
                    entity.No16 = 0;
                    entity.No17 = 0;
                    entity.No18 = 0;
                    entity.No19 = 0;
                    entity.No20 = 0;
                    entity.No21 = 0;
                    entity.No22 = 0;
                    entity.No23 = 0;
                    entity.No24 = 0;
                    entity.No25 = 0;
                    entity.No26 = 0;
                    entity.No27 = 0;
                    entity.No28 = 0;
                    entity.No29 = 0;
                    entity.No30 = 0;
                    entity.No31 = 0;
                    entity.IsDelete = 0;
                    entity.Status = 0;
                    entity.Code = RandomHelper.GetRandomizer(8, true, false, true, true);
                    _dbContext.DataList.Add(entity);
                    _dbContext.SaveChanges();
                }
                var timelist = _dbContext.DataList.ToList();
                for (int i = 0; i < planlist.ToArray().Length; i++)
                {
                    //2021-04-08 - 2021-04-28

                    //首先保证 time1<=time2
                    DateTime day1 = Convert.ToDateTime(planlist[i].CompletionTime.Substring(0, 10));
                    DateTime day2 = Convert.ToDateTime(planlist[i].CompletionTime.Substring(13, 10));

                    if (day1 <= nowday && nowday <= day2)
                    {
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
                    } 
                    
                    if(day1 <= nowday && nowday > day2) {
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
                        for (int delays = 0; delays < delaylist.ToArray().Length; delays++)
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
                }

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(timelist);
                return Ok(response);
            }
        }
    }
}