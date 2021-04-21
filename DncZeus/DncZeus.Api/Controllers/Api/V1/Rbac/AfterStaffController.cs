using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using DncZeus.Api.ViewModels.Rbac.DncAfterStaff;
using DncZeus.Api.RequestPayload.Rbac.AfterStaff;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class AfterStaffController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AfterStaffController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(AfterStaffRequestPayload payload)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                response.SetFailed("角色不存在");
                return Ok(response);
            }
        }
        // GET: AfterStaffController
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Index()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                response.SetFailed("角色不存在");
                return Ok(response);
            }
        }

        // GET: AfterStaffController/Details/5
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Details(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // GET: AfterStaffController/Create
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Create()
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // POST: AfterStaffController/Create
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Create(AfterStaffController collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var response = ResponseModelFactory.CreateInstance;
                response.SetFailed("角色不存在");
                return Ok(response);
            }
        }

        // GET: AfterStaffController/Edit/5
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Edit(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // POST: AfterStaffController/Edit/5
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Edit(int id, AfterStaffController collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var response = ResponseModelFactory.CreateInstance;
                response.SetFailed("角色不存在");
                return Ok(response);
            }
        }

        // GET: AfterStaffController/Delete/5
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Delete(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // POST: AfterStaffController/Delete/5
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Delete(int id, AfterStaffController collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var response = ResponseModelFactory.CreateInstance;
                response.SetFailed("角色不存在");
                return Ok(response);
            }
        }
    }
}
