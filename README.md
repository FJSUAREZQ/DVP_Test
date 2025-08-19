# LabDev Database

Base de datos para sistema de facturación y gestión de productos desarrollada en SQL Server.

## Descripción

LabDev es una base de datos diseñada para manejar un sistema de facturación que incluye gestión de clientes, productos, usuarios y facturación. El sistema está estructurado con una arquitectura normalizada que permite el manejo eficiente de datos geogrpificos, productos, usuarios con roles y facturación detallada.

## Arquitectura de la Base de Datos

### Estructura Principal

- **Gestión Geogrífica**: Departamentos y Ciudades
- **Gestión de Usuarios**: Roles y Usuarios
- **Gestión Comercial**: Clientes, Productos, Facturas


## Tablas del Sistema

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

## Instalación y Configuración

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

El script incluye la creación automítica de un usuario de desarrollo:
- **Login**: `developer`
- **Password**: `abc123ABC`
- **Permisos**: `db_owner` en la base de datos LabDev

> **Nota**: Este usuario es opcional y está pensado para entornos de desarrollo. 
> - Si ya tienes un usuario configurado, puedes comentar la sección de creación del usuario
> - En producción, usa tu propio usuario con permisos específicos
> - El script verifica si el usuario ya existe antes de crearlo

## Datos Iniciales

El script incluye datos de ejemplo.

## Caracterásticas Técnicas

### Auditoría
Todas las tablas incluyen campos de auditoría:
- `UsuarioCreacion` / `FechaCreacion`
- `UsuarioModificacion` / `FechaModificacion`
- `Activo` (soft delete)

### Validaciones
- Precios no negativos
- Cantidades positivas
- Emails
- Nombres
- Otros

### Indices
- `IX_Facturas_ClienteId`: Optimización para consultas de facturas por cliente

### Tipos Personalizados
- `FacturaDetalleType`: Para procedimientos almacenados con múltiples detalles

## Procedimientos Almacenados

El sistema incluye un conjunto completo de procedimientos almacenados para operaciones de negocio:

### Gestión de Clientes
- `dbo.SP_Clientes_Listar`: Lista todos los clientes activos
- `dbo.SP_Clientes_ListarForCombos`: Lista clientes para controles de selección
- `dbo.SP_Clientes_Crear`: Crea un nuevo cliente con validaciones

### Gestión de Facturas
- `dbo.SP_Factura_BuscarByNumero`: Busca factura por número
- `dbo.SP_Factura_Crear`: Crea factura completa con transacciones
- `dbo.SP_Factura_BuscarConDetalle`: Obtiene factura con su detalle
- `dbo.SP_Factura_BuscarDetalleByNumFactura`: Detalle de factura por número

### Gesti籀n de Productos
- `dbo.SP_Productos_Listar`: Lista todos los productos activos
- `dbo.SP_Productos_ListarForCombos`: Lista productos para controles
- `dbo.SP_Productos_ProductoById`: Obtiene producto espec赤fico por ID

### Características de los SPs
- **Manejo de Transacciones**: Los SPs críticos incluyen control transaccional
- **Manejo de Errores**: Implementan `TRY-CATCH` para captura de excepciones
- **Validaciones de Negocio**: Verifican integridad y reglas de negocio
- **Optimización**: Utilizan índices y consultas optimizadas
- **Parámetros Tipados**: Uso de tipos personalizados para operaciones complejas


## Gestión del Usuario Developer

### Omitir creación del usuario
Si no necesitas el usuario `developer`, puedes omitir la respectiva sección de la cración dentro el script.

## Próximas Mejoras

- Triggers para actualización automática de stock
- Vistas para reportes frecuentes
- Respaldos autom芍ticos
- índices adicionales para optimización
- Logging de operaciones críticas

-------------------------------------------------------------------------------------------------------------------------------------------------
# DVP_TEST - Sistema de Facturación API

API REST desarrollada en .NET 8 con Clean Architecture para la gestión de facturación (Buscar y crear) del sistema LabDev.

## Descripción

DVP_TEST es una API RESTful que implementa un sistema de facturación con arquitectura limpia, permitiendo la gestión de facturación de manera eficiente y escalable.

## Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

DVP_TEST/
- DVP_TEST.Domain/          # Entidades de negocio y contratos
- DVP_TEST.Application/     # Lógica de aplicación y casos de uso
- DVP_TEST.Infrastructure/  # Acceso a datos y servicios externos
- DVP_TEST.WebAPI/         # Controladores y configuraci´pn API



## Características Técnicas

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

## Estructura del Proyecto

### Domain Layer
Domain/
- Entities/          # Entidades de negocio
- Interfaces/        # Contratos de repositorios

### Application Layer
Application/
- Services/         # Servicios de aplicación
- Interfaces/       # Contratos de servicios

### Infrastructure Layer
Infrastructure/
- Repositories/     # Implementación de repositorios

### WebAPI Layer
WebAPI/
- Controllers/     # Controladores REST
- DTOs/            # DTOs con Datanotation para validación
- Configuration/   # Configuración de servicios
- Program.cs       # Punto de entrada

