using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Interface
{
    interface ICRUD<T>
    {
        List<T> FindAll(string CurrentUsername);
        T FindById(int Id, string CurrentUsername);
        string Save(T Type, string CurrentUsername);
        string Update(T Type, string CurrentUsername);
        string Delete(int Id, string CurrentUsername);
    }
}
