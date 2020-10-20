using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Models
{
    public class Programs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public Departments Departments { get; set; }
        public EntryInformation EntryInformation { get; set; }
    }
}
