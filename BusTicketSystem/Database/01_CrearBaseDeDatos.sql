-- creacion de la BD
CREATE DATABASE BusTicketSystemDB;
GO

-- Inicialisamos la BD
USE BusTicketSystemDB;
GO

--Creacion de las tablas 

-- 1 TABLA ROLES
CREATE TABLE Roles (
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    NombreRol VARCHAR(50) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1
);
GO

--2 TABLA USUARIOS
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Correo VARCHAR(150) NOT NULL UNIQUE,
    ClaveHash VARCHAR(255) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- 3 TABLA UsuarioRol
CREATE TABLE UsuarioRol (
    IdUsuarioRol INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    IdRol INT NOT NULL,
    CONSTRAINT FK_UsuarioRol_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_UsuarioRol_Rol FOREIGN KEY (IdRol) REFERENCES Roles(IdRol),
    CONSTRAINT UQ_UsuarioRol UNIQUE (IdUsuario, IdRol)
);
GO

--4 TABLA BUSES
CREATE TABLE Buses (
    IdBus INT PRIMARY KEY IDENTITY(1,1),
    Placa VARCHAR(20) NOT NULL UNIQUE,
    Modelo VARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    Pisos INT NOT NULL DEFAULT 1,
    Estado BIT NOT NULL DEFAULT 1
);
GO

--5 TABLA ASIENTOS
CREATE TABLE Asientos (
    IdAsiento INT PRIMARY KEY IDENTITY(1,1),
    IdBus INT NOT NULL,
    NumeroAsiento VARCHAR(10) NOT NULL,
    Piso INT NOT NULL DEFAULT 1,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Asientos_Bus FOREIGN KEY (IdBus) REFERENCES Buses(IdBus),
    CONSTRAINT UQ_Asiento_Bus_Numero UNIQUE (IdBus, NumeroAsiento)
);
GO

--6 TABLA RUTAS
CREATE TABLE Rutas (
    IdRuta INT PRIMARY KEY IDENTITY(1,1),
    Origen VARCHAR(100) NOT NULL,
    Destino VARCHAR(100) NOT NULL,
    DuracionEstimada TIME NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

--7 TABLA HORARIOS
CREATE TABLE Horarios (
    IdHorario INT PRIMARY KEY IDENTITY(1,1),
    IdRuta INT NOT NULL,
    IdBus INT NOT NULL,
    FechaSalida DATE NOT NULL,
    HoraSalida TIME NOT NULL,
    HoraLlegada TIME NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Horarios_Ruta FOREIGN KEY (IdRuta) REFERENCES Rutas(IdRuta),
    CONSTRAINT FK_Horarios_Bus FOREIGN KEY (IdBus) REFERENCES Buses(IdBus)
);
GO

--8 TABLA CLIENTES
CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL UNIQUE,
    Dni VARCHAR(15) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    CONSTRAINT FK_Clientes_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
GO

-- 9 TABLA RESERVAS
CREATE TABLE Reservas (
    IdReserva INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    IdHorario INT NOT NULL,
    FechaReserva DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(30) NOT NULL DEFAULT 'PENDIENTE',
    CodigoReserva VARCHAR(20) NOT NULL UNIQUE,
    CONSTRAINT FK_Reservas_Cliente FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    CONSTRAINT FK_Reservas_Horario FOREIGN KEY (IdHorario) REFERENCES Horarios(IdHorario)
);
GO

--10 TABLA DetalleReserva
CREATE TABLE DetalleReserva (
    IdDetalleReserva INT PRIMARY KEY IDENTITY(1,1),
    IdReserva INT NOT NULL,
    IdAsiento INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleReserva_Reserva FOREIGN KEY (IdReserva) REFERENCES Reservas(IdReserva),
    CONSTRAINT FK_DetalleReserva_Asiento FOREIGN KEY (IdAsiento) REFERENCES Asientos(IdAsiento)
);
GO



SELECT h.*, r.Origen, r.Destino, b.Placa
FROM Horarios h
INNER JOIN Rutas r ON h.IdRuta = r.IdRuta
INNER JOIN Buses b ON h.IdBus = b.IdBus





GO


USE BusTicketSystemDB;
GO

SELECT IdRuta, Origen, Destino, DuracionEstimada
FROM Rutas;

--aqui solo actualice las horas pq en rutas se definieron como varchar pero lo cambie a time

UPDATE Rutas SET DuracionEstimada = '16:00:00' WHERE IdRuta = 1;
UPDATE Rutas SET DuracionEstimada = '20:00:00' WHERE IdRuta = 2;
UPDATE Rutas SET DuracionEstimada = '06:00:00' WHERE IdRuta = 3;
UPDATE Rutas SET DuracionEstimada = '10:00:00' WHERE IdRuta = 4;

select * from Rutas;

--aqui cree nuevas tablas para el tema de las rutas, ya que por logica en cada departamento/provincia hay un terminal xxxx, entonces por ello se crearon esas tablas
--para tambien en horarios tener mas claridad el origen y llegada
CREATE TABLE Departamentos (
    IdDepartamento INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100)
);
GO


CREATE TABLE Provincias (
    IdProvincia INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100),
    IdDepartamento INT,

    FOREIGN KEY (IdDepartamento) REFERENCES Departamentos(IdDepartamento)
);
GO


CREATE TABLE Distritos (
    IdDistrito INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100),
    CodigoPostal VARCHAR(10),
    IdProvincia INT,

    FOREIGN KEY (IdProvincia) REFERENCES Provincias(IdProvincia)
);
GO


CREATE TABLE Terminales (
    IdTerminal INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100),
    IdDistrito INT,
    Direccion VARCHAR(200),
    Estado BIT DEFAULT 1,

    FOREIGN KEY (IdDistrito) REFERENCES Distritos(IdDistrito)
);
GO


--aqui solo a adimos unos campos y vinculamos con otras tablas
ALTER TABLE Rutas
ADD IdTerminalOrigen INT,
    IdTerminalDestino INT;


ALTER TABLE Rutas
ADD CONSTRAINT FK_Ruta_TerminalOrigen 
FOREIGN KEY (IdTerminalOrigen) REFERENCES Terminales(IdTerminal);

ALTER TABLE Rutas
ADD CONSTRAINT FK_Ruta_TerminalDestino 
FOREIGN KEY (IdTerminalDestino) REFERENCES Terminales(IdTerminal);

SELECT 
    COLUMN_NAME,
    DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Rutas'
  AND COLUMN_NAME = 'DuracionEstimada';

  ALTER TABLE Rutas
ALTER COLUMN DuracionEstimada TIME NOT NULL;
GO