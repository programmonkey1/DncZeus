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
                List<string> daylist = new List<string>();
                List<string> delaylist = new List<string>();
                DateTime day1, day2;
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
                    day2 = Convert.ToDateTime(timelist[i].CompletionEndTime);
                    completionEndTime = day1.ToString("yyyy-MM-dd") + " - " + day2.ToString("yyyy-MM-dd");
                    tasktime = timelist[i].TaskTime.ToString().Replace("T", " ");
                    //2021-04-08 - 2021-04-28
                    //首先保证 time1<=time2
                    if (timelist[i].IsFinished == 1)
                    {

                        timelist[i].CompletionTime = completionEndTime;
                        /*if (day1 < nowday)
                        {
                            timelist[i].ProgressDeviation = "提前完成";
                        }
                        else if (day1 == nowday) {
                            timelist[i].ProgressDeviation = "按期完成";
                        }
                        if (day1 <= nowday && nowday > day2) {
                            timelist[i].ProgressDeviation = "延期完成";
                        }*/
                    }
                    else if(timelist[i].IsFinished == 0){
                        
                        timelist[i].CompletionTime = completionEndTime;
                        if (day1 <= nowday && nowday <= day2)
                        {
                            daylist.Clear();
                            delaylist.Clear();

                            if (day1 < nowday && nowday < day2)
                            {
                                timelist[i].ProgressDeviation = "正在完成中";
                            }
                            else if (nowday == day2) {
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
                        }

                        if (day1 <= nowday && nowday > day2)
                        {
                            timelist[i].ProgressDeviation = "已逾期";
                            daylist.Clear();
                            delaylist.Clear();
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
                            for (int delays = 1; delays < delaylist.ToArray().Length; delays++)
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
                        string sql = "UPDATE DncWorkTask SET NO1 = '" + timelist[i].No1 + "',NO2 = '" + timelist[i].No2 + "',NO3 = '" + timelist[i].No3 + "',NO4 = '" + timelist[i].No4 + "',NO5 = '" + timelist[i].No5 + "',NO6 = '" + timelist[i].No6 + "',NO7 = '" + timelist[i].No7 + "',NO8 = '" + timelist[i].No8 + "',NO9 = '" + timelist[i].No9 + "',NO10 = '" + timelist[i].No10 + "',NO11 = '" + timelist[i].No11 + "',NO12 = '" + timelist[i].No12 + "',NO13 = '" + timelist[i].No13 + "',NO14 = '" + timelist[i].No14 + "',NO15 = '" + timelist[i].No15 + "',NO16 = '" + timelist[i].No16 + "',NO17 = '" + timelist[i].No17 + "',NO18 = '" + timelist[i].No18 + "',NO19 = '" + timelist[i].No19 + "',NO20 = '" + timelist[i].No20 + "',NO21 = '" + timelist[i].No21 + "',NO22 = '" + timelist[i].No22 + "',NO23 = '" + timelist[i].No23 + "',NO24 = '" + timelist[i].No24 + "',NO25 = '" + timelist[i].No25 + "',NO26 = '" + timelist[i].No26 + "',NO27 = '" + timelist[i].No27 + "',NO28 = '" + timelist[i].No28 + "',NO29 = '" + timelist[i].No29 + "',NO30 = '" + timelist[i].No30 + "',NO31 = '" + timelist[i].No31 + "',ProgressDeviation ='" + timelist[i].ProgressDeviation + "' WHERE id = '" + (i+1) + "'";
                        _dbContext.Database.ExecuteSqlCommand(sql);
                    }
                    
                }

                var query = _dbContext.DncWorkTask.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.TaskPerson.Contains(payload.Kw.Trim()) || x.Code.Contains(payload.Kw.Trim()));
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
            if (model.CompletionFirstTime.Length<=0|| model.CompletionEndTime.Length <= 0)
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
            if (model.TaskPerson.Length<=0)
            {
                response.SetFailed("请输入任务人");
                return Ok(response);
            }
            if (model.Telephone.Length<=0)
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
            {}else
            {
                response.SetFailed("请输入正确的联系电话");
                return Ok(response);
            }
            if (model.ProjectManager.Length<= 0)
            {
                response.SetFailed("请输入经理");
                return Ok(response);
            }
            if (model.Publisher.Length <= 0)
            {
                response.SetFailed("请输入发布人");
                return Ok(response);
            }

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
                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                entity.CompletionFirstTime = Convert.ToDateTime(model.CompletionFirstTime).ToString("yyyy-MM-dd");
                entity.CompletionEndTime = Convert.ToDateTime(model.CompletionEndTime).ToString("yyyy-MM-dd");
                entity.Code = RandomHelper.GetRandomizer(8, true, false, true, true);
                entity.TaskTime = DateTime.Now.ToString();
                _dbContext.DncWorkTask.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 编辑任务主题
        /// </summary>
        /// <param name="Code">主题惟一编码</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Edit/5
        public ActionResult Edit(int id)
        {
            using (_dbContext)
            {
                var entity = _dbContext.DncWorkTask.FirstOrDefault(x => x.Id == id);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<DncWorkTask, WorkTaskCreateViewModel>(entity));
                return Ok(response);
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
                entity.ThirdPartyCooperation = model.ThirdPartyCooperation;
                entity.MattersNeedingAttention = model.MattersNeedingAttention;
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

        

        /*/// <summary>
        /// 恢复未完成
        /// </summary>
        /// <param name="model">任务完成实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RecoverFinished(WorkTaskeditViewModel model)
        {
            var response = UpdateIsFinished(model);
            return Ok(response);
        }*/

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

        /// <summary>
        /// 更改任务状态
        /// </summary>
        /// <param name="model">任务状态实体</param>
        /// <returns></returns>
        private ResponseModel UpdateIsFinished(WorkTaskeditViewModel model)
        {
            using (_dbContext)
            {
                if (model.ProgressDeviation == "提前完成")
                {
                    string sql = "UPDATE DncWorkTask SET ProgressDeviation= '提前完成',IsFinished = 1 WHERE id ='" + model.Id + "'";
                    _dbContext.Database.ExecuteSqlCommand(sql);
                }
                else if (model.ProgressDeviation == "按时完成") {
                    string sql = "UPDATE DncWorkTask SET ProgressDeviation= '按时完成',IsFinished = 1 WHERE id ='" + model.Id + "'";
                    _dbContext.Database.ExecuteSqlCommand(sql);
                }
                else if (model.ProgressDeviation == "延时完成") {
                    string sql = "UPDATE DncWorkTask SET ProgressDeviation= '延时完成',IsFinished = 1 WHERE id ='" + model.Id + "'";
                    _dbContext.Database.ExecuteSqlCommand(sql);
                }
                    
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
    }
}
