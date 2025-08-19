namespace WebAPI.DTOs
{
    public class FacturaDetalleDTO
    {
        public int DetalleId { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalLinea { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
