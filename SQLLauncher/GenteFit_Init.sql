IF DB_ID('GenteFit') IS NULL
BEGIN
    CREATE DATABASE GenteFit;
END
GO

USE GenteFit;
GO



-- Crear tablas y otros objetos de la base de datos

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
    dni NVARCHAR(9) NOT NULL UNIQUE,   -- 9 caracteres alfanum?ricos
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
    SalaId INT NOT NULL,
    FechaInicio DATETIME2 NOT NULL,
    FechaFin DATETIME2 NOT NULL,
    CONSTRAINT FK_Sesion_Actividad FOREIGN KEY (ActividadId)
        REFERENCES Actividad(Id),
    CONSTRAINT FK_Sesion_Instructor FOREIGN KEY (InstructorId)
        REFERENCES Instructor(Id),
    CONSTRAINT FK_Sesion_Sala FOREIGN KEY (SalaId)
        REFERENCES Sala(Id),
    CONSTRAINT CK_Sesion_RangoHoras CHECK (FechaFin > FechaInicio)
);


CREATE TABLE Reserva (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    SesionId INT NOT NULL,
    EstadoReserva INT NOT NULL,
    FechaReserva DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    PosicionEspera INT NULL,
    CONSTRAINT FK_Reserva_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(Id),
    CONSTRAINT FK_Reserva_Sesion FOREIGN KEY (SesionId)
        REFERENCES Sesion(Id),
    CONSTRAINT UQ_Reserva_Cliente_Sesion UNIQUE (ClienteId, SesionId),
    CONSTRAINT CK_Reserva_Estado CHECK (EstadoReserva BETWEEN 1 AND 4)
);

CREATE TABLE ReservaCancelada (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    SesionId INT NOT NULL,
    FechaCancelacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Motivo NVARCHAR(200) NULL,

    -- datos �tiles para informes
    PosicionEnCancelacion INT NULL,
    EstadoPrevio NVARCHAR(20) NOT NULL,

    CONSTRAINT FK_ReservaCancelada_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(Id),

    CONSTRAINT FK_ReservaCancelada_Sesion FOREIGN KEY (SesionId)
        REFERENCES Sesion(Id)
);


-- Insertar Administrador por defecto

INSERT INTO Usuario (
    username,
    email,
    passwordHash,
    TipoRolId,
    activo,
    creadoEn
)
VALUES (
    'admin',
    'admin@gentefit.com',
    'admin',
    1,
    1,
    GETDATE()
);