using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Producto
{

    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Activo { get; set; } = true;
    public decimal Stock { get; set; }

    //auditoria
    public string UsuarioCreacion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

}