## Endpoints Principales

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
GET    /api/facturas/buscarNumFactura/{numero}  # Buscar por n迆mero
GET    /api/facturas/buscarClienteId/{id}       # Buscar por cliente
POST   /api/facturas/crear                      # Crear factura
```

## Configuración

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

## Validaciones
Los DTOs incluyen validaciones automáticas usando Data Annotations:


## Próximas Mejoras

- Implementar autenticación JWT
- Implementar paginación
- Pruebas unitarias e integración

## Tecnolog赤as Utilizadas

- **.NET 8**: Framework principal
- **ADO.NET**: Acceso a datos
- **SQL Server**: Base de datos
- **Clean Architecture**: Patrón arquitectónico
- **Repository Pattern**: Patrón de acceso a datos
- **Data Annotations**: Validaciones

-----------------------------------------------------------------------------------------------------------------------------------------
# DVP_TEST Frontend - Sistema de Facturacion

Aplicacion web desarrollada en Angular 20 para la gestion de facturacion del sistema LabDev, implementando patrones de diseno modernos y arquitectura escalable.

## Descripcion

Frontend Angular que consume la API DVP_TEST para proporcionar una interfaz de usuario intuitiva para la creacion y busqueda de facturas.

## Arquitectura

El proyecto implementa una **arquitectura modular** con separacion clara de responsabilidades:

```
src/
├── app/
│   ├── core/                   # Funcionalidad central de la app
│   │   ├── interceptors/       # Interceptores HTTP
│   ├── features/               # Modulos de funcionalidad
│   │   ├── facturacion/        # Modulo de facturacion
│	│       ├── CasosUso/       # Casos de uso para los diferentes procesos
│	│       ├── Paginas/        # Paginas de componentes para Buscar y crear facturas
│	│       ├── Servicios/      # Servicios y repositorios para el modulo de facturacion
│	│       └── Modelos/        # Modelos que representan las entidades involucradas 
│   └── environments/           # Configuraciones de entorno (URL de APIs)
```

### Patrones Implementados
- **Repository Pattern**: Abstraccion del acceso a datos
- **Facade Pattern**: Simplificacion de interfaces complejas
- **Dependency Injection**: IoC nativo de Angular
- **Feature Modules**: Organizacion modular por funcionalidad

## Caracteristicas Tecnicas

### Framework y Version
- **Angular 20**: Framework principal
- **TypeScript**: Lenguaje de desarrollo
- **RxJS**: Programacion reactiva
- **Bootstrap**: UI Components (opcional)

### Funcionalidades Principales
- **Crear Facturas**: Interfaz completa para facturacion
- **Buscar Facturas**: Busqueda por numero y filtros
- **Gestion de Productos**: Catalogo y seleccion
- **Manejo de Errores**: Interceptor global
- **Responsive Design**: Adaptable a diferentes dispositivos

## Estructura del Proyecto

### Core Module
```
core/
├── interceptors/
│   ├── error.interceptor.ts          # Manejo global de errores

```

### Features Module
```
features/
├── facturacion/
│   ├── components/
│   │   ├── crear-factura/           # Componente crear factura
│   │   ├── buscar-factura/          # Componente buscar factura
│   │   └── detalle-factura/         # Componente detalle
│   ├── models/
│   │   ├── factura.model.ts         # Modelo de factura
│   │   └── factura-detalle.model.ts # Modelo detalle
│   ├── services/
│   │   ├── factura.service.ts       # Servicio de facturacion
│   │   └── factura.facade.ts        # Facade de facturacion
│   ├── repositories/
│   │   └── factura.repository.ts    # Repository de facturas
│   └── facturacion.module.ts
│
├── clientes/
│   ├── components/
│   ├── models/
│   ├── services/
│   └── repositories/
│
└── productos/
    ├── components/
    ├── models/
    ├── services/
    └── repositories/
```

### Environments
```
environments/
├── environment.ts              # Desarrollo
├── environment.prod.ts         # Produccion
```

## Configuracion

### Requisitos Previos
- Node.js 18+ 
- Angular CLI 20
- NPM o Yarn

### Instalacion

1. **Clonar el repositorio**
   ```bash
   git clone <url-del-repositorio>
   cd dvp-test-frontend
   ```

2. **Instalar dependencias**
   ```bash
   npm install
   # o
   yarn install
   ```

3. **Configurar variables de entorno**
   
   Editar `src/environments/environment.ts`:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:5001/api',
     apiTimeout: 30000,
     enableLogging: true
   };
   ```

4. **Ejecutar la aplicacion**
   ```bash
   ng serve
   # La app estara disponible en http://localhost:4200
   ```


## Funcionalidades de la Aplicación

### Crear Factura
- Formulario reactivo con validaciones
- Seleccion de cliente desde dropdown
- Agregacion dinamica de productos
- Calculo automatico de totales
- Validacion de stock disponible

### Buscar Factura
- Busqueda por numero de factura
- Busqueda por cliente



## Proximas Mejoras

- Implementar lazy loading completo
- Anadir tests unitarios completos
- CRUD completo para productos, clientes, facturas y usuasrios
- Implementar sistema de autenticacion y autprizacion

## Tecnologias Utilizadas

- **Angular 20**: Framework principal
- **TypeScript**: Lenguaje de programacion
- **RxJS**: Programacion reactiva
- **Angular CLI**: Herramientas de desarrollo
- **SCSS**: Preprocesador CSS
- **Angular Forms**: Formularios reactivos

