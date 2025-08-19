CREATE OR ALTER PROCEDURE dbo.SP_Clientes_ListarForCombos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ClienteId,
        Nombre
    FROM dbo.Clientes
    WHERE Activo = 1
    ORDER BY Nombre;
END