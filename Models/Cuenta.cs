using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Cuenta
    {
        public Guid Id { get; set; }
        public Guid CuentaId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pais { get; set; }
        public string Celular { get; set; }
        public DateTime Fecha { get; set; }


        [ForeignKey("CuentaId")]
        private ICollection<Dispositivo> Dispositivos { get; set; }
        [ForeignKey("CuentaId")]
        private ICollection<Lugar> Lugares { get; set; }
    }
}
