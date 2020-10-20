using System;
using System.Collections.Generic;
using System.Text;

namespace EduBIN.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public int InstitutionId { get; set; }
    }
}
