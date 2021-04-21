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
                
                var list = query.OrderBy(x => x.TaskTime).Paged(payload.CurrentPage, payload.PageSize).ToList();
                 var totalCount = query.Count();
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
            if (model.TaskContent.Trim().Length <= 0)
            {
                response.SetFailed("请输入主题");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.DncWorkTask.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("设任务已存在");
                    return Ok(response);
                }
                var entity = _mapper.Map<WorkTaskCreateViewModel, DncWorkTask>(model);
                entity.Code = RandomHelper.GetRandomizer(8, true, false, true, true);
                entity.TaskTime = DateTime.Now;
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
        public ActionResult Edit(string Code)
        {
            using (_dbContext)
            {
                var entity = _dbContext.DncWorkTask.FirstOrDefault(x => x.Code == Code);
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
                entity.TaskTime = DateTime.Now;
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
        /// 删除主题
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
