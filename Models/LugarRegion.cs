using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class LugarRegion
    {
        public Guid Id { get; set; }
        public Guid LugarId { get; set; }
        public string Nombre {get; set;}

        [ForeignKey("LugarRegionId")]
        public ICollection<Dispositivo> Dispositivos { get; set; }
    }
}
