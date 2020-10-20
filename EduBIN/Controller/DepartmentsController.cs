using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduBIN.BLL.Service;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduBIN.Controller
{
    [Authorize(Roles ="Superadmin, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private static readonly DepartmentsService DepartmentsService = new DepartmentsService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Departments> Departmentss = DepartmentsService.FindAll(User.Identity.Name);
            return Ok(Departmentss);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Departments Departments = DepartmentsService.FindById(id, User.Identity.Name);
            if (Departments != null)
            {
                return Ok(Departments);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [HttpPost]
        public ActionResult PostById([FromBody] Departments Departments)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = DepartmentsService.Save(Departments, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, ErrorResponse);
                }
            }
            else ErrorResponse.Message = Messages.InvalidField;
            return BadRequest(ErrorResponse);
        }

        [HttpPut("{id}")]
        public ActionResult PutById(int id, [FromBody] Departments Departments)
        {
            if (ModelState.IsValid && id != 0)
            {
                Departments.Id = id;
                ErrorResponse.Message = DepartmentsService.Update(Departments, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Updated;
                    return Ok(ErrorResponse);
                }
            }
            return BadRequest(ErrorResponse);
        }
    }
}