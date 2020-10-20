using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Programs Programs { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Guardian Guardian { get; set; }
        public Login Login { get; set; }
        public EntryInformation EntryInformation { get; set; }
    }
}
