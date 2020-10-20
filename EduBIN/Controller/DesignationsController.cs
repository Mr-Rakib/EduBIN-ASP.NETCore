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
    public class DesignationsController : ControllerBase
    {
        private static readonly DesignationsService DesignationsService = new DesignationsService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Designations> Designationss = DesignationsService.FindAll(User.Identity.Name);
            return Ok(Designationss);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Designations Designations = DesignationsService.FindById(id, User.Identity.Name);
            if (Designations != null)
            {
                return Ok(Designations);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [HttpPost]
        public ActionResult PostById([FromBody] Designations Designations)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = DesignationsService.Save(Designations, User.Identity.Name);
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
        public ActionResult PutById(int id, [FromBody] Designations Designations)
        {
            if (ModelState.IsValid && id != 0)
            {
                Designations.Id = id;
                ErrorResponse.Message = DesignationsService.Update(Designations, User.Identity.Name);
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
                ErrorResponse.Message = DesignationsService.Delete(id, User.Identity.Name);
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