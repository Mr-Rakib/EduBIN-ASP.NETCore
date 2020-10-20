using System;
using System.Collections.Generic;
using System.Text;

namespace EduBIN.Models
{
    public class TokenProvider
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string Token_type { get; set; }
        public int Expire_in { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expired { get; set; }
    }
}
