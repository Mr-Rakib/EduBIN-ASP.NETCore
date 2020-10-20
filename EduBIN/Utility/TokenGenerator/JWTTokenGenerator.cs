using System; 
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EduBIN.BLL.Service;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace EduBIN.Utility.TokenGenerator
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class JWTTokenGenerator : ControllerBase
    {
        private static readonly LoginService LoginService = new LoginService();
        private static readonly InstitutionService InstitutionService = new InstitutionService();

        [HttpPost]
        [HttpGet]
        [Route("/token")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Token([FromForm] LoginCredential loginCredential)
        {
            ErrorResponse response = new ErrorResponse();
            if (AuthorizeUser(loginCredential))
            {
                response.Message = IsActiveLoginPermission(loginCredential);
                if (response.Message == null)
                {
                    TokenProvider provider = GenerateToken(loginCredential);
                    if (provider != null)
                        return Ok(provider);

                    else response.Message = Messages.InvalidUser;
                }
            }
            else response.Message = Messages.InvalidField;
            
            return BadRequest(response);
        }

        private string IsActiveLoginPermission(LoginCredential loginCredential)
        {
            Login isActiveLogin = LoginService.FindByUsername(loginCredential.Username);
            if (isActiveLogin != null)
            {
                Institution institution = InstitutionService.FindById(isActiveLogin.InstitutionId);
                if (institution != null)
                {
                    if (institution.IsActive)
                    {
                        return (isActiveLogin.IsActive) ?
                            null : Messages.AccessDenied;
                    }
                    else return Messages.InstitutionNotActive;
                }
                else return Messages.InstitutionNotExist;
            }
            else return Messages.NotFound;
        }

        private bool AuthorizeUser(LoginCredential loginCredential)
        {
            return (String.IsNullOrEmpty(loginCredential.Username)      || 
                    String.IsNullOrEmpty(loginCredential.Password)      ||
                    String.IsNullOrEmpty(loginCredential.Grant_type)
                    )   ? 
            false: (loginCredential.Grant_type.ToLower() == "password") ?  true: false;
        }

        private TokenProvider GenerateToken(LoginCredential loginCredential)
        {
            
            Login login = LoginService.FindByUsernameAndPassword(loginCredential.Username, loginCredential.Password);
            if (login != null)
            {
                LoginService.UpdateLastloginDate(login);
                return Authenticate(login);
            }
            else return null;
        }

        private TokenProvider Authenticate(Login login)
        {
            TokenProvider provider = new TokenProvider
            {
                Username = login.Username,
                Role = login.Role
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, login.Role),
                }),

                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token           = tokenHandler.CreateToken(tokenDescriptor);
            provider.Token      = tokenHandler.WriteToken(token);
            provider.Token_type = "Bearer";
            provider.Issued     = token.ValidFrom;
            provider.Expired    = token.ValidTo;
            provider.Expire_in  = Convert.ToInt32((provider.Expired - provider.Issued).TotalSeconds);
            return provider;
        }

    }
}