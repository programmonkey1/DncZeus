using AutoMapper;
using DncZeus.Api.Entities;

using DncZeus.Api.Extensions;

using DncZeus.Api.RequestPayload.Rbac.StatisticsReport;
using DncZeus.Api.ViewModels.Rbac.DncStatisticsReport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class StatisticsReportController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public StatisticsReportController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(StatisticsReportRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                // var query = _dbContext.StatisticsReport.AsQueryable();
                var query = _dbContext.StatisticsReport.AsQueryable();
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();             
                var data = list.Select(_mapper.Map<StatisticsReport, StatisticsReportJsonModel>);
                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            using (_dbContext)
            {
                var list = _dbContext.StatisticsReport.ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Select()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var sql = string.Format("SELECT top 1 * FROM StatisticsReport order by Timeofday desc");
                var query = _dbContext.StatisticsReport.FromSql(sql);
                var list = query.ToList();
               
                response.SetData(list);
                return Ok(response);
            }
        }

      

    }
}
