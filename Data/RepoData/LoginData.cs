using Data.Intarfaces;
using Data.Services.Encrypt;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepoData
{
    public class LoginData : ILoginData
    {
        readonly SparkDBContext DB;
        public LoginData(SparkDBContext db)
        {
            DB = db;
        }

        public Task<bool> Forgot(Forgot forgot)
        {
            throw new NotImplementedException();
        }

        public async Task<Cuenta> Login(Login login)
        {
            login.Email = login.Email.ToLower();
            //login.Password = Encrypt.GetSHA256(login.Password);

            string sql = "SP_Login @Email, @Password";
            SqlParameter param = new SqlParameter("@Email", login.Email);
            SqlParameter param2 = new SqlParameter("@Password", login.Password);

            SqlParameter[] parames = new SqlParameter[] { param, param2 };
            var res = await DB.Cuentas.FromSqlRaw(sql, parames).ToListAsync();

            if (res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
    }
}
