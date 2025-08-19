using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Create
{
    public class FacturaCrear
    {
        public string NumeroFactura { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public List<FacturaDetalleLinea> Detalles { get; set; } = new();
    }

}
