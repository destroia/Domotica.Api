using Models;
using ModelsNotDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intarfaces
{
    public interface IDispositivoData
    {
        Task<Dispositivo> Post(Dispositivo cuenta);
        Task<Dispositivo> Update(Dispositivo cuenta);
        Task<Dispositivo> Get(Dispositivo cuenta);
        Task<List<Dispositivo>> FindAsync(List<MacAddress> macs);
        Task<string> LoginDevice(string mac);
        Task<object> MachDispositivo(Dispositivo dispositivo);
    }
}
