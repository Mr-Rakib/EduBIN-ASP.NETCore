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
    public class ProgramsRepository : ICRUDRepository<Programs>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Programs> FindAll()
        {
            List<Programs> ProgramsList = new List<Programs>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllPrograms, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Programs Programs = new Programs();
                                GetAllProgramsColumns(Programs);
                                ProgramsList.Add(Programs);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return ProgramsList;
        }

        public Programs FindById(int Id)
        {
            Programs Programs = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetProgramsById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Programs = new Programs();
                                GetAllProgramsColumns(Programs);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Programs;
        }

        public bool Save(Programs Programs)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SavePrograms, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Programs);
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

        public bool Update(Programs Programs)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdatePrograms, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Id", Programs.Id));
                        SetAllParameters(Programs);
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
                    using (command = new SqlCommand(Procedures.DeletePrograms, connection))
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
        private void SetAllParameters(Programs Programs)
        {
            command.Parameters.Add(new SqlParameter("@Name", Programs.Name));
            command.Parameters.Add(new SqlParameter("@Descriptions", Programs.Descriptions));
            command.Parameters.Add(new SqlParameter("@DepartmentsId", Programs.Departments.Id));
            command.Parameters.Add(new SqlParameter("@EntryById", Programs.EntryInformation.EntryById));
            command.Parameters.Add(new SqlParameter("@EntryDate", Programs.EntryInformation.EntryDate));
        }
        private void GetAllProgramsColumns(Programs Programs)
        {
            Programs.Id = reader.GetInt32("id");
            Programs.Name = reader.GetString("name");
            Programs.Descriptions = ReadNullorString("descriptions");
            Programs.Departments = new Departments()
            {
                Id = reader.GetInt32("department_id")
            };
            Programs.EntryInformation = new EntryInformation()
            {
                EntryById = reader.GetInt32("EntryBy_id"),
                EntryDate = reader.GetDateTime("EntryDate")
            };
        }
        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }
        ///-----------------------END------------------------/// 
    }
}
