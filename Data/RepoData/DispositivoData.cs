using Data.Intarfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using ModelsNotDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepoData
{
    public class DispositivoData : IDispositivoData
    {
        readonly SparkDBContext DB;
        public DispositivoData(SparkDBContext db)
        {
            DB = db;
        }

        public async Task<List<Dispositivo>> FindAsync(List<MacAddress> macs)
        {
            
            List<Dispositivo> Listdevices = new List<Dispositivo>();

            foreach (var item in macs)
            {
                string sql = "GetDispositivoByMac @Mac";
                SqlParameter param = new SqlParameter("@Mac", item.Mac);
                List<Dispositivo> dispositivos = await DB.Dispositivos.FromSqlRaw(sql, param).ToListAsync();

                if (dispositivos.Count != 0)
                {
                    Listdevices.Add(dispositivos[0]);
                }
            }
            return Listdevices;
        }

        public Task<Dispositivo> Get(Dispositivo cuenta)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginDevice(string mac)
        {
            string sql = "GetMacOnlyMac @Mac";
            SqlParameter param = new SqlParameter("@Mac", mac);
           
            List<string> macAddress = await DB.DispositivosQuery.FromSqlRaw(sql, param).Select(x => x.MacAddress).ToListAsync();//.Clientes.FromSqlRaw(sql,param2).ToListAsync();//.sqlQuery().ExecuteSqlRawAsync(sql);
            return macAddress[0];
        }

        public async Task<object> MachDispositivo(Dispositivo dispositivo)
        {
            string sql = "MachDispocitivo @Id, @Mac, @Cuenta, @Region, @Type";
            SqlParameter param = new SqlParameter("@Id", dispositivo.Id.ToString());
            SqlParameter param2 = new SqlParameter("@Mac", dispositivo.MacAddress);
            SqlParameter param3 = new SqlParameter("@Cuenta", dispositivo.CuentaId.ToString());
            SqlParameter param4 = new SqlParameter("@Region", dispositivo.LugarRegionId.ToString());
            SqlParameter param5 = new SqlParameter("@Type", dispositivo.Tipo);

            SqlParameter[] parames = new SqlParameter[] { param , param2, param3, param4, param5};

            var res = await DB.DispositivosQuery.FromSqlRaw(sql, parames).ToListAsync();
            return res[0];
        }

        public Task<Dispositivo> Post(Dispositivo cuenta)
        {
            throw new NotImplementedException();
        }

        public Task<Dispositivo> Update(Dispositivo cuenta)
        {
            throw new NotImplementedException();
        }
    }
}
