----------------------------------------------------
-- 1. Departamentos
----------------------------------------------------
CREATE TABLE Departamentos (
    DepartamentoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE, -- que no se repitan nombres
    Descripcion NVARCHAR(255) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL
);


----------------------------------------------------
-- 2. Ciudades
----------------------------------------------------
CREATE TABLE Ciudades (
    CiudadId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    DepartamentoId INT NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,

    CONSTRAINT FK_Ciudades_Departamentos FOREIGN KEY (DepartamentoId) REFERENCES Departamentos(DepartamentoId),
    CONSTRAINT UQ_Ciudades UNIQUE (Nombre, DepartamentoId) -- No se pueden repetir ciudades en el mismo departamento.
);

----------------------------------------------------
-- 3. Clientes
----------------------------------------------------
CREATE TABLE Clientes (
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL UNIQUE,
    Telefono NVARCHAR(20) NULL,
    Direccion NVARCHAR(200) NULL,
    CiudadId INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,
    CONSTRAINT FK_Clientes_Ciudades FOREIGN KEY (CiudadId) REFERENCES Ciudades(CiudadId)
);

----------------------------------------------------
-- 4. Productos
----------------------------------------------------
CREATE TABLE Productos (
    ProductoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL CHECK (PrecioUnitario >= 0),
    ImagenUrl NVARCHAR(200) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    Stock DECIMAL(18,2) NOT NULL,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL
);

----------------------------------------------------
-- 5. Roles
----------------------------------------------------
CREATE TABLE Roles (
    RolId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL
);

----------------------------------------------------
-- 6. Usuarios
----------------------------------------------------
CREATE TABLE Usuarios (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    RolId INT NOT NULL,
    ClienteId INT NULL, -- Solo si el usuario es también cliente
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (RolId) REFERENCES Roles(RolId),
    CONSTRAINT FK_Usuarios_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

----------------------------------------------------
-- 7. Facturas
----------------------------------------------------
CREATE TABLE Facturas (
    FacturaId INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura NVARCHAR(20) NOT NULL UNIQUE,
    ClienteId INT NOT NULL,
    UsuarioId INT NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL CHECK (Subtotal >= 0),
    Impuestos DECIMAL(18,2) NOT NULL CHECK (Impuestos >= 0),
    Total DECIMAL(18,2) NOT NULL CHECK (Total >= 0),
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,
    CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
    CONSTRAINT FK_Facturas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);

----------------------------------------------------
-- 8. FacturaDetalle
----------------------------------------------------
CREATE TABLE FacturaDetalle (
    DetalleId INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    TotalLinea DECIMAL(18,2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    UsuarioCreacion NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioModificacion NVARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,
    CONSTRAINT FK_FacturaDetalle_Facturas FOREIGN KEY (FacturaId) REFERENCES Facturas(FacturaId),
    CONSTRAINT FK_FacturaDetalle_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(ProductoId)
);

----------------------------------------------------
-- 9. Type para pasar detalles (ADO.NET)
----------------------------------------------------
CREATE TYPE FacturaDetalleType AS TABLE
(
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    TotalLinea DECIMAL(18,2) NOT NULL
);

----------------------------------------------------
-- 10. Índices
----------------------------------------------------
-- Para búsqueda rápida de facturas por cliente
CREATE INDEX IX_Facturas_ClienteId ON Facturas(ClienteId);


***********************************************************************************************************
-----------------------------------------------------
--Inserts iniciales
----------------------------------------------------
-- 1. Departamentos
----------------------------------------------------
INSERT INTO Departamentos (Nombre, Descripcion, Activo, UsuarioCreacion)
VALUES 
('Risaralda', 'Departamento en la región cafetera', 1, 'admin'),
('Antioquia', 'Departamento con capital en Medellín', 1, 'admin'),
('Cundinamarca', 'Incluye la capital del país', 1, 'admin');

----------------------------------------------------
-- 2. Ciudades
----------------------------------------------------
INSERT INTO Ciudades (Nombre, DepartamentoId, Descripcion, Activo, UsuarioCreacion)
VALUES
('Pereira', 1, 'Capital de Risaralda', 1, 'admin'),
('Medellín', 2, 'Capital de Antioquia', 1, 'admin'),
('Bogotá', 3, 'Capital de Cundinamarca y de Colombia', 1, 'admin');

----------------------------------------------------
-- 3. Clientes
----------------------------------------------------
INSERT INTO Clientes (Nombre, Email, Telefono, Direccion, CiudadId, Activo, UsuarioCreacion)
VALUES
('Juan Pérez', 'juan.perez@email.com', '3001234567', 'Calle 10 #5-20', 1, 1, 'admin'),
('María Gómez', 'maria.gomez@email.com', '3109876543', 'Carrera 15 #8-50', 2, 1, 'admin');

----------------------------------------------------
-- 4. Productos
----------------------------------------------------
INSERT INTO dbo.Productos (Nombre, PrecioUnitario, ImagenUrl, Stock, Activo, UsuarioCreacion)
VALUES
('Bolso Azul', 45000, 'https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_t.png', 50, 1, 'admin'),
('Polo Blanco/Negro', 35000, 'https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_t.png', 30, 1, 'admin'),
('Chamarra Marron', 180000, 'https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_t.png', 40, 1, 'admin'),
('Polo Manga Larga', 40000, 'https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_t.png', 15, 1, 'admin'),
('Manilla plata', 20000, 'https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_t.png', 10, 1, 'admin');
----------------------------------------------------
-- 5. Roles
----------------------------------------------------
INSERT INTO Roles (Nombre, Activo, UsuarioCreacion)
VALUES
('Administrador', 1, 'admin'),
('Vendedor', 1, 'admin');

----------------------------------------------------
-- 6. Usuarios
----------------------------------------------------
-- Suponiendo que la contraseña ya viene hasheada (ejemplo simulado con HASH ficticio)
INSERT INTO Usuarios (Username, PasswordHash, Nombre, RolId, ClienteId, Activo, UsuarioCreacion)
VALUES
('admin', 'HASH_ADMIN', 'Admin del Sistema', 1, NULL, 1, 'admin'),
('vendedor1', 'HASH_VEN1', 'Carlos López', 2, 1, 1, 'admin');

----------------------------------------------------
-- 7. Facturas
----------------------------------------------------
INSERT INTO Facturas (NumeroFactura, ClienteId, UsuarioId, Subtotal, Impuestos, Total, Activo, UsuarioCreacion)
VALUES
('FAC-0001', 1, 2, 50000, 9500, 59500, 1, 'admin');

----------------------------------------------------
-- 8. FacturaDetalle
----------------------------------------------------
INSERT INTO FacturaDetalle (FacturaId, ProductoId, Cantidad, PrecioUnitario, TotalLinea, Activo, UsuarioCreacion)
VALUES
(1, 1, 2, 25000, 50000, 1, 'admin');