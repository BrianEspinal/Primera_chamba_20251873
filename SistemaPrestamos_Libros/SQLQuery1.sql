USE master;
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaPrestamos')
    CREATE DATABASE SistemaPrestamos;
GO
USE SistemaPrestamos;
GO

IF OBJECT_ID('Prestamos','U')  IS NOT NULL DROP TABLE Prestamos;
IF OBJECT_ID('Materiales','U') IS NOT NULL DROP TABLE Materiales;
IF OBJECT_ID('Personas','U')   IS NOT NULL DROP TABLE Personas;
GO

CREATE TABLE Materiales (
    Id          INT           IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL,
    Codigo      NVARCHAR(50)  NOT NULL UNIQUE,
    Tipo        NVARCHAR(20)  NOT NULL,
    Disponible  BIT           NOT NULL DEFAULT 1,
    Propietario NVARCHAR(100) NOT NULL,
    Materia     NVARCHAR(80)  NULL,
    Paginas     INT           NULL,
    Autor       NVARCHAR(100) NULL,
    Edicion     NVARCHAR(30)  NULL,
    Categoria   NVARCHAR(80)  NULL
);

CREATE TABLE Personas (
    Id     INT           IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Grado  NVARCHAR(50)  NOT NULL DEFAULT 'Sin especificar'
);

CREATE TABLE Prestamos (
    Id            INT           IDENTITY(1,1) PRIMARY KEY,
    IdPersona     INT           NOT NULL REFERENCES Personas(Id),
    IdMaterial    INT           NOT NULL REFERENCES Materiales(Id),
    FechaPrestamo DATETIME      NOT NULL DEFAULT GETDATE(),
    FechaDevol    DATETIME      NULL,
    Devuelto      BIT           NOT NULL DEFAULT 0,
    Nota          NVARCHAR(250) NULL
);
GO
