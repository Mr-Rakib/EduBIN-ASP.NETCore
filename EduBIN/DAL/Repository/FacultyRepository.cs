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
    public class FacultyRepository : ICRUDRepository<Faculty>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Faculty> FindAll()
        {
            List<Faculty> FacultyList = new List<Faculty>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllFaculty, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Faculty Faculty = new Faculty();
                                GetAllFacultyColumns(Faculty);
                                FacultyList.Add(Faculty);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return FacultyList;
        }

        public Faculty FindById(int Id)
        {
            Faculty Faculty = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetFacultyById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Faculty = new Faculty();
                                GetAllFacultyColumns(Faculty);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Faculty;
        }

        public bool Save(Faculty Faculty)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveFaculty, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Faculty);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            }
            return true;
        }

        public bool Update(Faculty Faculty)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateFaculty, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Id", Faculty.Id));
                        SetAllParameters(Faculty);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            }
            return true;
        }

        public bool Delete(int Id)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.DeleteFaculty, connection))
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
        private void SetAllParameters(Faculty Faculty)
        {
            command.Parameters.Add(new SqlParameter("@Username", Faculty.Username));
            if (String.IsNullOrEmpty(Faculty.NIDNumber))
                command.Parameters.Add(new SqlParameter("@NIDNumber", DBNull.Value));

            else
                command.Parameters.Add(new SqlParameter("@NIDNumber", Faculty.NIDNumber));

            command.Parameters.Add(new SqlParameter("@DesignationId", Faculty.Designations.Id));
            command.Parameters.Add(new SqlParameter("@DepartmentId", Faculty.Departments.Id));

            command.Parameters.Add(new SqlParameter("@FullName", Faculty.PersonalInformation.FullName));
            command.Parameters.Add(new SqlParameter("@FathersName", Faculty.PersonalInformation.FathersName));
            command.Parameters.Add(new SqlParameter("@MothersName", Faculty.PersonalInformation.MothersName));
            command.Parameters.Add(new SqlParameter("@Gender", Faculty.PersonalInformation.Gender));
            command.Parameters.Add(new SqlParameter("@DateOfBirth", Faculty.PersonalInformation.DateOfBirth));
            command.Parameters.Add(new SqlParameter("@Nationality", Faculty.PersonalInformation.Nationality));
            command.Parameters.Add(new SqlParameter("@Image", Faculty.PersonalInformation.Image));
            command.Parameters.Add(new SqlParameter("@Contact", Faculty.PersonalInformation.Contact));
            command.Parameters.Add(new SqlParameter("@Email", Faculty.PersonalInformation.Email));
            command.Parameters.Add(new SqlParameter("@Address", Faculty.PersonalInformation.Address));

            command.Parameters.Add(new SqlParameter("@Password", Faculty.Login.Password));
            command.Parameters.Add(new SqlParameter("@Role", Faculty.Login.Role));
            command.Parameters.Add(new SqlParameter("@IsActive", Faculty.Login.IsActive));
            command.Parameters.Add(new SqlParameter("@InstitutionId", Faculty.Login.InstitutionId));

            command.Parameters.Add(new SqlParameter("@EntryById", Faculty.EntryInformation.EntryById));
            command.Parameters.Add(new SqlParameter("@EntryDate", Faculty.EntryInformation.EntryDate));
        }

        private void GetAllFacultyColumns(Faculty Faculty)
        {
            Faculty.Id = reader.GetInt32("id");
            Faculty.Username = reader.GetString("username");
            Faculty.NIDNumber = ReadNullorString("nidNumber");
            Faculty.Designations = new Designations()
            {
                Id = reader.GetInt32("designation_id")
            };
            Faculty.Departments = new Departments()
            {
                Id = reader.GetInt32("department_id")
            };

            Faculty.PersonalInformation = new PersonalInformation()
            {
                Username = reader.GetString("username"),
                FullName = reader.GetString("fullName"),
                FathersName = reader.GetString("fatherName"),
                MothersName = reader.GetString("motherName"),
                Gender = reader.GetString("gender"),
                DateOfBirth = ReadNullorDateTime("dateOfBirth"),
                Nationality = reader.GetString("nationality"),
                Image = ReadNullorString("image"),
                Contact = reader.GetString("contact"),
                Email = reader.GetString("email"),
                Address = ReadNullorString("address"),
            };

            Faculty.Login = new Login
            {
                Username = reader.GetString("username"),
                Password = reader.GetString("password"),
                Role = ReadNullorString("role"),
                LastLoginDate = ReadNullorDateTime("lastlogindate"),
                IsActive = reader.GetBoolean("isActive"),
                InstitutionId = reader.GetInt32("institution_id")
            };

            Faculty.EntryInformation = new EntryInformation()
            {
                EntryById = reader.GetInt32("EntryBy_id"),
                EntryDate = reader.GetDateTime("EntryDate")
            };
        }

        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }

        private DateTime? ReadNullorDateTime(string column)
        {
            return (reader.IsDBNull(column)) ? (DateTime?)null : reader.GetDateTime(column);
        }
        ///-----------------------END------------------------///
    }
}
