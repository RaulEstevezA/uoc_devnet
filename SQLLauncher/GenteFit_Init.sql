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


-- Insertar datos iniciales

SET IDENTITY_INSERT [dbo].[Actividad] ON 

INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (1, N'Cycling', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (2, N'Body Pump', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (3, N'Pilates', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (4, N'AiguaGim', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (5, N'LM Dance', 46, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (6, N'Body Burn', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (7, N'Core', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (8, N'CrossTraining', 45, 16)
INSERT [dbo].[Actividad] ([Id], [Nombre], [DuracionMin], [PlazasMax]) VALUES (9, N'Zumba', 45, 16)
SET IDENTITY_INSERT [dbo].[Actividad] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoRol] ON 

INSERT [dbo].[TipoRol] ([id], [nombre]) VALUES (1, N'ADMINISTRADOR')
INSERT [dbo].[TipoRol] ([id], [nombre]) VALUES (4, N'CLIENTE')
INSERT [dbo].[TipoRol] ([id], [nombre]) VALUES (2, N'ENCARGADO')
INSERT [dbo].[TipoRol] ([id], [nombre]) VALUES (5, N'MONITOR')
INSERT [dbo].[TipoRol] ([id], [nombre]) VALUES (3, N'RECEPCIONISTA')
SET IDENTITY_INSERT [dbo].[TipoRol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (1, N'admin', N'admin@gentefit.com', N'admin', 1, 1, CAST(N'2025-11-22T18:19:40.8498121' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (2, N'juan', N'juan@gentefit.com', N'1234', 5, 0, CAST(N'2025-11-22T18:43:32.3318269' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (8, N'Dani', N'dani@gentefit.com', N'1234', 5, 1, CAST(N'2025-11-22T20:43:00.7966488' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (9, N'Marcos', N'marcos@gentefit.com', N'1234', 5, 1, CAST(N'2025-11-22T22:10:20.3275864' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (10, N'Raul', N'raul@gentefit.com', N'1234', 5, 1, CAST(N'2025-11-22T22:12:26.1420547' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (11, N'Cliente01', N'carlos.garcia@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T22:40:41.9577373' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (12, N'Cliente02', N'maria.fernandez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T22:56:42.7493674' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (14, N'Cliente03', N'jose.martinez@example.com', N'1234', 4, 1, CAST(N'2025-11-22T22:59:49.4184360' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (15, N'Cliente04', N'laura.sanchez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:06:37.1456647' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (16, N'Cliente05', N'marta.fernandez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:07:20.2880219' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (17, N'Cliente06', N'sergio.navarro@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:08:23.7329400' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (18, N'Cliente07', N'ana.morales@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:09:16.4911821' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (19, N'Cliente08', N'daniel.castro@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:10:20.2850781' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (20, N'Cliente09', N'patricia.herrera@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:11:22.7081147' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (21, N'Cliente10', N'ruben.dominguez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:12:02.4815484' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (22, N'Cliente11', N'elena.garcia@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:12:40.0056826' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (23, N'Cliente12', N'alberto.molina@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:14:49.0808500' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (24, N'Cliente13', N'cristina.jimenez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:15:40.3811388' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (25, N'Cliente14', N'oscar.leon@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:16:18.5609484' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (26, N'Cliente15', N'nuria.reyes@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:28:09.3782459' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (27, N'Cliente16', N'ivan.suarez@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:28:51.2470657' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (28, N'Cliente17', N'beatriz.ramos@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:29:20.3885747' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (29, N'Cliente18', N'manuel.cruz@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:30:12.3090902' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (30, N'Cliente19', N'silvia.vega@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:31:39.6757169' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (31, N'Cliente20', N'adrian.pardo@ejemplo.com', N'1234', 4, 1, CAST(N'2025-11-22T23:32:44.8478580' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (32, N'responsable', N'responsable@gentefit.com', N'1234', 2, 1, CAST(N'2025-11-22T23:34:47.8966325' AS DateTime2))
INSERT [dbo].[Usuario] ([id], [username], [email], [passwordHash], [TipoRolId], [activo], [creadoEn]) VALUES (33, N'recepcion', N'recepcion@gentefit.com', N'1234', 3, 1, CAST(N'2025-11-22T23:35:09.2723603' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[Instructor] ([id], [nombre], [apellido1], [apellido2]) VALUES (8, N'Dani', N'Gomez', N'Roma')
INSERT [dbo].[Instructor] ([id], [nombre], [apellido1], [apellido2]) VALUES (9, N'Marcos', N'Roma', N'Pastor')
INSERT [dbo].[Instructor] ([id], [nombre], [apellido1], [apellido2]) VALUES (10, N'Ra�l', N'Roma', N'Gomez')
GO
SET IDENTITY_INSERT [dbo].[Sala] ON 

INSERT [dbo].[Sala] ([Id], [Nombre], [AforoMax], [Disponible]) VALUES (1, N'Sala 1', 16, 1)
INSERT [dbo].[Sala] ([Id], [Nombre], [AforoMax], [Disponible]) VALUES (2, N'Sala 2', 16, 1)
INSERT [dbo].[Sala] ([Id], [Nombre], [AforoMax], [Disponible]) VALUES (3, N'Sala 3', 16, 1)
INSERT [dbo].[Sala] ([Id], [Nombre], [AforoMax], [Disponible]) VALUES (4, N'Piscina', 16, 1)
SET IDENTITY_INSERT [dbo].[Sala] OFF
GO
SET IDENTITY_INSERT [dbo].[Sesion] ON 

INSERT [dbo].[Sesion] ([Id], [ActividadId], [InstructorId], [SalaId], [FechaInicio], [FechaFin]) VALUES (1, 1, 8, 1, CAST(N'2025-11-24T08:00:00.0000000' AS DateTime2), CAST(N'2025-11-24T08:45:00.0000000' AS DateTime2))
INSERT [dbo].[Sesion] ([Id], [ActividadId], [InstructorId], [SalaId], [FechaInicio], [FechaFin]) VALUES (2, 4, 9, 2, CAST(N'2025-11-24T08:00:00.0000000' AS DateTime2), CAST(N'2025-11-24T08:45:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Sesion] OFF
GO
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (11, N'12345678A', N'Carlos', N'Garc�a', N'L�pez', N'carlos.garcia@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (12, N'23456789B', N'Mar�a', N'Fern�ndez', N'Ruiz', N'maria.fernandez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (14, N'34567890C', N'Jos�', N'Mart�nez', N'S�nchez', N'jose.martinez@example.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (15, N'48291376K', N'Laura', N'S�nchez', N'Romero', N'laura.sanchez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (16, N'83726194P', N'Marta', N'Fern�ndez', N'L�pez', N'marta.fernandez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (17, N'56473829T', N'Sergio', N'Navarro', N'Ruiz', N'sergio.navarro@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (18, N'91827364L', N'Ana', N'Morales', N'Ortega', N'ana.morales@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (19, N'37482915H', N'Daniel', N'Castro', N'G�mez', N'daniel.castro@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (20, N'65748392J', N'Patricia', N'Herrera', N'Mart�n', N'patricia.herrera@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (21, N'82937465S', N'Rub�n', N'Dom�nguez', N'P�rez', N'ruben.dominguez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (22, N'29384756N', N'Elena', N'Garc�a', N'Serrano', N'elena.garcia@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (23, N'74638295R', N'Alberto', N'Molina', N'D�az', N'alberto.molina@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (24, N'56473829C', N'Cristina', N'Jim�nez', N'Vargas', N'cristina.jimenez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (25, N'91827364X', N'�scar', N'Le�n', N'Cabrera', N'oscar.leon@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (26, N'37482915F', N'Nuria', N'Reyes', N'Campos', N'nuria.reyes@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (27, N'65748392Z', N'Iv�n', N'Su�rez', N'Bravo', N'ivan.suarez@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (28, N'82937465Y', N'Beatriz', N'Ramos', N'Gil', N'beatriz.ramos@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (29, N'29384756D', N'Manuel', N'Cruz', N'Pastor', N'manuel.cruz@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (30, N'74638295E', N'Silvia', N'Vega', N'Lozano', N'silvia.vega@ejemplo.com')
INSERT [dbo].[Cliente] ([id], [dni], [nombre], [apellido1], [apellido2], [email]) VALUES (31, N'56473829A', N'Adri�n', N'Pardo', N'Esteban', N'adrian.pardo@ejemplo.com')
GO
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (1, 11, 1, 1, CAST(N'2025-11-22T23:38:37.5900000' AS DateTime2), 1)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (2, 12, 1, 1, CAST(N'2025-11-22T23:40:47.0533333' AS DateTime2), 2)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (3, 14, 1, 1, CAST(N'2025-11-22T23:42:06.4000000' AS DateTime2), 3)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (5, 16, 1, 1, CAST(N'2025-11-22T23:44:52.8600000' AS DateTime2), 4)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (6, 17, 1, 1, CAST(N'2025-11-22T23:45:46.9700000' AS DateTime2), 5)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (7, 18, 1, 1, CAST(N'2025-11-22T23:46:45.1866667' AS DateTime2), 6)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (8, 19, 1, 1, CAST(N'2025-11-22T23:47:18.4100000' AS DateTime2), 7)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (9, 20, 1, 1, CAST(N'2025-11-22T23:48:24.5200000' AS DateTime2), 8)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (10, 21, 1, 1, CAST(N'2025-11-22T23:49:00.5800000' AS DateTime2), 9)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (11, 22, 1, 1, CAST(N'2025-11-22T23:49:38.7766667' AS DateTime2), 10)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (12, 23, 1, 1, CAST(N'2025-11-22T23:50:37.4633333' AS DateTime2), 11)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (13, 24, 1, 1, CAST(N'2025-11-22T23:51:17.4666667' AS DateTime2), 12)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (14, 25, 1, 1, CAST(N'2025-11-22T23:51:51.7200000' AS DateTime2), 13)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (15, 26, 1, 1, CAST(N'2025-11-22T23:52:49.6933333' AS DateTime2), 14)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (16, 27, 1, 1, CAST(N'2025-11-22T23:53:56.5233333' AS DateTime2), 15)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (17, 28, 1, 1, CAST(N'2025-11-22T23:54:41.5833333' AS DateTime2), 16)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (18, 29, 1, 2, CAST(N'2025-11-22T23:57:22.0333333' AS DateTime2), 17)
INSERT [dbo].[Reserva] ([Id], [ClienteId], [SesionId], [EstadoReserva], [FechaReserva], [PosicionEspera]) VALUES (19, 30, 1, 2, CAST(N'2025-11-22T23:58:20.1533333' AS DateTime2), 18)
SET IDENTITY_INSERT [dbo].[Reserva] OFF
GO
SET IDENTITY_INSERT [dbo].[ReservaCancelada] ON 

INSERT [dbo].[ReservaCancelada] ([Id], [ClienteId], [SesionId], [FechaCancelacion], [Motivo], [PosicionEnCancelacion], [EstadoPrevio]) VALUES (1, 15, 1, CAST(N'2025-11-22T23:59:15.1366667' AS DateTime2), N'Cancelado por usuario', 4, N'Reservada')
SET IDENTITY_INSERT [dbo].[ReservaCancelada] OFF
GO