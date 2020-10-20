using EduBIN.DAL.Interface;
using EduBIN.Models;
using EduBIN.Utility;
using EduBIN.Utility.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.DAL.Repository
{
    public class DesignationsRepository : ICRUDRepository<Designations>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Designations> FindAll()
        {
            List<Designations> DesignationsList = new List<Designations>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllDesignations, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Designations Designations = new Designations();
                                GetAllDesignationsColumns(Designations);
                                DesignationsList.Add(Designations);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return DesignationsList;
        }

        public Designations FindById(int Id)
        {
            Designations Designations = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetDesignationsById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Designations = new Designations();
                                GetAllDesignationsColumns(Designations);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Designations;
        }

        public bool Save(Designations Designations)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveDesignations, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Designations);
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

        public bool Update(Designations Designations)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateDesignations, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Id", Designations.Id));
                        SetAllParameters(Designations);
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

        public bool Delete(int Id)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.DeleteDesignations, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

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

        ///-------------------PRIVATE METHODS-----------------------///
        private void SetAllParameters(Designations Designations)
        {
            command.Parameters.Add(new SqlParameter("@Name", Designations.Name));
            command.Parameters.Add(new SqlParameter("@InstitutionId", Designations.InstitutionId));
            command.Parameters.Add(new SqlParameter("@EntryById", Designations.EntryInformation.EntryById));
            command.Parameters.Add(new SqlParameter("@EntryDate", Designations.EntryInformation.EntryDate));
        }
        private void GetAllDesignationsColumns(Designations Designations)
        {
            Designations.Id = reader.GetInt32("id");
            Designations.Name = reader.GetString("name");
            Designations.InstitutionId = reader.GetInt32("institution_id");
            Designations.EntryInformation = new EntryInformation()
            {
                EntryById = reader.GetInt32("EntryBy_id"),
                EntryDate = reader.GetDateTime("EntryDate")
            };
        }
        ///-----------------------END------------------------/// 
    }
}
