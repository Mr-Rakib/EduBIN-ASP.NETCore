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
    [Authorize(Roles = "Superadmin, Admin")]
    [Route("[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private static readonly ProgramsService ProgramsService = new ProgramsService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Programs> Programss = ProgramsService.FindAll(User.Identity.Name);
            return Ok(Programss);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Programs Programs = ProgramsService.FindById(id, User.Identity.Name);
            if (Programs != null)
            {
                return Ok(Programs);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [HttpPost]
        public ActionResult PostById([FromBody] Programs Programs)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = ProgramsService.Save(Programs, User.Identity.Name);
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
        public ActionResult PutById(int id, [FromBody] Programs Programs)
        {
            if (ModelState.IsValid && id != 0)
            {
                Programs.Id = id;
                ErrorResponse.Message = ProgramsService.Update(Programs, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Updated;
                    return Ok(ErrorResponse);
                }
            }
            return BadRequest(ErrorResponse);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            if (id != 0)
            {
                ErrorResponse.Message = ProgramsService.Delete(id, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Deleted;
                    return Ok(ErrorResponse);
                }
            }
            return BadRequest(ErrorResponse);
        }

    }
}