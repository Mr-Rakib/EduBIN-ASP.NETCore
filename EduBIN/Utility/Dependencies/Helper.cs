using EduBIN.BLL.Services;
using EduBIN.Models;
using EduBIN.Utility.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EduBIN.Utility.Dependencies
{
    public static class Helper
    {
        //----------------------------------------------- HELPER OF TRACER ----------------------------------------------//
        //-----------------------------------------------------UPDATE----------------------------------------------------//

        public static bool SetTracerUpdateDepartment(Departments Departments, string CurrentUsername)
        {
            return SetUpdateValues(Tables.Departments.ToString(), Departments.Id, Departments.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerUpdatePrograms(Programs Programs, string CurrentUsername)
        {
            return SetUpdateValues(Tables.Departments.ToString(), Programs.Id, Programs.Departments.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerUpdateDesignations(Designations Designations, string CurrentUsername)
        {
            return SetUpdateValues(Tables.Designations.ToString(),Designations.Id, Designations.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerUpdateFaculty(Faculty Faculty, string CurrentUsername)
        {
            return SetUpdateValues(Tables.Faculty.ToString(), Faculty.Id, Faculty.Login.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerUpdateStudent(Student Student, string CurrentUsername)
        {
            return SetUpdateValues(Tables.Student.ToString(), Student.Id, Student.Login.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerUpdateInstitution(Institution Institution, string CurrentUsername)
        {
            if (String.IsNullOrEmpty(CurrentUsername))
            {
                return TracerService.Update(Tables.Institution.ToString(), 1, Institution.Id, Institution.Id);
            }
            else
            {
                return SetUpdateValues(Tables.Institution.ToString(), Institution.Id, Institution.Id, CurrentUsername);
            }
        }

        public static bool SetTracerUpdateLogin(Login Login, string CurrentUsername)
        {
            Faculty Faculty = Authorization.GetCurrentUser(CurrentUsername);
            if (Faculty != null)
            {
                return SetUpdateValues(Tables.Login.ToString(), Faculty.Id, Login.InstitutionId, CurrentUsername);
            }
            else
            {
                Student Student = Authorization.GetCurrentStudnet(CurrentUsername);
                return SetUpdateValues(Tables.Login.ToString(), Student.Id, Login.InstitutionId, CurrentUsername);
            }
        }

        //-----------------------------------------------------DELETE----------------------------------------------------//

        public static bool SetTracerDeleteDepartment(Departments Departments, string CurrentUsername)
        {
            return SetDeleteValues(Tables.Departments.ToString(), Departments.Id, Departments.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerDeleteDesignations(Designations Designations, string CurrentUsername)
        {
            return SetDeleteValues(Tables.Designations.ToString(), Designations.Id, Designations.InstitutionId, CurrentUsername);
        }

        public static bool SetTracerDeletePrograms(Programs Programs, string CurrentUsername)
        {
            return SetDeleteValues(Tables.Programs.ToString(), Programs.Id, Programs.Departments.InstitutionId, CurrentUsername);
        }

        //-------------------------------------------------PRIVATE METHODS------------------------------------------------//

        private static bool SetUpdateValues(string Table, int Id, int InstitutionId, string CurrentUsername)
        {
            return TracerService.Update(Table, Authorization.GetCurrentUser(CurrentUsername).Id, Id, InstitutionId);
        }

        private static bool SetDeleteValues(string Table, int Id, int InstitutionId, string CurrentUsername)
        {
            return TracerService.Delete(Table, Authorization.GetCurrentUser(CurrentUsername).Id, Id, InstitutionId);
        }
    }
}
