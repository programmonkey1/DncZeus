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
using DncZeus.Api.RequestPayload.Rbac.EquipmentInstallation;
using DncZeus.Api.ViewModels.Rbac.DncEquipmentInstallation;
using DncZeus.Api.Models.Response;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DncZeus.Api.Utils;


namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class EquipmentInstallationController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public EquipmentInstallationController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(EquipmentInstallationRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.EquipmentInstallation.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Eunumber.Contains(payload.Kw.Trim()) || x.Eiid.Contains(payload.Kw.Trim()));
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
                var data = list.Select(_mapper.Map<EquipmentInstallation, EquipmentInstallationJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/equipmentinstallation/find_list_by_kw/{kw}")]
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

                var query = _dbContext.EquipmentInstallation.Where(x => x.Eunumber.Contains(kw));

                var list = query.ToList();
                var data = list.Select(x => new { x.Eiid, x.Diid, x.Ipid,x.Eunumber,x.PayNumber,x.InstallationTime });

                response.SetData(data);
                return Ok(response);
            }
        }


        /// <summary>
        /// 创建电子单元
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>
        // POST: ECUTableController/Create
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Create(EquipmentInstallationCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.Eunumber.Trim().Length <= 0)
            {
                response.SetFailed("请输入设备编号");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.EquipmentInstallation.Count(x => x.Eunumber == model.Eunumber) > 0)
                {
                    response.SetFailed("设备编号已存在");
                    return Ok(response);
                }
                var entity = _mapper.Map<EquipmentInstallationCreateViewModel, EquipmentInstallation>(model);
                entity.Eiid = RandomHelper.GetRandomizer(8, true, false, true, true);
                entity.InstallationTime = DateTime.Now;
                _dbContext.EquipmentInstallation.Add(entity);
                _dbContext.SaveChanges();


                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="eiid">角色惟一编码</param>
        /// <returns></returns>
        [HttpGet("{eiid}")]
        [ProducesResponseType(200)]
        // GET: ECUTableController/Edit/5
        public ActionResult Edit(string eiid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.EquipmentInstallation.FirstOrDefault(x => x.Eiid == eiid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<EquipmentInstallation, EquipmentInstallationCreateViewModel>(entity));
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
        public ActionResult Edit(EquipmentInstallationCreateViewModel model)
        {

            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.EquipmentInstallation.Count(x => x.Eiid == model.Eiid) == 0)
                {
                    response.SetFailed("设备编号不存在");
                    return Ok(response);
                }

                var entity = _dbContext.EquipmentInstallation.FirstOrDefault(x => x.Eiid == model.Eiid);

                // entity.Ecuid = model.Ecuid;
                entity.Diid = model.Diid;
                entity.Eunumber = model.Eunumber;
                entity.Ipid = model.Ipid;
                entity.PayNumber = model.PayNumber;
                entity.InstallationTime = DateTime.Now;
             
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
                var sql = string.Format("UPDATE EquipmentInstallation SET IsDeleted=@IsDeleted WHERE EIID IN ({0})", parameterNames);
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
                var sql = string.Format("UPDATE EquipmentInstallation SET Status=@Status WHERE EIID IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@Status", (int)status));
                _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
    }
}
