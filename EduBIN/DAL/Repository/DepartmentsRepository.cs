using EduBIN.DAL.Interface;
using EduBIN.Models;
using EduBIN.Utility;
using EduBIN.Utility.Database;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.DAL.Repository
{
    public class DepartmentsRepository : ICRUDRepository<Departments>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Departments> FindAll()
        {
            List<Departments> DepartmentsList = new List<Departments>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllDepartments, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Departments Departments = new Departments();
                                GetAllDepartmentsColumns(Departments);
                                DepartmentsList.Add(Departments);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return DepartmentsList;
        }

        public Departments FindById(int Id)
        {
            Departments Departments = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetDepartmentsById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Departments = new Departments();
                                GetAllDepartmentsColumns(Departments);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Departments;
        }

        public bool Save(Departments Departments)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveDepartments, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Departments);
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

        public bool Update(Departments Departments)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateDepartments, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Id", Departments.Id));
                        SetAllParameters(Departments);
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
                    using (command = new SqlCommand(Procedures.DeleteDepartments, connection))
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
        private void SetAllParameters(Departments Departments)
        {
            command.Parameters.Add(new SqlParameter("@Name", Departments.Name));
            command.Parameters.Add(new SqlParameter("@Descriptions", Departments.Descriptions));
            command.Parameters.Add(new SqlParameter("@InstitutionId", Departments.InstitutionId));
            command.Parameters.Add(new SqlParameter("@EntryById", Departments.EntryInformation.EntryById));
            command.Parameters.Add(new SqlParameter("@EntryDate", Departments.EntryInformation.EntryDate));
        }
        private void GetAllDepartmentsColumns(Departments Departments)
        {
            Departments.Id = reader.GetInt32("id");
            Departments.Name = reader.GetString("name");
            Departments.Descriptions = ReadNullorString("descriptions");
            Departments.InstitutionId = reader.GetInt32("institution_id");
            Departments.EntryInformation = new EntryInformation()
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
