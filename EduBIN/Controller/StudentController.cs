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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static readonly StudentService StudentService = new StudentService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [Authorize(Roles = "Superadmin, Admin, Faculty")]
        [HttpGet]
        public ActionResult GetAll()
        {
            List<Student> Students = StudentService.FindAll(User.Identity.Name);
            return Ok(Students);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Student Student = StudentService.FindById(id, User.Identity.Name);
            if (Student != null)
            {
                return Ok(Student);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [Authorize(Roles = "Superadmin, Admin")]
        [HttpPost]
        public ActionResult PostById([FromBody] Student Student)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = StudentService.Save(Student, User.Identity.Name);
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
        public ActionResult PutById(int id, [FromBody] Student Student)
        {
            if (ModelState.IsValid && id != 0)
            {
                Student.Id = id;
                ErrorResponse.Message = StudentService.Update(Student, User.Identity.Name);
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
            ErrorResponse.Message = StudentService.Enable(id, User.Identity.Name);
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
            ErrorResponse.Message = StudentService.Disable(id, User.Identity.Name);
            if (String.IsNullOrEmpty(ErrorResponse.Message))
            {
                ErrorResponse.Message = Messages.Disable;
                return Ok(ErrorResponse);
            }
            else return BadRequest(ErrorResponse);
        }
    }
}