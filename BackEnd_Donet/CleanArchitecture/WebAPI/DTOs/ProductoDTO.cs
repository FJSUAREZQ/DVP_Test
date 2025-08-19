namespace WebAPI.DTOs
{
    public class ProductoDTO
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public string? ImagenUrl { get; set; }
        public decimal Stock { get; set; }

    }
}
