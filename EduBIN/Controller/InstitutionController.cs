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
    [Authorize (Roles = "Superadmin")]
    [Route("[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private static InstitutionService InstitutionService = new InstitutionService();
        private static ErrorResponse ErrorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Institution> institutions = InstitutionService.FindAll();
            return Ok(institutions);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Institution Institution = InstitutionService.FindById(id);
            if (Institution != null)
            {
                return Ok(Institution);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostById([FromBody] Institution institution)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = InstitutionService.Save(institution);
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
        public ActionResult PutById(int id, [FromBody] Institution institution)
        {
            if (ModelState.IsValid && id != 0)
            {
                institution.Id = id;
                ErrorResponse.Message = InstitutionService.Update(institution, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Updated;
                    return Ok(ErrorResponse);
                }
            }
            return BadRequest(ErrorResponse);
        }

        [HttpPost("Enable/{id}")]
        public ActionResult EnableById(int id)
        {
            ErrorResponse.Message = InstitutionService.Enable(id, User.Identity.Name);
            if (String.IsNullOrEmpty(ErrorResponse.Message))
            {
                ErrorResponse.Message = Messages.Enable;
                return Ok(ErrorResponse);
            }
            else return BadRequest(ErrorResponse);
        }

        [HttpPost("Disable/{id}")]
        public ActionResult DisableById(int id)
        {
            ErrorResponse.Message = InstitutionService.Disable(id, User.Identity.Name);
            if (String.IsNullOrEmpty(ErrorResponse.Message))
            {
                ErrorResponse.Message = Messages.Disable;
                return Ok(ErrorResponse);
            }
            else return BadRequest(ErrorResponse);
        }
    }
}