using EduBIN.DAL.Interface;
using EduBIN.Models;
using EduBIN.Utility;
using EduBIN.Utility.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EduBIN.DAL.Repository
{
    public class StudentRepository : ICRUDRepository<Student>
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public List<Student> FindAll()
        {
            List<Student> StudentList = new List<Student>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetAllStudent, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student Student = new Student();
                                GetAllStudentColumns(Student);
                                StudentList.Add(Student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return StudentList;
        }

        public Student FindById(int Id)
        {
            Student Student = null;
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new SqlCommand(Procedures.GetStudentById, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", Id));

                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student = new Student();
                                GetAllStudentColumns(Student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return Student;
        }

        public bool Save(Student Student)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.SaveStudent, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Student);
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

        public bool Update(Student Student)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new SqlCommand(Procedures.UpdateStudent, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@Id", Student.Id));
                        SetAllParameters(Student);
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
                    using (command = new SqlCommand(Procedures.DeleteStudent, connection))
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
        private void SetAllParameters(Student Student)
        {
            command.Parameters.Add(new SqlParameter("@Username", Student.Username));
            command.Parameters.Add(new SqlParameter("@GuardianName", Student.Guardian.Name));
            command.Parameters.Add(new SqlParameter("@GuardianContact", Student.Guardian.Contact));
            command.Parameters.Add(new SqlParameter("@GuardianAddress", Student.Guardian.Address));

            command.Parameters.Add(new SqlParameter("@ProgramsId", Student.Programs.Id));
            
            command.Parameters.Add(new SqlParameter("@FullName", Student.PersonalInformation.FullName));
            command.Parameters.Add(new SqlParameter("@FathersName", Student.PersonalInformation.FathersName));
            command.Parameters.Add(new SqlParameter("@MothersName", Student.PersonalInformation.MothersName));
            command.Parameters.Add(new SqlParameter("@Gender", Student.PersonalInformation.Gender));
            command.Parameters.Add(new SqlParameter("@DateOfBirth", Student.PersonalInformation.DateOfBirth));
            command.Parameters.Add(new SqlParameter("@Nationality", Student.PersonalInformation.Nationality));
            command.Parameters.Add(new SqlParameter("@Image", Student.PersonalInformation.Image));
            command.Parameters.Add(new SqlParameter("@Contact", Student.PersonalInformation.Contact));
            command.Parameters.Add(new SqlParameter("@Email", Student.PersonalInformation.Email));
            command.Parameters.Add(new SqlParameter("@Address", Student.PersonalInformation.Address));

            command.Parameters.Add(new SqlParameter("@Password", Student.Login.Password));
            command.Parameters.Add(new SqlParameter("@Role", Student.Login.Role));
            command.Parameters.Add(new SqlParameter("@IsActive", Student.Login.IsActive));
            command.Parameters.Add(new SqlParameter("@InstitutionId", Student.Login.InstitutionId));

            command.Parameters.Add(new SqlParameter("@EntryById", Student.EntryInformation.EntryById));
            command.Parameters.Add(new SqlParameter("@EntryDate", Student.EntryInformation.EntryDate));
        }

        private void GetAllStudentColumns(Student Student)
        {
            Student.Id = reader.GetInt32("id");
            Student.Username = reader.GetString("username");

            Student.Guardian = new Guardian
            {
                Name = ReadNullorString("guardianName"),
                Contact = ReadNullorString("guardianContact"),
                Address = ReadNullorString("guardianAddress")
            };

            Student.Programs = new Programs()
            {
                Id = reader.GetInt32("program_id")
            };

            Student.PersonalInformation = new PersonalInformation()
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

            Student.Login = new Login
            {
                Username = reader.GetString("username"),
                Password = reader.GetString("password"),
                Role = ReadNullorString("role"),
                LastLoginDate = ReadNullorDateTime("lastlogindate"),
                IsActive = reader.GetBoolean("isActive"),
                InstitutionId = reader.GetInt32("institution_id")
            };

            Student.EntryInformation = new EntryInformation()
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
        ///-------------------------END--------------------------///
    }
}
