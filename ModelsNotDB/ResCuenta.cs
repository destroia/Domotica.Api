using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNotDB
{
    public class ResCuenta : Cuenta
    {
        public string res { get; set; }
        public int StatusCode { get; set; }
    }
}
