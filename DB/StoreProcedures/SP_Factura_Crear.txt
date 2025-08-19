CREATE PROCEDURE dbo.SP_Factura_Crear
    @NumeroFactura NVARCHAR(20),
    @ClienteId INT,
    @UsuarioId INT,
    @Subtotal DECIMAL(18,2),
    @Impuestos DECIMAL(18,2),
    @Total DECIMAL(18,2),
    @Detalles FacturaDetalleType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

 	-- Verificar si el numero de factura ya existe
        IF EXISTS (SELECT 1 FROM dbo.Facturas WHERE NumeroFactura = @NumeroFactura)
            THROW 50000, 'El número de factura ya existe.', 1;

    	-- Verificar si existe stock
        IF EXISTS (
            SELECT 1
            FROM @Detalles d
            JOIN Productos p ON d.ProductoId = p.ProductoId
            WHERE d.Cantidad > p.Stock
        )
            THROW 50004, 'Stock insuficiente para uno o más productos.', 1;

        -- Insertar el encabezado de factura
        INSERT INTO Facturas (
            NumeroFactura,
            ClienteId,
            UsuarioId,
            Subtotal,
            Impuestos,
            Total,
            UsuarioCreacion,
            FechaCreacion
        )
        VALUES (
            @NumeroFactura,
            @ClienteId,
            @UsuarioId,
            @Subtotal,
            @Impuestos,
            @Total,
            CAST(@UsuarioId AS NVARCHAR(100)),
            GETDATE()
        );
	
	--Devuelve el Id generado previamente
        DECLARE @FacturaId INT = SCOPE_IDENTITY();

        -- Insertar detalle de la factura
        INSERT INTO FacturaDetalle (
            FacturaId,
            ProductoId,
            Cantidad,
            PrecioUnitario,
            TotalLinea,
            UsuarioCreacion,
            FechaCreacion
        )
        SELECT
            @FacturaId,
            ProductoId,
            Cantidad,
            PrecioUnitario,
            TotalLinea,
            CAST(@UsuarioId AS NVARCHAR(100)),
            GETDATE()
        FROM @Detalles;

        -- Actualizar stock de productos
        UPDATE p
        SET p.Stock = p.Stock - d.Cantidad
        FROM Productos p
        JOIN @Detalles d ON p.ProductoId = d.ProductoId;

        COMMIT;

	--Retorna el numero de la factura del proceso
        SELECT NumeroFactura = @NumeroFactura;
    END TRY
    BEGIN CATCH
	--Valida si existen transacciones activas para ejecutar el RollBack
        IF @@TRANCOUNT > 0
            ROLLBACK;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END