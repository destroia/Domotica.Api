using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class Forgot
    {
        public string Email { get; set; }
    }
}
