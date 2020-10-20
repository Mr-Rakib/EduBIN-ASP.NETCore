using EduBIN.BLL.Service;
using EduBIN.DAL.Repositories;
using EduBIN.Models;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EduBIN.BLL.Services
{
    public static class TracerService 
    {
        public static readonly TracerRepository tracerRepository = new TracerRepository();

        public static List<Tracer> FindAll(string currentUsername)
        {
            List<Tracer> tracers = tracerRepository.FindAll();
            Login CurrentLogin = Authorization.LoggedInUser(currentUsername);
            if (CurrentLogin != null) 
            {
                if (CurrentLogin.Role.ToLower() == Roles.Admin.ToString().ToLower())
                {
                    tracers = tracers.FindAll(st => st.InstitutionId == CurrentLogin.InstitutionId);
                }
            }
            return tracers;
        }

        public static List<Tracer> FindById(int id, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.Id == id);
            return tracers;
        }

        public static List<Tracer> FindByActorID(int id, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.Actor_id == id);
            return tracers;
        }

        public static bool Delete(string table, int actorId, int id , int institutionId )
        {
            Tracer tracer = new Tracer
            {
                Actor_id            = actorId,
                ActionAppliedId     = id,
                ActionName          = "Delete",
                TableName           = table,
                ActionTime          = DateTime.Now,
                InstitutionId       = institutionId
            };

            return tracerRepository.Save(tracer);
        }

        public static bool Update(string table, int actorId, int id, int institutionId)
        {
            Tracer tracer = new Tracer
            {
                Actor_id            = actorId,
                ActionAppliedId     = id,
                ActionName          = "Update",
                TableName           = table,
                ActionTime          = DateTime.Now,
                InstitutionId       = institutionId
            };

            return tracerRepository.Save(tracer);
        }

        public static List<Tracer> FindByInstitution(int id, int userId, string currentUsername)
        {
            List<Tracer> tracers = FindAll(currentUsername).FindAll(tr => tr.InstitutionId == id);
            tracers = userId != 0 ? tracers.FindAll(tr => tr.Actor_id == userId) : tracers;
            return tracers;
        }

    }


}
