﻿using EduBIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Interface
{
    interface IStudent : ICRUD<Student>
    {
        string Enable(int Id, string CurrentUsername);
        string Disable(int Id, string CurrentUsername);
    }
}
