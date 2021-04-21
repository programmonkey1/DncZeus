using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Rbac.Icon;
using DncZeus.Api.ViewModels.Rbac.DncData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DncZeus.Api.Controllers.Api.V1.Rbac
{
    //[CustomAuthorize]
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class MeterViewController : Controller
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public MeterViewController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        // GET: MeterViewController
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


        // GET: MeterViewController/Details/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult Details(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(200)]
        // GET: MeterViewController/Create
        public ActionResult Create()
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }
        
        // POST: MeterViewController/Create
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Create(MeterViewController collection)
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
        
        // GET: MeterViewController/Edit/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult Edit(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // POST: MeterViewController/Edit/5
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Edit(int id, MeterViewController collection)
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

        // GET: MeterViewController/Delete/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public ActionResult Delete(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            response.SetFailed("角色不存在");
            return Ok(response);
        }

        // POST: MeterViewController/Delete/5
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult Delete(int id, MeterViewController collection)
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
