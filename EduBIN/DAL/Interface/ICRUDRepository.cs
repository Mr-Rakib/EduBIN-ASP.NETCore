using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.DAL.Interface
{
    interface ICRUDRepository<T>
    {
        List<T> FindAll();
        T FindById(int Id);
        bool Save(T Type);
        bool Update(T Type);
        bool Delete(int Id);
    }
}
