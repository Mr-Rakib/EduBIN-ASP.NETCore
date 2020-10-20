using EduBIN.BLL.Interface;
using EduBIN.DAL.Repository;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Service
{
    public class LoginService : ILogin
    {
        private static readonly LoginRepository LoginRepository = new LoginRepository();

        public List<Login> FindAll(string CurrentUsername)
        {
            List<Login> LoginList = LoginRepository.FindAll();
            Login CurrentLogin = FindByUsername(CurrentUsername);
            if(CurrentLogin != null)
            {
                if(String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    LoginList = LoginList.Where(login => login.InstitutionId == CurrentLogin.InstitutionId).ToList();
                }
            }
            return LoginList;
        }

        public Login FindByUsername(string Username)
        {
            Login Login = LoginRepository.FindByUsername(Username);
            return Login;
        }

        public Login FindByUsernameAndPassword(string Username, string Password)
        {
            Login Login = FindByUsername(Username);
            if(Login != null)
            {
                Password = Security.Encrypt(Password);
                return (Login.Password == Password) ? Login : null;
            }
            return Login;
        }

        public string Save(Login Login, string CurrentUsername)
        {
            if (FindByUsername(Login.Username) == null)
            {
                Login.IsActive = Status.Enabled;
                Login.Password = Security.Encrypt(Login.Password);
                return LoginRepository.Save(Login) ? null : Messages.IssueInDatabase;
            }
            else return Messages.UsernameExist;
        }

        public string Update(Login Login, string CurrentUsername)
        {
            if (FindByUsername(Login.Username) != null)
            {
                if (LoginRepository.Update(Login))
                {
                    return Helper.SetTracerUpdateLogin(Login, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.UsernameExist;
        }

        public string UpdateLastloginDate(Login login)
        {
            login.LastLoginDate = DateTime.Now;
            return Update(login);
        }

        public string Enable(Login Login, string CurrentUsername)
        {
            if (Login != null)
            {
                Login.IsActive = Status.Enabled;
                return Update(Login, CurrentUsername);
            }
            else return Messages.InvalidUser;
        }

        public string Disable(Login Login, string CurrentUsername)
        {
            if (Login != null)
            {
                Login.IsActive = Status.Disabled;
                return Update(Login, CurrentUsername);
            }
            else return Messages.InvalidUser;
        }
        
        //Override Method
        private string Update(Login Login)
        {
            if (FindByUsername(Login.Username) != null)
            {
                return LoginRepository.Update(Login) ? null : Messages.IssueInDatabase;
            }
            else return Messages.UsernameExist;
        }
    }
}
