using Data.Intarfaces;
using Data.Services.Encrypt;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using ModelsNotDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepoData
{
    public class CuentaData : ICuentaData
    {
        readonly SparkDBContext DB;
        public CuentaData(SparkDBContext db)
        {
            DB = db;
        }
        public async Task<Cuenta> Get(Guid id)
        {
            return await DB.Cuentas.FindAsync(id);
        }

        public async Task<ResCuenta> Post(Cuenta cuenta)
        {
            cuenta.Email = cuenta.Email.ToLower();

            var dt = DateTime.Now;
            string sql = "CreateCuenta @Id, @Email, @Cel, @Pais, @Password, @Date";
            SqlParameter param = new SqlParameter("@Id", Guid.NewGuid().ToString());
            SqlParameter param2 = new SqlParameter("@Email", cuenta.Email);
            SqlParameter param3 = new SqlParameter("@Cel", cuenta.Celular);
            SqlParameter param4 = new SqlParameter("@Pais", cuenta.Pais);
            SqlParameter param5 = new SqlParameter("@Password", "");
            SqlParameter param6 = new SqlParameter("@Date", dt.ToString());

            SqlParameter[] parames = new SqlParameter[] { param, param2, param3, param4, param5 ,param6};

            var res = await DB.Cuentas.FromSqlRaw(sql, parames).ToListAsync();

            if (res[0] != null)
            {
                var resCuenta = new ResCuenta()
                {
                    Id = res[0].Id,
                    Celular = res[0].Celular,
                    Email = res[0].Email,
                    Fecha = res[0].Fecha,
                    Pais = res[0].Pais,
                    Password = res[0].Password
                };
           
                if (dt.ToString() == resCuenta.Fecha.ToString())
                {
                    resCuenta.res = "Create Account Success";
                    resCuenta.StatusCode = 200;

                }
                else
                {
                    resCuenta.res = "Exist Account ";
                    resCuenta.StatusCode = 204;
                    //Todo Send email
                }
                return resCuenta;
            }

            return null;
        }

        public async Task<Cuenta> Update(Cuenta cuenta)
        {
            var res = await DB.Cuentas.FindAsync(cuenta.Id);

             res.Email = cuenta.Email.ToLower();
            res.Celular = cuenta.Celular;
            res.Pais = cuenta.Pais;
            

             DB.Cuentas.Update(res);

            await DB.SaveChangesAsync();

            return cuenta;
        }
    }
}
