/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using System;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Rbac.TaskList;
using DncZeus.Api.Utils;
using DncZeus.Api.ViewModels.Rbac.DncTaskList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class TaskListController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public TaskListController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(TaskListRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.DncTaskList.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.PersonLiable.Contains(payload.Kw.Trim()) || x.Code.Contains(payload.Kw.Trim()));
                }
                /*if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsComplete == payload.IsDeleted);
                }*/
                /* if (payload.Status > CommonEnum.Status.All)
                 {
                     query = query.Where(x => x.Status == payload.Status);
                 }*/
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var data = list.Select(_mapper.Map<DncTaskList, TaskListJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="model">角色视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(TaskListCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
        var entity = _mapper.Map<TaskListCreateViewModel, DncTaskList>(model);
                entity.KeyWork = model.KeyWork;
                entity.ImportanceOfWork = model.ImportanceOfWork;
                entity.CreationTime = DateTime.Now;
                entity.MajorIinitiatives = model.MajorIinitiatives;
                entity.ImplementationProgress = model.ImplementationProgress;
                entity.ProgressStatus = model.ProgressStatus;
                entity.PersonLiable = model.PersonLiable;
                entity.Remarks = model.Remarks;
                entity.IsComplete = model.IsComplete;
                entity.Code = RandomHelper.GetRandomizer(8, true, false, true, true);
                _dbContext.DncTaskList.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="code">角色惟一编码</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(string code)
        {
            using (_dbContext)
            {
                var entity = _dbContext.DncTaskList.FirstOrDefault(x => x.PersonLiable == code);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<DncTaskList, TaskListCreateViewModel>(entity));
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
        public IActionResult Edit(TaskListCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (ConfigurationManager.AppSettings.IsTrialVersion)
            {
                response.SetIsTrial();
                return Ok(response);
            }
            using (_dbContext)
            {
                /*if (_dbContext.DncTaskList.Count(x => x.Name == model.Name && x.Code != model.Code) > 0)
                {
                    response.SetFailed("角色已存在");
                    return Ok(response);
                }*/
                //FirstOrDefault返回序列中的第一个元素；如果序列中不包含任何元素，则返回默认值
                var entity = _dbContext.DncTaskList.FirstOrDefault(x => x.Code == model.Code);

                entity.KeyWork = model.KeyWork;
                entity.ImportanceOfWork = model.ImportanceOfWork;
                entity.CreationTime = DateTime.Now;
                entity.MajorIinitiatives = model.MajorIinitiatives;
                entity.ImplementationProgress = model.ImplementationProgress;
                entity.ProgressStatus = model.ProgressStatus;
                entity.PersonLiable = model.PersonLiable;
                entity.Remarks = model.Remarks;
                entity.IsComplete = model.IsComplete;
                _dbContext.SaveChanges();
                return Ok(response);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Select(TaskListRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var sql = string.Format("exec QueryStatistics");
                var query = _dbContext.DncTaskList.FromSql(sql);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var data = list.Select(_mapper.Map<DncTaskList, TaskListJsonModel>);

                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /*/// <summary>
        /// 获取指定用户的角色列表
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/Tasklist/find_list_by_user_guid/{guid}")]
        public IActionResult FindListByUserGuid(Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                //有N+1次查询的性能问题
                //var query = _dbContext.DncUser
                //    .Include(r => r.UserRoles)
                //    .ThenInclude(x => x.DncRole)
                //    .Where(x => x.Guid == guid);
                //var roles = query.FirstOrDefault().UserRoles.Select(x => new
                //{
                //    x.DncRole.Code,
                //    x.DncRole.Name
                //});
                var sql = @"SELECT R.* FROM DncUserRoleMapping AS URM
INNER JOIN DncRole AS R ON R.Code=URM.RoleCode
WHERE URM.UserGuid={0}";
                var query = _dbContext.DncRole.FromSql(sql, guid).ToList();
                var assignedRoles = query.ToList().Select(x => x.Code).ToList();
                var roles = _dbContext.DncRole.Where(x => x.IsDeleted == CommonEnum.IsDeleted.No && x.Status == CommonEnum.Status.Normal).ToList().Select(x => new { label = x.Name, key = x.Code });
                response.SetData(new { roles, assignedRoles });
                return Ok(response);
            }
        }*/

        /// <summary>
        /// 查询所有角色列表(只包含主要的字段信息:name,code)
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/TaskList/find_simple_list")]
        public IActionResult FindSimpleList()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var tasklists = _dbContext.DncTaskList.Where(x => x.IsComplete == 1).Select(x => new { x.PersonLiable, x.Code }).ToList();
                response.SetData(tasklists);
            }
            return Ok(response);
        }
    }
}