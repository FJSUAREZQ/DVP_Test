CREATE PROCEDURE dbo.SP_Productos_ListarForCombos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ProductoId,
        Nombre
    FROM Productos
    WHERE Activo = 1
    ORDER BY Nombre;
END;