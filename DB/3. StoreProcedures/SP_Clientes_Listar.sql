CREATE OR ALTER PROCEDURE dbo.SP_Clientes_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.ClienteId,
        c.Nombre,
        c.Email,
        c.Telefono,
        c.Direccion,
        ci.Nombre AS Ciudad,
        d.Nombre AS Departamento,
        c.Activo,
        c.UsuarioCreacion,
        c.FechaCreacion,
        c.UsuarioModificacion,
        c.FechaModificacion
    FROM Clientes AS c
    INNER JOIN Ciudades AS ci 
        ON c.CiudadId = ci.CiudadId
    INNER JOIN Departamentos AS d 
        ON ci.DepartamentoId = d.DepartamentoId
    WHERE c.Activo = 1
    ORDER BY c.Nombre;
END