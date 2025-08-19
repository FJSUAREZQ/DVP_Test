CREATE OR ALTER PROCEDURE dbo.SP_Factura_BuscarByClienteId
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación mínima
    IF @ClienteId IS NULL
    BEGIN
        RAISERROR('ClienteId es obligatorio.', 16, 1);
        RETURN;
    END

    SELECT 
        f.FacturaId,
        f.NumeroFactura,
		c.ClienteId,
		c.Email,
        c.Nombre AS Cliente,
		c.Direccion,
        ci.Nombre AS Ciudad,
        d.Nombre AS Departamento,
        f.Subtotal,
        f.Impuestos,
        f.Total,
        f.Activo,
        f.UsuarioCreacion,
        f.FechaCreacion,
        f.UsuarioModificacion,
        f.FechaModificacion
    FROM dbo.Facturas AS f
    INNER JOIN dbo.Clientes AS c   ON f.ClienteId = c.ClienteId
    INNER JOIN dbo.Ciudades AS ci  ON c.CiudadId = ci.CiudadId
    INNER JOIN dbo.Departamentos AS d ON ci.DepartamentoId = d.DepartamentoId
    LEFT JOIN dbo.Usuarios AS u   ON f.UsuarioId = u.UsuarioId     -- LEFT JOIN para no perder facturas si el usuario fue desactivado
    LEFT JOIN dbo.Roles AS r    ON u.RolId = r.RolId
    WHERE 
        f.ClienteId = @ClienteId
        AND f.Activo = 1                 -- solo facturas vigentes
        AND c.Activo = 1                 -- solo clientes vigentes
    ORDER BY 
        f.FechaCreacion DESC,
        f.NumeroFactura DESC;
END