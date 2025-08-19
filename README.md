# LabDev Database

Base de datos para sistema de facturación y gestión de productos desarrollada en SQL Server.

## 📋 Descripción

LabDev es una base de datos diseñada para manejar un sistema de facturación que incluye gestión de clientes, productos, usuarios y facturación. El sistema está estructurado con una arquitectura normalizada que permite el manejo eficiente de datos geográficos, productos, usuarios con roles y facturación detallada.

## 🏗�?Arquitectura de la Base de Datos

### Estructura Principal

- **Gestión Geográfica**: Departamentos �?Ciudades
- **Gestión de Usuarios**: Roles �?Usuarios
- **Gestión Comercial**: Clientes �?Productos �?Facturas �?FacturaDetalle


## 📊 Tablas del Sistema

### 1. **Departamentos**
Gestiona los departamentos del país.

### 2. **Ciudades**
Ciudades asociadas a departamentos.

### 3. **Clientes**
Información de clientes del sistema.

### 4. **Productos**
Catálogo de productos.

### 5. **Roles**
Roles del sistema para usuarios.

### 6. **Usuarios**
Usuarios del sistema con autenticación.

### 7. **Facturas**
Cabecera de facturación.

### 8. **FacturaDetalle**
Detalle de productos por factura.

## 🚀 Instalación y Configuración

### Requisitos Previos

- SQL Server 2016 o superior
- Permisos de administrador en la instancia de SQL Server (si vas a crear el usuario `developer`)
- O permisos suficientes para crear bases de datos con tu usuario actual

### Pasos de Instalación

1. **Clonar el repositorio**

2. **Ejecutar el script principal**
   -- Ejecutar el archivo de creación de DB y tablas
   -- Ejecutar los procedimientos almacenados

### Usuario Developer (Opcional)

El script incluye la creación automática de un usuario de desarrollo:
- **Login**: `developer`
- **Password**: `abc123ABC`
- **Permisos**: `db_owner` en la base de datos LabDev

> ⚠️ **Nota**: Este usuario es opcional y está pensado para entornos de desarrollo. 
> - Si ya tienes un usuario configurado, puedes comentar la sección de creación del usuario
> - En producción, usa tu propio usuario con permisos específicos
> - El script verifica si el usuario ya existe antes de crearlo

## 📝 Datos Iniciales

El script incluye datos de ejemplo.

## 🔧 Características Técnicas

### Auditoría
Todas las tablas incluyen campos de auditoría:
- `UsuarioCreacion` / `FechaCreacion`
- `UsuarioModificacion` / `FechaModificacion`
- `Activo` (soft delete)

### Validaciones
- Precios no negativos
- Cantidades positivas
- Emails únicos
- Nombres únicos donde corresponde

### Índices
- `IX_Facturas_ClienteId`: Optimización para consultas de facturas por cliente

### Tipos Personalizados
- `FacturaDetalleType`: Para procedimientos almacenados con múltiples detalles

## 📦 Procedimientos Almacenados

El sistema incluye un conjunto completo de procedimientos almacenados para operaciones CRUD y de negocio:

### Gestión de Clientes
- `dbo.SP_Clientes_Listar`: Lista todos los clientes activos
- `dbo.SP_Clientes_ListarForCombos`: Lista clientes para controles de selección
- `dbo.SP_Clientes_Crear`: Crea un nuevo cliente con validaciones

### Gestión de Facturas
- `dbo.SP_Factura_BuscarByNumero`: Busca factura por número
- `dbo.SP_Factura_Crear`: Crea factura completa con transacciones
- `dbo.SP_Factura_BuscarConDetalle`: Obtiene factura con su detalle
- `dbo.SP_Factura_BuscarDetalleByNumFactura`: Detalle de factura por número

### Gestión de Productos
- `dbo.SP_Productos_Listar`: Lista todos los productos activos
- `dbo.SP_Productos_ListarForCombos`: Lista productos para controles
- `dbo.SP_Productos_ProductoById`: Obtiene producto específico por ID

### Características de los SPs
- �?**Manejo de Transacciones**: Los SPs críticos incluyen control transaccional
- �?**Manejo de Errores**: Implementan `TRY-CATCH` para captura de excepciones
- �?**Validaciones de Negocio**: Verifican integridad y reglas de negocio
- �?**Optimización**: Utilizan índices y consultas optimizadas
- �?**Parámetros Tipados**: Uso de tipos personalizados para operaciones complejas


