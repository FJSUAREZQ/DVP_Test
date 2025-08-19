CREATE OR ALTER PROCEDURE dbo.SP_Productos_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.ProductoId,
        p.Nombre,
        p.PrecioUnitario,
        p.ImagenUrl,
        p.Stock, -- Nuevo campo
        p.Activo,
        p.UsuarioCreacion,
        p.FechaCreacion,
        p.UsuarioModificacion,
        p.FechaModificacion
    FROM dbo.Productos AS p
    WHERE p.Activo = 1
    ORDER BY p.Nombre;
END