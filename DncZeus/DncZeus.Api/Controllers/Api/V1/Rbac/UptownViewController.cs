using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.RequestPayload.Rbac.UptownView;
using DncZeus.Api.ViewModels.Rbac.DncUptown;
using Microsoft.EntityFrameworkCore;


namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class UptownViewController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UptownViewController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(UptownViewRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //var sql = string.Format("exec QueryStatistics");
                var query = _dbContext.UptownView.AsQueryable();
                //var query = _dbContext.AfterView.FromSql(sql);

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                //var C2Sum = query.Sum(Cbwc2count);
                var data = list.Select(_mapper.Map<UptownView, UptownJsonModel>);

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
                var list = _dbContext.UptownView.ToList();

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Select(UptownViewRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var sql = string.Format("exec QueryUPStatistics");
                var query = _dbContext.UptownView.FromSql(sql);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var data = list.Select(_mapper.Map<UptownView, UptownJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult AfterSum()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.UptownView.AsQueryable();
                //var query = _dbContext.AfterView.FromSql(sql);

                var list = query.ToList();
                var totalCount = query.Count();
                //var C2Sum = query.Sum(Cbwc2count);
                var c2 = query.Sum(o => o.Cbwc2count);
                var c5 = query.Sum(o => o.Cbwc5number);

                // response.SetData(list, totalCount);
                response.SetData(new
                {
                    DataName = query.Count(),
                    C2 = query.Sum(o => o.Cbwc2count),
                    C5 = query.Sum(o => o.Cbwc5number),
                    C0 = query.Sum(o => o.Cbwc0count),
                    C4 = query.Sum(o => o.Cbwc4count),
                    UptownSum = query.Sum(o => o.UptownNumber),
                    MeterSum = query.Sum(o => o.MeterCount)



                });
                return Ok(response);
            }
        }

    }
}
