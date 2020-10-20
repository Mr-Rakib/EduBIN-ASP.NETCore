using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Models
{
    public class Designations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstitutionId { get; set; }
        public EntryInformation EntryInformation { get; set; }
    }
}
