using Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mqtt.DataMqtt
{
    public class DispositivoMqtt
    {
       
        public static async Task<object> LoginDevice(string mac)
        {
            try
            {
                string sql = "GetMacOnlyMac @Mac, @Id";

                SqlParameter param = new SqlParameter("@Mac", mac);
                SqlParameter param1 = new SqlParameter("@Id", Guid.NewGuid().ToString());
                SqlParameter[] parammes = new SqlParameter[] { param, param1 };
                using (SparkDBContext DB = new SparkDBContext())
                {
                    List<Dispositivo> dispositivo = await DB.DispositivosQuery.FromSqlRaw(sql, parammes)
                        .Select(x => new Dispositivo { Id = x.Id, MacAddress = x.MacAddress }).ToListAsync();

                    if (dispositivo[0] != null)
                    {
                        string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(dispositivo[0]);

                        var es = await MsgMqttNet.Publish(Topics.TopicRaiz + dispositivo[0].MacAddress + Topics.InputDispositivo, "Init");
                    }
                    return dispositivo[0];
                }
            }
            catch (Exception e)
            {

                return e;
            }
                                          
           
           
        }

        public static async Task SubcripcionAllDevice()
        {
            try
            {
                await Task.Run(async () =>
                {
                    using (SparkDBContext DB = new SparkDBContext())
                    {
                      
                        List<string> dispositivos = await DB.DispositivosQuery.FromSqlRaw("ListDispositivosAll")
                        .Select(x => x.MacAddress).ToListAsync();

                        for (int i = 0; i < dispositivos.Count; i++)
                        {
                            dispositivos[i] = Topics.TopicRaiz + dispositivos[i] + Topics.OutPutDispositivo;
                        }
                        
                        await MsgMqttNet.SubscribeList(dispositivos);

                    }
                });
               
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }



        }
    }
}
