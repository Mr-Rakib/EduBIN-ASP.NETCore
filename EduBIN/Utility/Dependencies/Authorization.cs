using EduBIN.BLL.Service;
using EduBIN.DAL.Repository;
using EduBIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Utility.Dependencies
{
    public static class Authorization
    {
        private static LoginService LoginService;
        private static StudentService StudentService;
        private static FacultyService FacultyService;
        
        public static Faculty GetCurrentUser(string CurrentUsername)
        {
            FacultyService = new FacultyService();
            Faculty Faculty = FacultyService.FindAll(CurrentUsername).Find(fc => fc.Username == CurrentUsername);
            return Faculty;
        }

        public static Student GetCurrentStudnet(string CurrentUsername)
        {
            StudentService = new StudentService();
            Student Student = StudentService.FindAll(CurrentUsername).Find(st => st.Username == CurrentUsername);
            return Student;
        }

        public static Login LoggedInUser(string currentUsername)
        {
            LoginService = new LoginService();
            Login Login = LoginService.FindByUsername(currentUsername);
            return Login;
        }

        public static bool IsSuperAdmin(string currentUsername)
        {
            Login CurrentLogin = LoggedInUser(currentUsername);
            return String.Equals(CurrentLogin.Role, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase)
                ? true : false;
        }
        
        public static bool IsAdmin(string currentUsername)
        {
            Login CurrentLogin = LoggedInUser(currentUsername);
            return String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase)
                ? true : false;
        }

        public static bool IsFaculty(string currentUsername)
        {
            Login CurrentLogin = LoggedInUser(currentUsername);
            return String.Equals(CurrentLogin.Role, Roles.Faculty.ToString(), StringComparison.OrdinalIgnoreCase)
                ? true : false;
        }

        public static bool IsStudent(string currentUsername)
        {
            Login CurrentLogin = LoggedInUser(currentUsername);
            return String.Equals(CurrentLogin.Role, Roles.Student.ToString(), StringComparison.OrdinalIgnoreCase)
                ? true : false;
        }

    }
}
