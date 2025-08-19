CREATE PROCEDURE SP_Factura_ListarDetalleByNumFactura
    @NumeroFactura NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        fd.DetalleId,
        fd.FacturaId,
        f.NumeroFactura,
        fd.ProductoId,
        p.Nombre AS NombreProducto,
        fd.Cantidad,
        fd.PrecioUnitario,
        fd.TotalLinea,
        fd.Activo,
        fd.UsuarioCreacion,
        fd.FechaCreacion,
        fd.UsuarioModificacion,
        fd.FechaModificacion
    FROM FacturaDetalle fd
    INNER JOIN Facturas f ON fd.FacturaId = f.FacturaId
    INNER JOIN Productos p ON fd.ProductoId = p.ProductoId
    WHERE f.NumeroFactura = @NumeroFactura
      AND fd.Activo = 1;
END;