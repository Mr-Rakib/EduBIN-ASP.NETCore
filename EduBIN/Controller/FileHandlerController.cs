using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduBIN.BLL.Services;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduBIN.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileHandlerController : ControllerBase
    {
        private readonly FileService FileService = new FileService();
        private readonly ErrorResponse errorResponse = new ErrorResponse();

        [HttpPost("UploadImage")]
        public ActionResult UploadImage([FromForm] IFormFile Image)
        {
            Dictionary<int, string> Keyvalue = FileService.UploadImage(Image);

            if (String.IsNullOrEmpty(Keyvalue[2]))
            {
                if (!String.IsNullOrEmpty(Keyvalue[1]))
                {
                    File file = new File()
                    {
                        url = Keyvalue[1]
                    };
                    return Ok(file);
                }
            }
            else errorResponse.Message = Keyvalue[2];
            return BadRequest(errorResponse);
        }

        [HttpPost("UploadFile")]
        public ActionResult UploadFile([FromForm] IFormFile File)
        {
            Dictionary<int, string> Keyvalue = FileService.UploadFile(File);
            if (String.IsNullOrEmpty(Keyvalue[2]))
            {
                if (!String.IsNullOrEmpty(Keyvalue[1]))
                {
                    File file = new File()
                    {
                        url = Keyvalue[1]
                    };
                    return Ok(file);
                }
            }
            else errorResponse.Message = Keyvalue[2];
            return BadRequest(errorResponse);
        }

        [HttpDelete]
        public ActionResult Delete(string url)
        {
            string message = FileService.Delete(url);
            if (String.IsNullOrEmpty(message))
            {
                return Ok();
            }
            else errorResponse.Message = message;
            return BadRequest(errorResponse);
        }
    }
}
