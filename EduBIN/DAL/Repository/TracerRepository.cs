using EduBIN.DAL.Interface;
using EduBIN.Models;
using EduBIN.Utility.Database;
using EduBIN.Utility;
using EduBIN.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EduBIN.DAL.Repositories
{
    public class TracerRepository
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        
        public List<Tracer> FindAll()
        {
            List<Tracer> TrackerList = new List<Tracer>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetALLTracer , connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tracer Tracker = new Tracer
                                {
                                    Id                  = reader.GetInt32("id"),
                                    Actor_id            = reader.GetInt32("actor_id"),
                                    ActionName          = reader.GetString("actionName"),
                                    TableName           = reader.GetString("tableName"),
                                    ActionAppliedId    = reader.GetInt32("actionApplied_id"),
                                    ActionTime          = reader.GetDateTime("actionTime"),
                                    InstitutionId      = reader.GetInt32("institution_id")
                                };
                                TrackerList.Add(Tracker);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return TrackerList;
        }

        public bool Save(Tracer Tracker)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveTracer, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Actor_id", Tracker.Actor_id));
                        command.Parameters.Add(new SqlParameter("@ActionName", Tracker.ActionName));
                        command.Parameters.Add(new SqlParameter("@TableName", Tracker.TableName));
                        command.Parameters.Add(new SqlParameter("@ActionAppliedId", Tracker.ActionAppliedId));
                        command.Parameters.Add(new SqlParameter("@ActionTime", Tracker.ActionTime));
                        command.Parameters.Add(new SqlParameter("@InstitutionId", Tracker.InstitutionId));

                        connection.Open();
                        status = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return (status > 0) ? true : false;
        }

    }
}
