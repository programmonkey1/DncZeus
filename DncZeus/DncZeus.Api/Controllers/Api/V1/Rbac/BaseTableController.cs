using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.RequestPayload.Rbac.BaseTable;
using DncZeus.Api.ViewModels.Rbac.DncBaseTable;
using DncZeus.Api.Utils;
using System.Data.SqlClient;
using DncZeus.Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class BaseTableController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public BaseTableController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// aaa
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(BaseTableRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.BaseTable.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.BatchNumber.Contains(payload.Kw.Trim()) || x.Btid.Contains(payload.Kw.Trim()));
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == payload.IsDeleted);
                }
                if (payload.Status > CommonEnum.Status.All)
                {
                    query = query.Where(x => x.Status == payload.Status);
                }
                
                var list = query.OrderBy(x => x.DateOfManufacture).Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var data = list.Select(_mapper.Map<BaseTable, BaseTableJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/basetable/find_list_by_kw/{kw}")]
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

                var query = _dbContext.BaseTable.Where(x => x.BatchNumber.Contains(kw));

                var list = query.ToList();
                var data = list.Select(x => new { x.Btid, x.BatchNumber, x.DateOfManufacture });

                response.SetData(data);
                return Ok(response);
            }
        }


        /// <summary>
        /// 创建电子单元
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Create(BaseTableCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.BatchNumber.Trim().Length <= 0)
            {
                response.SetFailed("请输入批次号");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.BaseTable.Count(x => x.BatchNumber == model.BatchNumber) > 0)
                {
                    response.SetFailed("设备编号已存在");
                    return Ok(response);
                }
                var entity = _mapper.Map<BaseTableCreateViewModel, BaseTable>(model);
                entity.Btid = RandomHelper.GetRandomizer(8, true, false, true, true);
                entity.DateOfManufacture = DateTime.Now;
                _dbContext.BaseTable.Add(entity);
                _dbContext.SaveChanges();


                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="btid">角色惟一编码</param>
        /// <returns></returns>
        [HttpGet("{btid}")]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Edit/5
        public ActionResult Edit(string btid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.BaseTable.FirstOrDefault(x => x.Btid == btid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<BaseTable, BaseTableCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的角色信息
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Edit(BaseTableCreateViewModel model)
        {

            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.BaseTable.Count(x => x.Btid == model.Btid) == 0)
                {
                    response.SetFailed("设备编号不存在");
                    return Ok(response);
                }

                var entity = _dbContext.BaseTable.FirstOrDefault(x => x.Btid == model.Btid);

           
                entity.BatchNumber = model.BatchNumber;
                entity.SteelSealNumberRange = model.SteelSealNumberRange;
                entity.ParameterSpecification = model.ParameterSpecification;
                entity.InstallationMethod = model.InstallationMethod;
                entity.DateOfManufacture = DateTime.Now;
                entity.CommonTraffic = model.CommonTraffic;
                entity.CommonFlowRatio = model.CommonFlowRatio;
                entity.InitialFlow = model.InitialFlow;
                entity.MinimumReading = model.MinimumReading;
                entity.MaximumDegree = model.MaximumDegree;
                entity.MaximumWorkingPressure = model.MaximumWorkingPressure;
                entity.MaximumOperatingTemperature = model.MaximumOperatingTemperature;
                entity.WorkingEnvironmentTemperature = model.WorkingEnvironmentTemperature;
                entity.WorkingEnvironmentHumidity = model.WorkingEnvironmentHumidity;
                entity.ExecutiveStandard = model.ExecutiveStandard;

                entity.Remarks = model.Remarks;
                entity.IsDeleted = model.IsDeleted;
                entity.Status = model.Status;

                _dbContext.SaveChanges();
                return Ok(response);
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
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
        /// 恢复角色
        /// </summary>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
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
        /// <param name="command"></param>
        /// <param name="ids">角色ID,多个以逗号分隔</param>
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
        /// 删除角色
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE BaseTable SET IsDeleted=@IsDeleted WHERE Btid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="status">角色状态</param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(UserStatus status, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE BaseTable SET Status=@Status WHERE Btid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@Status", (int)status));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
    }
}
