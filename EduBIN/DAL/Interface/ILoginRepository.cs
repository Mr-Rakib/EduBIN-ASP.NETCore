using EduBIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.DAL.Interface
{
    interface ILoginRepository
    {
        public List<Login> FindAll();
        public Login FindByUsername(string usernname);
        public bool Update(Login Login);
    }
}
