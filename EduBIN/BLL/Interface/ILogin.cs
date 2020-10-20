using EduBIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Interface
{
    interface ILogin
    {
        List<Login> FindAll(string CurrentUsername);
        Login FindByUsername(string Username);
        Login FindByUsernameAndPassword(string Username, string Password);
        string Save(Login Login, string CurrentUsername);
        string Update(Login Login, string CurrentUsername);
        public string UpdateLastloginDate(Login login);
        string Enable(Login Login, string CurrentUsername);
        string Disable(Login Login, string CurrentUsername);
    }
}
