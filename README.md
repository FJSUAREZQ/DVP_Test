# LabDev Database

Base de datos para sistema de facturaci贸n y gesti贸n de productos desarrollada en SQL Server.

## 馃搵 Descripci贸n

LabDev es una base de datos dise帽ada para manejar un sistema de facturaci贸n que incluye gesti贸n de clientes, productos, usuarios y facturaci贸n. El sistema est谩 estructurado con una arquitectura normalizada que permite el manejo eficiente de datos geogr谩ficos, productos, usuarios con roles y facturaci贸n detallada.

## 馃彈锔?Arquitectura de la Base de Datos

### Estructura Principal

- **Gesti贸n Geogr谩fica**: Departamentos 鈫?Ciudades
- **Gesti贸n de Usuarios**: Roles 鈫?Usuarios
- **Gesti贸n Comercial**: Clientes 鈫?Productos 鈫?Facturas 鈫?FacturaDetalle


## 馃搳 Tablas del Sistema

### 1. **Departamentos**
Gestiona los departamentos del pa铆s.

### 2. **Ciudades**
Ciudades asociadas a departamentos.

### 3. **Clientes**
Informaci贸n de clientes del sistema.

### 4. **Productos**
Cat谩logo de productos.

### 5. **Roles**
Roles del sistema para usuarios.

### 6. **Usuarios**
Usuarios del sistema con autenticaci贸n.

### 7. **Facturas**
Cabecera de facturaci贸n.

### 8. **FacturaDetalle**
Detalle de productos por factura.

## 馃殌 Instalaci贸n y Configuraci贸n

### Requisitos Previos

- SQL Server 2016 o superior
- Permisos de administrador en la instancia de SQL Server (si vas a crear el usuario `developer`)
- O permisos suficientes para crear bases de datos con tu usuario actual

### Pasos de Instalaci贸n

1. **Clonar el repositorio**

2. **Ejecutar el script principal**
   -- Ejecutar el archivo de creaci贸n de DB y tablas
   -- Ejecutar los procedimientos almacenados

### Usuario Developer (Opcional)

El script incluye la creaci贸n autom谩tica de un usuario de desarrollo:
- **Login**: `developer`
- **Password**: `abc123ABC`
- **Permisos**: `db_owner` en la base de datos LabDev

> 鈿狅笍 **Nota**: Este usuario es opcional y est谩 pensado para entornos de desarrollo. 
> - Si ya tienes un usuario configurado, puedes comentar la secci贸n de creaci贸n del usuario
> - En producci贸n, usa tu propio usuario con permisos espec铆ficos
> - El script verifica si el usuario ya existe antes de crearlo

## 馃摑 Datos Iniciales

El script incluye datos de ejemplo.

## 馃敡 Caracter铆sticas T茅cnicas

### Auditor铆a
Todas las tablas incluyen campos de auditor铆a:
- `UsuarioCreacion` / `FechaCreacion`
- `UsuarioModificacion` / `FechaModificacion`
- `Activo` (soft delete)

### Validaciones
- Precios no negativos
- Cantidades positivas
- Emails 煤nicos
- Nombres 煤nicos donde corresponde

### 脥ndices
- `IX_Facturas_ClienteId`: Optimizaci贸n para consultas de facturas por cliente

### Tipos Personalizados
- `FacturaDetalleType`: Para procedimientos almacenados con m煤ltiples detalles

## 馃摝 Procedimientos Almacenados

El sistema incluye un conjunto completo de procedimientos almacenados para operaciones CRUD y de negocio:

### Gesti贸n de Clientes
- `dbo.SP_Clientes_Listar`: Lista todos los clientes activos
- `dbo.SP_Clientes_ListarForCombos`: Lista clientes para controles de selecci贸n
- `dbo.SP_Clientes_Crear`: Crea un nuevo cliente con validaciones

### Gesti贸n de Facturas
- `dbo.SP_Factura_BuscarByNumero`: Busca factura por n煤mero
- `dbo.SP_Factura_Crear`: Crea factura completa con transacciones
- `dbo.SP_Factura_BuscarConDetalle`: Obtiene factura con su detalle
- `dbo.SP_Factura_BuscarDetalleByNumFactura`: Detalle de factura por n煤mero

### Gesti贸n de Productos
- `dbo.SP_Productos_Listar`: Lista todos los productos activos
- `dbo.SP_Productos_ListarForCombos`: Lista productos para controles
- `dbo.SP_Productos_ProductoById`: Obtiene producto espec铆fico por ID

