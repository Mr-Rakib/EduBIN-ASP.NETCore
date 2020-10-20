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
    public class DesignationsService : ICRUD<Designations>
    {
        private static readonly DesignationsRepository DesignationsRepository = new DesignationsRepository();

        public string Delete(int Id, string CurrentUsername)
        {
            Designations Designations = FindById(Id, CurrentUsername);
            if (Designations != null)
            {
                if (DesignationsRepository.Delete(Id))
                {
                    return Helper.SetTracerDeleteDesignations(Designations, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.NotFound;
        }

        public List<Designations> FindAll(string CurrentUsername)
        {
            List<Designations> DesignationsList = DesignationsRepository.FindAll();
            Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
            if (CurrentLogin != null)
            {
                if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    DesignationsList = DesignationsList.Where(login => login.InstitutionId == CurrentLogin.InstitutionId).ToList();
                    return DesignationsList;
                }
                else return DesignationsList;
            }
            else return null;
        }

        public Designations FindById(int Id, string CurrentUsername)
        {
            Designations Designations = DesignationsRepository.FindById(Id);
            if (Designations != null)
            {
                Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
                if (CurrentLogin != null)
                {
                    if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Designations.InstitutionId == CurrentLogin.InstitutionId)
                            return Designations;
                        else return null;
                    }
                    else return Designations;
                }
                else return null;
            }
            else return null;
        }

        public string Save(Designations Designations, string CurrentUsername)
        {
            if (FindById(Designations.Id, CurrentUsername) == null)
            {
                SetEntryInformation(Designations, CurrentUsername);
                return DesignationsRepository.Save(Designations) ? null : Messages.IssueInDatabase;
            }
            else return Messages.IdExist;
        }

        public string Update(Designations Designations, string CurrentUsername)
        {
            Designations FoundedDesignations = FindById(Designations.Id, CurrentUsername);
            if (FoundedDesignations != null)
            {
                Designations.EntryInformation = FoundedDesignations.EntryInformation;
                if (DesignationsRepository.Update(Designations))
                {
                    return Helper.SetTracerUpdateDesignations(Designations, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.IdExist;
        }

        private void SetEntryInformation(Designations Designations, string CurrentUsername)
        {
            Faculty Faculty = Authorization.GetCurrentUser(CurrentUsername);
            Designations.InstitutionId = (Authorization.IsSuperAdmin(CurrentUsername)) ? Designations.InstitutionId : Faculty.Login.InstitutionId;
            Designations.EntryInformation = new EntryInformation()
            {
                EntryById = Faculty.Id,
                EntryDate = DateTime.Now
            };
        }
    }
}
