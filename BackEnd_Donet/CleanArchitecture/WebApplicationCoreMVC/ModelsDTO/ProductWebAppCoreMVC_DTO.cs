using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreMVC.ModelsDTO
{
    public class ProductWebAppCoreMVC_DTO
    {
        [Display(Name = "Identification")]
        [Required(ErrorMessage = "The field is required")]
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int Stock { get; set; }

        public DateOnly? FechaVencimiento { get; set; }
    }
}
