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


-- Estados de reserva (RESERVADA, EN_ESPERA, CANCELADA, FINALIZADA)
CREATE TABLE EstadoReserva (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO EstadoReserva (nombre) VALUES
('RESERVADA'),
('EN_ESPERA'),
('CANCELADA'),
('FINALIZADA');


CREATE TABLE Usuario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL UNIQUE,
    passwordHash NVARCHAR(255) NOT NULL,
    tipoRol NVARCHAR(20) NOT NULL 
        CONSTRAINT CK_Usuario_TipoRol CHECK (
            tipoRol IN ('ADMINISTRADOR', 'ENCARGADO', 'RECEPCIONISTA', 'CLIENTE', 'MONITOR')
        )
        CONSTRAINT DF_Usuario_TipoRol DEFAULT 'CLIENTE',   -- valor por defecto
    activo BIT NOT NULL DEFAULT 1,
    creadoEn DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);



CREATE TABLE Cliente (
    id INT PRIMARY KEY,                -- mismo id que Usuario
    dni NVARCHAR(9) NOT NULL UNIQUE,   -- 9 caracteres alfanuméricos
    nombre NVARCHAR(50) NOT NULL,
    apellido1 NVARCHAR(50) NOT NULL,
    apellido2 NVARCHAR(50) NULL,
    email NVARCHAR(50) NULL,
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (id)
        REFERENCES Usuario(id)
);


CREATE TABLE Monitor (
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


CREATE TABLE ReservarSala (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SalaId INT NOT NULL,              -- sala reservada
    SesionId INT NOT NULL,            -- sesión que la ocupa
    Estado NVARCHAR(20) NOT NULL,     -- por ejemplo: 'RESERVADA', 'CANCELADA', etc.
    CONSTRAINT FK_ReservarSala_Sala FOREIGN KEY (SalaId)
        REFERENCES Sala(Id),
    CONSTRAINT FK_ReservarSala_Sesion FOREIGN KEY (SesionId)
        REFERENCES Sesion(Id)
);


CREATE TABLE Actividad (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    DuracionMin INT NOT NULL CHECK (DuracionMin > 0),
    PlazasMax INT NOT NULL DEFAULT 16 CHECK (PlazasMax > 0)
);


CREATE TABLE Sesion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId INT NOT NULL,            -- tipo de actividad
    MonitorId INT NOT NULL,              -- monitor asignado
    FechaInicio DATETIME2 NOT NULL,
    FechaFin DATETIME2 NOT NULL,
    CONSTRAINT FK_Sesion_Actividad FOREIGN KEY (ActividadId)
        REFERENCES Actividad(Id),
    CONSTRAINT FK_Sesion_Monitor FOREIGN KEY (MonitorId)
        REFERENCES Monitor(Id),
    CONSTRAINT CK_Sesion_RangoHoras CHECK (FechaFin > FechaInicio)
);


CREATE TABLE Reserva (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    SesionId INT NOT NULL,
    EstadoReservaId INT NOT NULL,        -- FK a EstadoReserva ('RESERVADA', 'EN_ESPERA', etc.)
    FechaReserva DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PosicionEspera INT NULL,             -- posición en la lista de espera (solo si está en espera)
    CONSTRAINT FK_Reserva_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(Id),
    CONSTRAINT FK_Reserva_Sesion FOREIGN KEY (SesionId)
        REFERENCES Sesion(Id),
    CONSTRAINT FK_Reserva_Estado FOREIGN KEY (EstadoReservaId)
        REFERENCES EstadoReserva(Id),
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

