using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string NIDNumber { get; set; }
        public Designations Designations { get; set; }
        public Departments Departments { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Login Login { get; set; }
        public EntryInformation EntryInformation { get; set; }
    }
}
