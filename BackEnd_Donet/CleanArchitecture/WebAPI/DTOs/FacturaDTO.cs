using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class FacturaDTO
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime FechaCreacion { get; set; }

    }
}
