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
    [Authorize(Roles = "Superadmin, Admin, Faculty")]
    [Route("[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private static readonly FacultyService FacultyService = new FacultyService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpGet]
        public ActionResult GetAll()
        {
            List<Faculty> Facultys = FacultyService.FindAll(User.Identity.Name);
            return Ok(Facultys);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Faculty Faculty = FacultyService.FindById(id, User.Identity.Name);
            if (Faculty != null)
            {
                return Ok(Faculty);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpPost]
        public ActionResult PostById([FromBody] Faculty Faculty)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = FacultyService.Save(Faculty, User.Identity.Name);
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
        public ActionResult PutById(int id, [FromBody] Faculty Faculty)
        {
            if (ModelState.IsValid && id != 0)
            {
                Faculty.Id = id;
                ErrorResponse.Message = FacultyService.Update(Faculty, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Updated;
                    return Ok(ErrorResponse);
                }
            }
            return BadRequest(ErrorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpPost("Enable/{id}")]
        public ActionResult EnableById(int id)
        {
            ErrorResponse.Message = FacultyService.Enable(id, User.Identity.Name);
            if (String.IsNullOrEmpty(ErrorResponse.Message))
            {
                ErrorResponse.Message = Messages.Enable;
                return Ok(ErrorResponse);
            }
            else return BadRequest(ErrorResponse);
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpPost("Disable/{id}")]
        public ActionResult DisableById(int id)
        {
            ErrorResponse.Message = FacultyService.Disable(id, User.Identity.Name);
            if (String.IsNullOrEmpty(ErrorResponse.Message))
            {
                ErrorResponse.Message = Messages.Disable;
                return Ok(ErrorResponse);
            }
            else return BadRequest(ErrorResponse);
        }
    }
}