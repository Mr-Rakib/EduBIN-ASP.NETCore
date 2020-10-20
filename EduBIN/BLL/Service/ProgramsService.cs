using EduBIN.BLL.Interface;
using EduBIN.DAL.Repository;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.BLL.Service
{
    public class ProgramsService : ICRUD<Programs>
    {
        private static readonly ProgramsRepository ProgramsRepository = new ProgramsRepository();
        private static DepartmentsService DepartmentsService;

        public string Delete(int Id, string CurrentUsername)
        {
            Programs Programs = FindById(Id, CurrentUsername);
            if (Programs != null)
            {
                if (ProgramsRepository.Delete(Id))
                {
                    return Helper.SetTracerDeletePrograms(Programs, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.NotFound;
        }

        public List<Programs> FindAll(string CurrentUsername)
        {
            DepartmentsService = new DepartmentsService();
            Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
            List<Programs> ProgramsList = ProgramsRepository.FindAll();
            if (ProgramsList.Count > 0)
            {
                if (CurrentLogin != null)
                {
                    ProgramsList.ForEach(pr => pr.Departments = new DepartmentsRepository().FindById(pr.Departments.Id));
                    if (String.Equals(CurrentLogin.Role, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return ProgramsList;
                    }
                    else
                    {
                        ProgramsList = ProgramsList.Where(pr => pr.Departments.InstitutionId == CurrentLogin.InstitutionId).ToList();
                        return ProgramsList;
                    }
                }
                else return null;
            }
            else return null;
        }

        public Programs FindById(int Id, string CurrentUsername)
        {
            Programs Programs = ProgramsRepository.FindById(Id);
            if (Programs != null)
            {
                DepartmentsService = new DepartmentsService();
                Programs.Departments = DepartmentsService.FindById(Programs.Departments.Id, CurrentUsername);
                
                Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
                if (CurrentLogin != null)
                {
                    if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Programs.Departments.InstitutionId == CurrentLogin.InstitutionId)
                            return Programs;
                        else return null;
                    }
                    else return Programs;
                }
                else return null;
            }
            else return null;
        }

        public string Save(Programs Programs, string CurrentUsername)
        {
            if (FindById(Programs.Id, CurrentUsername) == null)
            {
                string messages = ValidatePrograms(Programs, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                {
                    SetProgramsValues(Programs, CurrentUsername);
                    return ProgramsRepository.Save(Programs) ? null : Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.IdExist;
        }

        public string Update(Programs Programs, string CurrentUsername)
        {
            Programs FoundedPrograms = FindById(Programs.Id, CurrentUsername);
            if (FoundedPrograms != null)
            {
                SetProgramsUpdateValues(Programs, CurrentUsername);
                string messages = ValidatePrograms(Programs, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                { 
                    if (ProgramsRepository.Update(Programs))
                    {
                        return Helper.SetTracerUpdatePrograms(Programs, CurrentUsername) ? null : Messages.IssueInTracer;
                    }
                    else return Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.NotFound;
        }

        private string ValidatePrograms(Programs Programs, string CurrentUsername)
        {
            DepartmentsService = new DepartmentsService();
            if (DepartmentsService.FindById(Programs.Departments.Id, CurrentUsername) != null)
            {
                return null;
            }
            else return Messages.DepartmentNotExist;
        }
        
        private void SetProgramsUpdateValues(Programs Programs, string CurrentUsername)
        {
            Programs FoundedPrograms = FindById(Programs.Id, CurrentUsername);
            Programs.Departments = DepartmentsService.FindById(Programs.Departments.Id, CurrentUsername);
            Programs.EntryInformation = FoundedPrograms.EntryInformation;
        }

        private void SetProgramsValues(Programs Programs, string CurrentUsername)
        {
            Programs.Departments = DepartmentsService.FindById(Programs.Departments.Id, CurrentUsername);
            Programs.EntryInformation = new EntryInformation()
            {
                EntryById = Authorization.GetCurrentUser(CurrentUsername).Id,
                EntryDate = DateTime.Now
            };
        }
    }
}