### Caracter铆sticas de los SPs
- 鉁?**Manejo de Transacciones**: Los SPs cr铆ticos incluyen control transaccional
- 鉁?**Manejo de Errores**: Implementan `TRY-CATCH` para captura de excepciones
- 鉁?**Validaciones de Negocio**: Verifican integridad y reglas de negocio
- 鉁?**Optimizaci贸n**: Utilizan 铆ndices y consultas optimizadas
- 鉁?**Par谩metros Tipados**: Uso de tipos personalizados para operaciones complejas


## 馃棏锔?Gesti贸n del Usuario Developer

### Omitir creaci贸n del usuario
Si no necesitas el usuario `developer`, puedes omitir la respectiva secci贸n de la craci贸n dentro el script.

## 馃搱 Pr贸ximas Mejoras

- Triggers para actualizaci贸n autom谩tica de stock
- Vistas para reportes frecuentes
- Respaldos autom谩ticos
- 脥ndices adicionales para optimizaci贸n
- Logging de operaciones cr铆ticas

-------------------------------------------------------------------------------------------------------------------------------------------------
# DVP_TEST - Sistema de Facturación API

API REST desarrollada en .NET 8 con Clean Architecture para la gestión de facturación (Buscar y crear) del sistema LabDev.

## ?? Descripción

DVP_TEST es una API RESTful que implementa un sistema de facturación con arquitectura limpia, permitiendo la gestión de facturación de manera eficiente y escalable.

## ??? Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

```
DVP_TEST/
├── DVP_TEST.Domain/          # Entidades de negocio y contratos
├── DVP_TEST.Application/     # Lógica de aplicación y casos de uso
├── DVP_TEST.Infrastructure/  # Acceso a datos y servicios externos
└── DVP_TEST.WebAPI/         # Controladores y configuración API
```


## ?? Características Técnicas

### Arquitectura y Patrones
- Clean Architecture**: Separación clara de responsabilidades
- Repository Pattern**: Abstracción del acceso a datos
- Dependency Injection**: IoC container nativo de .NET
- ADO.NET**: Comunicación directa con SQL Server
- Data Annotations**: Validación automática de DTOs

### Funcionalidades
- Sistema de Facturación**: Creación y búsqueda de facturas
- Manejo de Errores**: Respuestas estructuradas
- CORS**: Configurado para desarrollo

## ?? Estructura del Proyecto

### ?? Domain Layer
```
Domain/
├── Entities/          # Entidades de negocio
├── Interfaces/        # Contratos de repositorios
```

### ?? Application Layer
```
Application/
├── Services/         # Servicios de aplicación
└── Interfaces/       # Contratos de servicios
```

### ??? Infrastructure Layer
```
Infrastructure/
├── Repositories/     # Implementación de repositorios
```

### ?? WebAPI Layer
```
WebAPI/
├── Controllers/     # Controladores REST
├── DTOs/            # DTOs con Datanotation para validación
├── Configuration/   # Configuración de servicios
└── Program.cs       # Punto de entrada
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
GET    /api/facturas/buscarNumFactura/{numero}  # Buscar por número
GET    /api/facturas/buscarClienteId/{id}       # Buscar por cliente
POST   /api/facturas/crear                      # Crear factura
```

## ?? Configuración

### Requisitos Previos
- .NET 8 SDK
- SQL Server (conexión a base de datos LabDev)
- Visual Studio 2022 o VS Code
 
### Instalación

1. **Clonar el repositorio**
   ```bash
   git clone <url-del-repositorio>
   cd DVP_TEST
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Configurar cadena de conexión**
   
   Editar `appsettings.json` en el proyecto WebAPI:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=<tu_servidor>;Database=LabDev;User Id=<tu_usuario>;Password=<tu_password>;"
     }
   }
   ```

## ??? Validaciones
Los DTOs incluyen validaciones automáticas usando Data Annotations:


## ?? Próximas Mejoras

- Implementar autenticación JWT
- Implementar paginación
- Pruebas unitarias e integración

## ?? Tecnologías Utilizadas

- **.NET 8**: Framework principal
- **ADO.NET**: Acceso a datos
- **SQL Server**: Base de datos
- **Clean Architecture**: Patrón arquitectónico
- **Repository Pattern**: Patrón de acceso a datos
- **Data Annotations**: Validaciones

-----------------------------------------------------------------------------------------------------------------------------------------
