using Models;
using ModelsNotDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intarfaces
{
    public interface ICuentaData
    {
        Task<ResCuenta> Post(Cuenta cuenta);
        Task<Cuenta> Update(Cuenta cuenta);
        Task<Cuenta> Get(Guid id);
       
    }
}
