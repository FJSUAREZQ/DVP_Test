namespace WebAPI.DTOs.Create
{
    /// <summary>
    /// DTO para el resultado de la creación de una factura.
    /// </summary>
    public class FacturaCrearResultDTO
    {
        /// La factura se creó correctamente
        public bool Exito { get; set; }

        /// Devuelve el número de factura almacenado en la base de datos
        public string? NumeroFactura { get; set; }

        /// Mensaje opcional para proporcionar información adicional sobre el resultado o error
        public string? Mensaje { get; set; }
    }
}