## 🗑�?Gestión del Usuario Developer

### Omitir creación del usuario
Si no necesitas el usuario `developer`, puedes omitir la respectiva sección de la cración dentro el script.

## 📈 Próximas Mejoras

- Triggers para actualización automática de stock
- Vistas para reportes frecuentes
- Respaldos automáticos
- Índices adicionales para optimización
- Logging de operaciones críticas

-------------------------------------------------------------------------------------------------------------------------------------------------
# DVP_TEST - Sistema de Facturaci��n API

API REST desarrollada en .NET 8 con Clean Architecture para la gesti��n de facturaci��n (Buscar y crear) del sistema LabDev.

## ?? Descripci��n

DVP_TEST es una API RESTful que implementa un sistema de facturaci��n con arquitectura limpia, permitiendo la gesti��n de facturaci��n de manera eficiente y escalable.

## ??? Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

```
DVP_TEST/
������ DVP_TEST.Domain/          # Entidades de negocio y contratos
������ DVP_TEST.Application/     # L��gica de aplicaci��n y casos de uso
������ DVP_TEST.Infrastructure/  # Acceso a datos y servicios externos
������ DVP_TEST.WebAPI/         # Controladores y configuraci��n API
```


## ?? Caracter��sticas T��cnicas

### Arquitectura y Patrones
- Clean Architecture**: Separaci��n clara de responsabilidades
- Repository Pattern**: Abstracci��n del acceso a datos
- Dependency Injection**: IoC container nativo de .NET
- ADO.NET**: Comunicaci��n directa con SQL Server
- Data Annotations**: Validaci��n autom��tica de DTOs

### Funcionalidades
- Sistema de Facturaci��n**: Creaci��n y b��squeda de facturas
- Manejo de Errores**: Respuestas estructuradas
- CORS**: Configurado para desarrollo

## ?? Estructura del Proyecto

### ?? Domain Layer
```
Domain/
������ Entities/          # Entidades de negocio
������ Interfaces/        # Contratos de repositorios
```

### ?? Application Layer
```
Application/
������ Services/         # Servicios de aplicaci��n
������ Interfaces/       # Contratos de servicios
```

### ??? Infrastructure Layer
```
Infrastructure/
������ Repositories/     # Implementaci��n de repositorios
```

### ?? WebAPI Layer
```
WebAPI/
������ Controllers/     # Controladores REST
������ DTOs/            # DTOs con Datanotation para validaci��n
������ Configuration/   # Configuraci��n de servicios
������ Program.cs       # Punto de entrada
```

## ?? Endpoints Principales

### Clientes
```
GET    /api/clientes/listarCombos       # Clientes para dropdowns
```

### Productos
```
GET    /api/productos/listarCombos      # Productos para dropdowns
GET    /api/productos/buscar/{id}       # Obtener producto por ID
```

### Facturas
```
GET    /api/facturas/buscarNumFactura/{numero}  # Buscar por n��mero
GET    /api/facturas/buscarClienteId/{id}       # Buscar por cliente
POST   /api/facturas/crear                      # Crear factura
```

## ?? Configuraci��n

### Requisitos Previos
- .NET 8 SDK
- SQL Server (conexi��n a base de datos LabDev)
- Visual Studio 2022 o VS Code
 
### Instalaci��n

1. **Clonar el repositorio**
   ```bash
   git clone <url-del-repositorio>
   cd DVP_TEST
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Configurar cadena de conexi��n**
   
   Editar `appsettings.json` en el proyecto WebAPI:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=<tu_servidor>;Database=LabDev;User Id=<tu_usuario>;Password=<tu_password>;"
     }
   }
   ```

## ??? Validaciones
Los DTOs incluyen validaciones autom��ticas usando Data Annotations:


## ?? Pr��ximas Mejoras

- Implementar autenticaci��n JWT
- Implementar paginaci��n
- Pruebas unitarias e integraci��n

## ?? Tecnolog��as Utilizadas

- **.NET 8**: Framework principal
- **ADO.NET**: Acceso a datos
- **SQL Server**: Base de datos
- **Clean Architecture**: Patr��n arquitect��nico
- **Repository Pattern**: Patr��n de acceso a datos
- **Data Annotations**: Validaciones

-----------------------------------------------------------------------------------------------------------------------------------------
