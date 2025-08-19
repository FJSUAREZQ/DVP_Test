using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Create
{
    public class FacturaDetalleLineaDTO
    {
        [Required(ErrorMessage = "El ProductoId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ProductoId debe ser mayor que 0.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que 0.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El total de la línea es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total de la línea debe ser mayor que 0.")]
        public decimal TotalLinea { get; set; }
    }
}
