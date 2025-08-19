CREATE PROCEDURE dbo.SP_Productos_ProductoByID
    @ProductoId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación básica
    IF @ProductoId IS NULL OR @ProductoId <= 0
    BEGIN
        RAISERROR('El ID del producto debe ser un número positivo.', 16, 1);
        RETURN;
    END

    SELECT 
        ProductoId,
        Nombre,
        PrecioUnitario,
        ImagenUrl,
        Activo,
        Stock,
        UsuarioCreacion,
        FechaCreacion,
        UsuarioModificacion,
        FechaModificacion
    FROM Productos
    WHERE ProductoId = @ProductoId;
END;