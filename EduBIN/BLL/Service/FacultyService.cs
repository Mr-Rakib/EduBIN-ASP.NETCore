using EduBIN.BLL.Interface;
using EduBIN.DAL.Repository;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EduBIN.BLL.Service
{
    public class FacultyService : IFaculty
    {
        private static readonly FacultyRepository FacultyRepository = new FacultyRepository();
        private static DesignationsService DesignationsService;
        private static DepartmentsService DepartmentsService;
        private static LoginService LoginService;

        public string Delete(int Id, string CurrentUsername)
        {
            return Messages.AccessDenied;
        }

        public List<Faculty> FindAll(string CurrentUsername)
        {
            DepartmentsService = new DepartmentsService();
            DesignationsService = new DesignationsService();
            List<Faculty> FacultyList =  FacultyRepository.FindAll();

            FacultyList.ForEach(fc => fc.Departments = DepartmentsService.FindById(fc.Departments.Id, CurrentUsername));
            FacultyList.ForEach(fc => fc.Designations = DesignationsService.FindById(fc.Designations.Id, CurrentUsername));

            Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
            if (CurrentLogin != null)
            {
                if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    FacultyList = FacultyList.Where(faculty=> faculty.Login.InstitutionId == CurrentLogin.InstitutionId).ToList();
                    return FacultyList;
                }
                else return FacultyList;
            }
            else return null;
        }

        public Faculty FindById(int Id, string CurrentUsername)
        {
            Faculty Faculty = FacultyRepository.FindById(Id);
            if (Faculty != null)
            {
                DepartmentsService = new DepartmentsService();
                DesignationsService = new DesignationsService();

                Faculty.Departments = DepartmentsService.FindById(Faculty.Departments.Id, CurrentUsername);
                Faculty.Designations = DesignationsService.FindById(Faculty.Designations.Id, CurrentUsername);

                Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
                if (CurrentLogin != null)
                {
                    if (String.Equals(CurrentLogin.Role, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return Faculty;
                    }
                    else if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Faculty.Login.InstitutionId == CurrentLogin.InstitutionId)
                            return Faculty;
                        else return null;
                    }
                    else if (String.Equals(CurrentLogin.Role, Roles.Faculty.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Faculty.Username == CurrentLogin.Username)
                            return Faculty;
                        else return null;
                    }
                    else return null;
                }
                else return null;
            }
            else return null;
        }

        public string Save(Faculty Faculty, string CurrentUsername)
        {
            if (FacultyRepository.FindById(Faculty.Id) == null)
            {
                string messages = ValidateFacultySave(Faculty, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                {
                    SetFacultyValues(Faculty, CurrentUsername);
                    return FacultyRepository.Save(Faculty) ? null : Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.IdExist;
        }

        public string Update(Faculty Faculty, string CurrentUsername)
        {
            if (FindById(Faculty.Id, CurrentUsername) != null)
            {
                SetFacultyUpdateValues(Faculty, CurrentUsername);
                string messages = ValidateFacultyUpdate(Faculty, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                {
                    if (FacultyRepository.Update(Faculty))
                    {
                        return Helper.SetTracerUpdateFaculty(Faculty, CurrentUsername) ? null : Messages.IssueInTracer;
                    }
                    else return Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.IdExist;
        }

        private void SetFacultyUpdateValues(Faculty Faculty, string CurrentUsername)
        {
            string Role     = Faculty.Login.Role;
            
            Faculty FoundedFaculty = FindById(Faculty.Id, CurrentUsername);
            Faculty.Login = FoundedFaculty.Login;
            Faculty.Login.Role = (string.IsNullOrEmpty(Role)) ? FoundedFaculty.Login.Role : Role;

            Faculty.Username = FoundedFaculty.Username;
            Faculty.PersonalInformation.Email = FoundedFaculty.PersonalInformation.Email;

            Faculty.EntryInformation = FoundedFaculty.EntryInformation;
        }

        private void SetFacultyValues(Faculty Faculty, string CurrentUsername)
        {
            Faculty.Username        = Faculty.PersonalInformation.Email;
            Faculty.Departments     = DepartmentsService.FindById(Faculty.Departments.Id, CurrentUsername);
            Faculty.Designations    = DesignationsService.FindById(Faculty.Designations.Id, CurrentUsername);

            Faculty.Login.Username  = Faculty.PersonalInformation.Email;
            Faculty.Login.Password  = Security.Encrypt(Faculty.Login.Password);
            Faculty.Login.Role      = String.IsNullOrEmpty(Faculty.Login.Role) ? Roles.Faculty.ToString() : Faculty.Login.Role;
            Faculty.Login.IsActive  = Status.Enabled;
            Faculty.Login.InstitutionId = (Authorization.IsSuperAdmin(CurrentUsername)) ? Faculty.Login.InstitutionId : Authorization.LoggedInUser(CurrentUsername).InstitutionId;

            Faculty.EntryInformation.EntryById = Authorization.GetCurrentUser(CurrentUsername).Id;
            Faculty.EntryInformation.EntryDate = DateTime.Now;
        }

        public string Enable(int Id, string CurrentUsername)
        {
            Faculty Faculty = FindById(Id, CurrentUsername);
            if (Faculty != null)
            {
                LoginService = new LoginService();
                return LoginService.Enable(Faculty.Login, CurrentUsername);
            }
            else return Messages.FacultyNotExist;
        }

        public string Disable(int Id, string CurrentUsername)
        {
            Faculty Faculty = FindById(Id, CurrentUsername);
            if (Faculty != null)
            {
                LoginService = new LoginService();
                return LoginService.Disable(Faculty.Login, CurrentUsername);
            }
            else return Messages.FacultyNotExist;
        }


        private string ValidateFaculty(Faculty Faculty, string CurrentUsername)
        {
            DepartmentsService = new DepartmentsService();
            if (DepartmentsService.FindById(Faculty.Departments.Id, CurrentUsername) != null)
            {
                DesignationsService = new DesignationsService();
                if (DesignationsService.FindById(Faculty.Designations.Id, CurrentUsername) != null)
                {
                    return null;
                }
                else return Messages.DesignationNotExist;
            }
            else return Messages.DepartmentNotExist;
        }

        private string ValidateFacultySave(Faculty Faculty, string CurrentUsername)
        {
            string message = ValidateFaculty(Faculty, CurrentUsername);
            if (String.IsNullOrEmpty(message))
            {
                LoginService = new LoginService();
                if (LoginService.FindByUsername(Faculty.PersonalInformation.Email) == null)
                {
                    if (!String.IsNullOrEmpty(Faculty.Login.Password))
                    {
                        return null;
                    }
                    else return Messages.EmptyPasswordField;
                }
                else return Messages.UsernameExist;
            }
            else return Messages.DesignationNotExist;
        }

        private string ValidateFacultyUpdate(Faculty Faculty, string CurrentUsername)
        {
            string message = ValidateFaculty(Faculty, CurrentUsername);
            if (String.IsNullOrEmpty(message))
            {
                LoginService = new LoginService();
                var FacultyList = FindAll(CurrentUsername).Where(fc => fc.Id != Faculty.Id).ToList();
                if (FacultyList.Find(fc => fc.Username == Faculty.Username) == null)
                {
                    return null;
                }
                else return Messages.UsernameExist;
            }
            else return Messages.DesignationNotExist;
        }
    }
}
