using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Create
{
    public class FacturaCrearResult
    {
        public bool Exito { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Mensaje { get; set; }
    }

}
