CREATE OR ALTER PROCEDURE dbo.SP_Factura_BuscarByNumero
    @NumeroFactura NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación mínima
    IF @NumeroFactura IS NULL OR LTRIM(RTRIM(@NumeroFactura)) = ''
    BEGIN
        RAISERROR('El número de factura es obligatorio.', 16, 1);
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
    INNER JOIN dbo.Clientes AS c 
        ON f.ClienteId = c.ClienteId
    INNER JOIN dbo.Ciudades AS ci 
        ON c.CiudadId = ci.CiudadId
    INNER JOIN dbo.Departamentos AS d 
        ON ci.DepartamentoId = d.DepartamentoId
    LEFT JOIN dbo.Usuarios AS u 
        ON f.UsuarioId = u.UsuarioId
    LEFT JOIN dbo.Roles AS r 
        ON u.RolId = r.RolId
    WHERE 
        f.NumeroFactura = @NumeroFactura
        AND f.Activo = 1
        AND c.Activo = 1
    ORDER BY 
        f.FechaCreacion DESC;
END