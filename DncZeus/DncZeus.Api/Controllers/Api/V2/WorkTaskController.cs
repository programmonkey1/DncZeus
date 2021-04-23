using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DncZeus.Api.Controllers.api.v2
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [Route("api/v2/[controller]/[action]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        public WorkTaskController(DncZeusDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult List()
        {
            using (_dbContext)
            {
                var list = _dbContext.DncWorkTask.ToList();
                List<string> timeList = new List<string>();
                string daylist = null;
                
                for (int i = 0; i < list.ToArray().Length; i++) {
                    //2021-04-08 - 2021-04-28
                    
                    //首先保证 rq1<=rq2
                    DateTime time1 = Convert.ToDateTime(list[i].CompletionTime.Substring(0, 10));
                    DateTime time2 = Convert.ToDateTime(list[i].CompletionTime.Substring(13, 10));
                    while (time1 <= time2)
                    {
                        timeList.Add(time1.ToString("yyyy-MM-dd"));
                        time1 = time1.AddDays(1);
                    }
                    for (int z = 0; z < timeList.ToArray().Length; z++)
                    {
                        daylist += timeList[z].Substring(8, 2) + " ";
                    }
                    list[i].TaskPlan = daylist;
                }

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }
    }
}