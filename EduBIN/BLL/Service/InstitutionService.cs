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
    public class InstitutionService : IInstitution
    {
        private static readonly InstitutionRepository InstitutionRepository = new InstitutionRepository();

        public string Disable(int Id, string CurrentUsername)
        {
            Institution Institution = FindById(Id);
            if (Institution != null)
            {
                Institution.IsActive = Status.Disabled;
                return Update(Institution, CurrentUsername);
            }
            else return Messages.InstitutionNotExist;
        }

        public string Enable(int Id, string CurrentUsername)
        {
            Institution Institution = FindById(Id);
            if (Institution != null)
            {
                Institution.IsActive = Status.Enabled;
                return Update(Institution, CurrentUsername);
            }
            else return Messages.InstitutionNotExist;
        }

        public List<Institution> FindAll()
        {
            List<Institution> InstitutionList = InstitutionRepository.FindAll();
            return InstitutionList;
        }

        public Institution FindById(int Id)
        {
            Institution Institution = InstitutionRepository.FindById(Id);
            return Institution;
        }

        public string Save(Institution Institution)
        {
            if (FindById(Institution.Id) == null)
            {
                //Set basic informations
                Institution.RegistrationDate = DateTime.Now;
                Institution.IsActive = Status.Enabled;
                return InstitutionRepository.Save(Institution) ? null : Messages.IssueInDatabase; 
            }
            else return Messages.IdExist;
        }

        public string Update(Institution Institution, string CurrentUsername)
        {
            if (FindById(Institution.Id) != null)
            {
                if (InstitutionRepository.Update(Institution))
                {
                    return Helper.SetTracerUpdateInstitution(Institution, CurrentUsername) ? null : Messages.IssueInTracer;
                }
                else return Messages.IssueInDatabase;
            }
            else return Messages.IdExist;
        }
    }
}
