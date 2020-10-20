
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
    public class LoginController : ControllerBase
    {
        private static readonly LoginService LoginService = new LoginService();
        private static readonly ErrorResponse ErrorResponse = new ErrorResponse();

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Login> Logins = LoginService.FindAll(User.Identity.Name);
            return Ok(Logins);
        }

        [HttpGet("{username}")]
        public ActionResult GetByUsername(string Username)
        {
            Login Login = LoginService.FindByUsername(Username);
            if (Login != null)
            {
                return Ok(Login);
            }
            else
            {
                ErrorResponse.Message = Messages.NotFound;
                return BadRequest(ErrorResponse);
            }
        }

        [HttpPost]
        public ActionResult PostById([FromBody] Login Login)
        {
            if (ModelState.IsValid)
            {
                ErrorResponse.Message = LoginService.Save(Login, User.Identity.Name);
                if (String.IsNullOrEmpty(ErrorResponse.Message))
                {
                    ErrorResponse.Message = Messages.Saved;
                    return Created(HttpContext.Request.Scheme, ErrorResponse);
                }
            }
            else ErrorResponse.Message = Messages.InvalidField;
            return BadRequest(ErrorResponse);
        }

        [HttpPut("{username}")]
        public ActionResult PutById(string Username, [FromBody] Login Login)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(Username))
            {
                Login.Username = Username;
                ErrorResponse.Message = LoginService.Update(Login, User.Identity.Name);
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