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
    public class LoginRepository : ILoginRepository
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;

        public List<Login> FindAll()
        {
            List<Login> LoginList = new List<Login>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllLogin, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login Login = new Login();
                                GetAllLoginColumns(Login);
                                LoginList.Add(Login);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return LoginList;
        }

        public Login FindByUsername(string usernname)
        {
            Login Login = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetLoginByUsername, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Username", usernname));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login = new Login();
                                GetAllLoginColumns(Login);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Login;
        }

        public bool Save(Login Login)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveLogin, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SetAllParameters(Login);
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

        public bool Update(Login Login)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateLogin, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SetAllParameters(Login);
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

        ///-----------------PRIVATE METHODS--------------------///
        private void SetAllParameters(Login Login)
        {
            command.Parameters.Add(new SqlParameter("@Username", Login.Username));
            command.Parameters.Add(new SqlParameter("@Password", Login.Password));
            command.Parameters.Add(new SqlParameter("@Role", Login.Role));
            command.Parameters.Add(new SqlParameter("@LastLoginDate", Login.LastLoginDate));
            command.Parameters.Add(new SqlParameter("@IsActive", Login.IsActive));
            command.Parameters.Add(new SqlParameter("@InstitutionId", Login.InstitutionId));
        }
        
        private void GetAllLoginColumns(Login Login)
        {
            Login.Username = reader.GetString("username");
            Login.Password = reader.GetString("password");
            Login.Role = ReadNullorString("role");
            Login.LastLoginDate = ReadNullorDateTime("lastlogindate");
            Login.IsActive = reader.GetBoolean("isActive");
            Login.InstitutionId = reader.GetInt32("institution_id");
        }

        private DateTime? ReadNullorDateTime(string column)
        {
            return (reader.IsDBNull(column)) ? (DateTime?)null : reader.GetDateTime(column);
        }

        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }
        ///-----------------------END------------------------///    
    }
}
