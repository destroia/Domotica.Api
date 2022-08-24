using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Dispositivo
    {
        public Guid Id { get; set; }
        public Guid CuentaId { get; set; }
        public Guid LugarRegionId { get; set; }
        public string MacAddress { get; set; }
        public string Tipo  { get; set; }
        public string Estado { get; set; }
        public DateTime  Fecha { get; set; }
        public DateTime FechaMact { get; set; }
    }
    public class DispositivoQuery : Dispositivo
    {
        public int TotalRecords { get; set; }
    }
}
