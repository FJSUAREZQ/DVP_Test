using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? Departamento { get; set; }
    public bool Activo { get; set; }

    //auditoria
    public string UsuarioCreacion { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

}
