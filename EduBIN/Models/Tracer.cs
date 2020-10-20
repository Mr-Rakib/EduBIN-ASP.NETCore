using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduBIN.Models
{
    public class Tracer
    {
        public int Id { get; set; }
        public int Actor_id { get; set; }
        public string ActionName { get; set; }
        public string TableName { get; set; }
        public int ActionAppliedId { get; set; }
        public DateTime ActionTime { get; set; }
        public int InstitutionId { get; set; }

    }
}
