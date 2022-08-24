using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intarfaces
{
    public interface ILoginData
    {
        Task<Cuenta> Login(Login login);
        Task<bool> Forgot(Forgot forgot);
    }
}
