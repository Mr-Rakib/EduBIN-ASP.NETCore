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
    public class StudentService : IStudent
    {
        private static readonly StudentRepository StudentRepository = new StudentRepository();
        private static ProgramsService ProgramsService;
        private static LoginService LoginService;

        public string Delete(int Id, string CurrentUsername)
        {
            return Messages.AccessDenied;
        }

        public List<Student> FindAll(string CurrentUsername)
        {
            ProgramsService = new ProgramsService();
            List<Student> StudentList = StudentRepository.FindAll();

            StudentList.ForEach(st => st.Programs = ProgramsService.FindById(st.Programs.Id, CurrentUsername));

            Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
            if (CurrentLogin != null)
            {
                if(String.Equals(CurrentLogin.Role, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return StudentList;
                }
                else
                {
                    StudentList = StudentList.Where(Student => Student.Login.InstitutionId == CurrentLogin.InstitutionId).ToList();
                    return StudentList;
                }
            }
            else return null;
        }

        public Student FindById(int Id, string CurrentUsername)
        {
            Student Student = StudentRepository.FindById(Id);
            if (Student != null)
            {
                ProgramsService = new ProgramsService();
                Student.Programs = ProgramsService.FindById(Student.Programs.Id, CurrentUsername);
                
                Login CurrentLogin = Authorization.LoggedInUser(CurrentUsername);
                if (CurrentLogin != null)
                {
                    if (String.Equals(CurrentLogin.Role, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return Student;
                    }
                    else if (String.Equals(CurrentLogin.Role, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Student.Login.InstitutionId == CurrentLogin.InstitutionId)
                            return Student;
                        else return null;
                    }
                    else if (String.Equals(CurrentLogin.Role, Roles.Student.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Student.Username == CurrentLogin.Username)
                            return Student;
                        else return null;
                    }
                    else return null;
                }
                else return null;
            }
            else return null;
        }

        public string Save(Student Student, string CurrentUsername)
        {
            if (StudentRepository.FindById(Student.Id) == null)
            {
                string messages = ValidateStudent(Student, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                {
                    SetStudentValues(Student, CurrentUsername);
                    return StudentRepository.Save(Student) ? null : Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.IdExist;
        }

        private string ValidateStudent(Student Student, string CurrentUsername)
        {
            ProgramsService = new ProgramsService();
            if (ProgramsService.FindById(Student.Programs.Id, CurrentUsername) != null)
            {
                LoginService = new LoginService();
                if (LoginService.FindByUsername(Student.PersonalInformation.Email) == null)
                {
                    if (!String.IsNullOrEmpty(Student.Login.Password))
                    {
                        return null;
                    }
                    else return Messages.EmptyPasswordField;
                }
                else return Messages.UsernameExist;
            }
            else return Messages.DepartmentNotExist;
        }

        public string Update(Student Student, string CurrentUsername)
        {
            if (FindById(Student.Id, CurrentUsername) != null)
            {
                SetStudentUpdateValues(Student, CurrentUsername);
                string messages = ValidateStudent(Student, CurrentUsername);
                if (String.IsNullOrEmpty(messages))
                {
                    if (StudentRepository.Update(Student))
                    {
                        return Helper.SetTracerUpdateStudent(Student, CurrentUsername) ? null : Messages.IssueInTracer;
                    }
                    else return Messages.IssueInDatabase;
                }
                else return messages;
            }
            else return Messages.IdExist;
        }

        private void SetStudentUpdateValues(Student Student, string CurrentUsername)
        {
            string Role = Student.Login.Role;

            Student FoundedStudent = FindById(Student.Id, CurrentUsername);
            Student.Login = FoundedStudent.Login;
            Student.Login.Role = (string.IsNullOrEmpty(Role)) ? FoundedStudent.Login.Role : Role;

            Student.Username = FoundedStudent.Username;
            Student.PersonalInformation.Email = FoundedStudent.PersonalInformation.Email;

            Student.EntryInformation = FoundedStudent.EntryInformation;
        }

        private void SetStudentValues(Student Student, string CurrentUsername)
        {
            Student.Username = Student.PersonalInformation.Email;
            Student.Programs = ProgramsService.FindById(Student.Programs.Id, CurrentUsername);

            Student.Login.Username = Student.PersonalInformation.Email;
            Student.Login.Password = Security.Encrypt(Student.Login.Password);
            Student.Login.Role = Roles.Student.ToString();
            Student.Login.IsActive = Status.Enabled;
            Student.Login.InstitutionId = (Authorization.IsSuperAdmin(CurrentUsername)) ? Student.Login.InstitutionId : Authorization.LoggedInUser(CurrentUsername).InstitutionId;
            
            Student.EntryInformation.EntryById = Authorization.GetCurrentUser(CurrentUsername).Id;
            Student.EntryInformation.EntryDate = DateTime.Now;
        }

        public string Enable(int Id, string CurrentUsername)
        {
            Student Student = FindById(Id, CurrentUsername);
            if (Student != null)
            {
                LoginService = new LoginService();
                return LoginService.Enable(Student.Login, CurrentUsername);
            }
            else return Messages.StudentNotExist;
        }

        public string Disable(int Id, string CurrentUsername)
        {
            Student Student = FindById(Id, CurrentUsername);
            if (Student != null)
            {
                LoginService = new LoginService();
                return LoginService.Disable(Student.Login, CurrentUsername);
            }
            else return Messages.StudentNotExist;
        }
    }
}
