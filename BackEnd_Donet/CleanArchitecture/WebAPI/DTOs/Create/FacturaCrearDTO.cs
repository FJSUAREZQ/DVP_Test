
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Create
{
    public class FacturaCrearDTO
    {
        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de factura no puede tener más de 20 caracteres.")]
        public string NumeroFactura { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ClienteId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser mayor que 0.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El UsuarioId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El UsuarioId debe ser mayor que 0.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = "Los impuestos son obligatorios.")]
        [Range(0, double.MaxValue, ErrorMessage = "Los impuestos no pueden ser negativos.")]
        public decimal Impuestos { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Debe especificar al menos una línea de detalle.")]
        [MinLength(1, ErrorMessage = "Debe existir al menos un detalle en la factura.")]
        public List<FacturaDetalleLineaDTO> Detalles { get; set; } = new();
    }
}
