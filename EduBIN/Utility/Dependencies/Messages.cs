﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Utility.Dependencies
{
    public static class Messages
    {
        public static string Empty                  = "No data found ! It's Empty";
        public static string Enable                 = "User successfully Enabled !";
        public static string Disable                = "User successfully disabled !";
        public static string NotActive              = "Item not Active now";

        public static string Saved                  = "Items successfully saved !";
        public static string Updated                = "Items successfully updated !";
        public static string Deleted                = "Items successfully deleted !";
        public static string PasswordUpdated        = "Password successfully Updated !";

        public static string AccessDenied           = "You are unable to access this property !";
        public static string InstitutionNotActive   = "Your institution unable to access this property!";

        public static string Exist                  = "Items already exist !";
        public static string IdExist                = "Id already exist !";
        public static string Conflict               = "Items Conflicts !";
        public static string emailExist             = "Email already exist !";
        public static string Unauthorize            = "Unauthorize User !";
        public static string ContactExist           = "contact already exist !";
        public static string UsernameExist          = "Username already exist !";
        public static string StaffNotExist          = "Staff Not Exist !";
        public static string FacultyNotExist        = "Faculty Not Exist";
        public static string StudentNotExist        = "Student Not Exist !";
        public static string AttendanceExist        = "User Already Present Today !";
        public static string DepartmentNotExist     = "Department Not Exist !";
        public static string InstitutionNotExist    = "Institution not exit !";
        public static string CurrentUserNotExist    = "Current User not exit !";
        public static string DesignationNotExist    = "Designation Not Exist !";
        public static string GragingSystemNotExist  = "Grading System is not exist !";
        public static string SubjectManagerNotExist = "Subject Manager Not Exist !";
        public static string ExamInformationNotExit = "Exam Information is not Exist !";

        public static string Issue                  = "There are some issue happen !";
        public static string NotFound               = "Items not found !";
        public static string InvalidUser            = "Invalid username or password";
        public static string InvalidField           = "Invalid input filds !";
        public static string IssueInTracer          = "Opps, There are some issue to save tracer";
        public static string InvalidNIDField        = "Invalid NID Field";
        public static string IssueInDatabase        = "Opps, There are some issue in database";
        public static string InvalidImageSize       = "Image Size not more than 200KB";
        public static string InvalidInformation     = "Invalid Information! Please provide the valid information.";
        public static string EmptyPasswordField     = "Password should not empty !";
        public static string InvalidPasswordField   = "Invalid password field";
        public static string InvaldFileExtrension   = "Invalid Extension! Only (.docs, .pdf, .txt) extentions are allowed ";
        public static string InvalidSubjectManager  = "Invalid ! Subject or Class or Batch are not from same institution";
        public static string InvaldImageExtrension  = "Invalid Extension! Only (.jpg, .jpeg, .png) extentions are allowed";
        public static string InvalidStudentAdmission= "Invalid ! Student and Subject Manager are not from same institution";
        //Large-Parts
        public static string InvalidExamManagerInformation = "Invalid Exam Manager. Please provide the valid information.";

        public static string DuplicateGrade             = "This grade already exist !";
        public static string DuplicateResult            = "Student result already exist !";
        public static string DuplicateExamManager       = "Duplicate Exam! Exam manager already exist.";
        public static string DuplicateFeesCollection    = "Student already paid for this month !";

    }
}
