using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Utility.Database
{
    public static class Procedures
    {
        //-------------------------------Find All-------------------------------------//
        public static string GetAllLogin        = "sp_GetAllLogin";
        public static string GetALLTracer       = "sp_GetAllTracer";
        public static string GetAllFaculty      = "sp_GetAllFaculty";
        public static string GetAllStudent      = "sp_GetAllStudent";
        public static string GetAllPrograms     = "sp_GetAllPrograms";
        public static string GetAllDepartments  = "sp_GetAllDepartments";
        public static string GetAllInstitution  = "sp_GetAllInstitution";
        public static string GetAllDesignations = "sp_GetAllDesignations";

        //-------------------------------Save-------------------------------------//
        public static string SaveLogin          = "sp_SaveLogin";
        public static string SaveTracer         = "sp_SaveTracer";
        public static string SaveStudent        = "sp_SaveStudent";
        public static string SaveFaculty        = "sp_SaveFaculty";
        public static string SavePrograms       = "sp_SavePrograms";
        public static string SaveDepartments    = "sp_SaveDepartments";
        public static string SaveInstitution    = "sp_SaveInstitution";
        public static string SaveDesignations   = "sp_SaveDesignations";
        
        //-------------------------------Update-------------------------------------//
        public static string UpdateLogin        = "sp_UpdateLogin";
        public static string UpdateStudent      = "sp_UpdateStudent";
        public static string UpdateFaculty      = "sp_UpdateFaculty";
        public static string UpdatePrograms     = "sp_UpdatePrograms";
        public static string UpdateDepartments  = "sp_UpdateDepartments";
        public static string UpdateInstitution  = "sp_UpdateInstitution";
        public static string UpdateDesignations = "sp_UpdateDesignations";

        //-------------------------------Delete-------------------------------------//
        public static string DeleteStudent      = "sp_DeleteStudent";
        public static string DeleteFaculty      = "sp_DeleteFaculty";
        public static string DeletePrograms     = "sp_DeletePrograms";
        public static string DeleteDepartments  = "sp_DeleteDepartments";
        public static string DeleteDesignations = "sp_DeleteDesignations";

        //-------------------------------Others-------------------------------------//
        public static string GetFacultyById     = "sp_GetFacultyById";
        public static string GetStudentById     = "sp_GetStudentById";
        public static string GetProgramsById    = "sp_GetProgramsById";
        public static string GetLoginByUsername = "sp_GetLoginByUsername";
        public static string GetInstitutionById = "sp_GetInstitutionById";
        public static string GetDepartmentsById = "sp_GetDepartmentsById";
        public static string GetDesignationsById= "sp_GetDesignationsById";

    }
}
