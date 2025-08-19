----------------------------------------------------
-- CREAR BASE DE DATOS Y USUARIO DEVELOPER
----------------------------------------------------

-- Crear base de datos (LabDev)
IF DB_ID('LabDev') IS NULL
BEGIN
    CREATE DATABASE LabDev;
END
GO

----------------------------------------------------
-- CREAR USUARIO DEVELOPER - SOLO EN CASO QUE SE REQUIERA
----------------------------------------------------
-- Crea un login de acceso al motor de SQL Server, no está ligado aún a ninguna base de datos aun.
-- sys.server_principals: contiene logins del servidor
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'developer')
BEGIN
    CREATE LOGIN developer WITH PASSWORD = 'abc123ABC';
END
GO

-- Usar la BD
USE LabDev;
GO

-- Crear usuario dentro de la BD especifica, asociado al login anterior.
-- sys.database_principals: contiene usuarios dentro de la BD.
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'developer') 
BEGIN
    -- Crea un usuario interno llamado developer vinculado al login del servidor, permitiendo que el login tenga acceso específico a esta BD
    CREATE USER developer FOR LOGIN developer; 
END
GO

-- Dar permisos de dueño de base de datos (acceso total). 
-- Agrega el usuario developer al rol db_owner.
ALTER ROLE db_owner ADD MEMBER developer;
GO


----------------------------------------------------
-- SECCIÓN PARA ELIMINAR EL USUARIO Y LOGIN
-- (ejecutar si quieres revertir cambios)
----------------------------------------------------
/*
USE LabDev;
GO
-- Quitar al usuario de los roles
ALTER ROLE db_owner DROP MEMBER developer;
GO

-- Eliminar usuario de la BD
DROP USER IF EXISTS developer;
GO

-- Cambiar a master para poder borrar login
USE master;
GO
DROP LOGIN IF EXISTS developer;
GO
*/

