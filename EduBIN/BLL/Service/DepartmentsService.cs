using EduBIN.BLL.Interface;
using EduBIN.BLL.Services;
using EduBIN.DAL.Repository;
using EduBIN.Models;
using EduBIN.Utility.Database;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Service
{
    public class DepartmentsService : ICRUD<Departments>
    {
        private static readonly DepartmentsRepository DepartmentsRepository = new DepartmentsRepository();

        public string Delete(int Id, string CurrentUsername)
        {
            Departments Departments = FindById(Id, CurrentUsername);
            if (Departments != null)
            {
                if (DepartmentsRepository.Delete(Id))
                {
                    return Helper.SetTracerDeleteDepartment(Departments, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.NotFound;
        }

        public List<Departments> FindAll(string CurrentUsername)
        {
            List<Departments> DepartmentsList = DepartmentsRepository.FindAll();
            Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
            if (CurrentLogin != null)
            {
                if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    DepartmentsList = DepartmentsList.Where(login => login.InstitutionId == CurrentLogin.InstitutionId).ToList();
                    return DepartmentsList;
                }
                else return DepartmentsList;
            }
            else return null;
        }

        public Departments FindById(int Id, string CurrentUsername)
        {
            Departments Departments = DepartmentsRepository.FindById(Id);
            if (Departments != null)
            {
                Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
                if (CurrentLogin != null)
                {
                    if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Departments.InstitutionId == CurrentLogin.InstitutionId)
                            return Departments;
                        else return null;
                    }
                    else return Departments;
                }
                else return null;
            }
            else return null;
        }

        public string Save(Departments Departments, string CurrentUsername)
        {
            if (FindById(Departments.Id, CurrentUsername) == null)
            {
                SetEntryInformation(Departments, CurrentUsername);
                return DepartmentsRepository.Save(Departments) ? null : Messages.IssueInDatabase;
            }
            else return Messages.IdExist;
        }

        public string Update(Departments Departments, string CurrentUsername) 
        {
            Departments FoundedDepartments = FindById(Departments.Id, CurrentUsername);
            if (FoundedDepartments != null)
            {
                Departments.EntryInformation = FoundedDepartments.EntryInformation;
                if (DepartmentsRepository.Update(Departments)) 
                {
                    return Helper.SetTracerUpdateDepartment(Departments, CurrentUsername) ? null : Messages.IssueInTracer;
                } 
                else return Messages.IssueInDatabase;
            }
            else return Messages.IdExist;
        }

        private void SetEntryInformation(Departments Departments, string CurrentUsername)
        {
            Faculty Faculty = Authorization.GetCurrentUser(CurrentUsername);
            Departments.InstitutionId = (Authorization.IsSuperAdmin(CurrentUsername)) ? Departments.InstitutionId : Faculty.Login.InstitutionId;
            Departments.EntryInformation = new EntryInformation()
            {
                EntryById = Faculty.Id,
                EntryDate = DateTime.Now
            };
        }
    }
}
