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
    public class InstitutionRepository : ICRUDRepository<Institution>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Institution> FindAll()
        {
            List<Institution> InstitutionList = new List<Institution>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Institution Institution = new Institution();
                                GetAllInstitutionColumns(Institution);
                                InstitutionList.Add(Institution);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return InstitutionList;
        }

        public Institution FindById(int Id)
        {
            Institution Institution = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetInstitutionById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Institution = new Institution();
                                GetAllInstitutionColumns(Institution);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Institution;
        }
 
        public bool Save(Institution Institution)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Institution);
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

        public bool Update(Institution Institution)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@id", Institution.Id));
                        SetAllParameters(Institution);
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
            return false;
        }

        ///-------------------PRIVATE METHODS-----------------------///
        private void SetAllParameters(Institution Institution)
        {
            command.Parameters.Add(new SqlParameter("@Name", Institution.Name));
            command.Parameters.Add(new SqlParameter("@Contact", Institution.Contact));
            command.Parameters.Add(new SqlParameter("@Email", Institution.Email));
            command.Parameters.Add(new SqlParameter("@Address", Institution.Address));
            command.Parameters.Add(new SqlParameter("@Logo", Institution.Logo));
            command.Parameters.Add(new SqlParameter("@RegistrationDate", Institution.RegistrationDate));
            command.Parameters.Add(new SqlParameter("@IsActive", Institution.IsActive));
        }
        private void GetAllInstitutionColumns(Institution Institution)
        {
            Institution.Id = reader.GetInt32("id");
            Institution.Name = reader.GetString("name");
            Institution.Contact = ReadNullorString("contact");
            Institution.Email = ReadNullorString("email");
            Institution.Address = reader.GetString("address");

            Institution.Logo = ReadNullorString("logo");
            Institution.RegistrationDate = reader.GetDateTime("registrationDate");
            Institution.IsActive = reader.GetBoolean("isActive");
        }
        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }
        ///-----------------------END------------------------///      
    }
}