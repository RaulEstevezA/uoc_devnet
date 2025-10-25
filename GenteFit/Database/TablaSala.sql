

-- Script para crear la tabla Sala en SQL Server


CREATE TABLE Sala (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    AforoMax INT NOT NULL,
    Disponible BIT NOT NULL
);


-- Script para insertar datos de ejemplo en la tabla Sala
-- Id es autoincremental, por lo que no es necesario especificarlo en el INSERT

INSERT INTO Sala (Nombre, AforoMax, Disponible) VALUES
('Sala Yoga', 16, 1),
('Sala Cardio', 16, 1);


-- Verificar los datos insertados

SELECT * FROM Sala;


-- Borrar la tabla Sala si es necesario

DROP TABLE Sala;
