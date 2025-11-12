-- Script para crear tablas en la base de datos GenteFit



-- Tipos de rol (ADMINISTRADOR, ENCARGADO, RECEPCIONISTA, CLIENTE, MONITOR)
CREATE TABLE TipoRol (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO TipoRol (nombre) VALUES
('ADMINISTRADOR'),
('ENCARGADO'),
('RECEPCIONISTA'),
('CLIENTE'),
('MONITOR');



CREATE TABLE Usuario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL UNIQUE,
    passwordHash NVARCHAR(255) NOT NULL,
    TipoRolId INT NOT NULL,
    CONSTRAINT FK_Usuario_TipoRol FOREIGN KEY (TipoRolId)
        REFERENCES TipoRol(Id),
    activo BIT NOT NULL DEFAULT 1,
    creadoEn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);



CREATE TABLE Cliente (
    id INT PRIMARY KEY,                -- mismo id que Usuario
    dni NVARCHAR(9) NOT NULL UNIQUE,   -- 9 caracteres alfanum�ricos
    nombre NVARCHAR(50) NOT NULL,
    apellido1 NVARCHAR(50) NOT NULL,
    apellido2 NVARCHAR(50) NULL,
    email NVARCHAR(50) NULL,
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (id)
        REFERENCES Usuario(id)
);


CREATE TABLE Instructor (
    id INT PRIMARY KEY,                      -- mismo id que Usuario
    nombre NVARCHAR(50) NOT NULL,
    apellido1 NVARCHAR(50) NOT NULL,
    apellido2 NVARCHAR(50) NULL,
    CONSTRAINT FK_Monitor_Usuario FOREIGN KEY (id)
        REFERENCES Usuario(id)
);


CREATE TABLE Sala (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    AforoMax INT NOT NULL,
    Disponible BIT NOT NULL
);


CREATE TABLE Actividad (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    DuracionMin INT NOT NULL CHECK (DuracionMin > 0),
    PlazasMax INT NOT NULL DEFAULT 16 CHECK (PlazasMax > 0)
);


CREATE TABLE Sesion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId INT NOT NULL,
    InstructorId INT NOT NULL,
    FechaInicio DATETIME2 NOT NULL,
    FechaFin DATETIME2 NOT NULL,
    CONSTRAINT FK_Sesion_Actividad FOREIGN KEY (ActividadId)
        REFERENCES Actividad(Id),
    CONSTRAINT FK_Sesion_Instructor FOREIGN KEY (InstructorId)
        REFERENCES Instructor(id),
    CONSTRAINT CK_Sesion_RangoHoras CHECK (FechaFin > FechaInicio)
);


CREATE TABLE Reserva (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    SesionId INT NOT NULL,
    EstadoReserva NVARCHAR(20) NOT NULL,  -- ahora se guarda el nombre del estado directamente
    FechaReserva DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PosicionEspera INT NULL,
    CONSTRAINT FK_Reserva_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(Id),
    CONSTRAINT FK_Reserva_Sesion FOREIGN KEY (SesionId)
        REFERENCES Sesion(Id),
    CONSTRAINT UQ_Reserva_Cliente_Sesion UNIQUE (ClienteId, SesionId),
    CONSTRAINT CK_Reserva_Posicion CHECK (PosicionEspera IS NULL OR PosicionEspera > 0)
);


-- =============================================
-- BORRADO COMPLETO DE TABLAS (ORDEN CORRECTO)
-- =============================================


DROP TABLE IF EXISTS Reserva;
DROP TABLE IF EXISTS ReservarSala;
DROP TABLE IF EXISTS Sesion;

DROP TABLE IF EXISTS Actividad;
DROP TABLE IF EXISTS Sala;

DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Monitor;


DROP TABLE IF EXISTS Usuario;

DROP TABLE IF EXISTS EstadoReserva;
DROP TABLE IF EXISTS TipoRol;

