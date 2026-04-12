-- ENTRAMOS A LA BD
USE BusTicketSystemDB;
GO

-- CREAMOS PA PARA GENERAR ASIENTOS
CREATE OR ALTER PROCEDURE sp_GenerarAsientosPorBus
    @IdBus INT,
    @Cantidad INT,
    @Piso INT = 1
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Contador INT = 1;
    DECLARE @NumeroAsiento VARCHAR(10);

    WHILE @Contador <= @Cantidad
    BEGIN
        SET @NumeroAsiento = CAST(@Contador AS VARCHAR(10));

        IF NOT EXISTS (
            SELECT 1
            FROM Asientos
            WHERE IdBus = @IdBus
              AND NumeroAsiento = @NumeroAsiento
        )
        BEGIN
            INSERT INTO Asientos (IdBus, NumeroAsiento, Piso, Estado)
            VALUES (@IdBus, @NumeroAsiento, @Piso, 1);
        END

        SET @Contador = @Contador + 1;
    END
END;
GO

--GENERAMOS ASIENTOS
EXEC sp_GenerarAsientosPorBus @IdBus = 1, @Cantidad = 40, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 2;
EXEC sp_GenerarAsientosPorBus @IdBus = 3, @Cantidad = 45, @Piso = 1;
GO

-- PA PARA INSERTAR BUS
CREATE OR ALTER PROCEDURE sp_InsertarBus
    @Placa VARCHAR(20),
    @Modelo VARCHAR(100),
    @Capacidad INT,
    @Pisos INT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Buses (Placa, Modelo, Capacidad, Pisos, Estado)
    VALUES (@Placa, @Modelo, @Capacidad, @Pisos, @Estado);
END;
GO

-- PA PARA LISTAR BUSES
CREATE OR ALTER PROCEDURE sp_ListarBuses
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdBus,
        Placa,
        Modelo,
        Capacidad,
        Pisos,
        Estado
    FROM Buses
    ORDER BY IdBus DESC;
END;
GO

-- PA PARA ACTUALIZAR BUS
CREATE OR ALTER PROCEDURE sp_ActualizarBus
    @IdBus INT,
    @Placa VARCHAR(20),
    @Modelo VARCHAR(100),
    @Capacidad INT,
    @Pisos INT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Buses
    SET
        Placa = @Placa,
        Modelo = @Modelo,
        Capacidad = @Capacidad,
        Pisos = @Pisos,
        Estado = @Estado
    WHERE IdBus = @IdBus;
END;
GO

-- PA PARA ELIMINAR (CAMBIAMOS DE ESTADO)
CREATE OR ALTER PROCEDURE sp_EliminarBus
    @IdBus INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Buses
    SET Estado = 0
    WHERE IdBus = @IdBus;
END;
GO

-- PA PARA OBTENER BUS POR ID
CREATE OR ALTER PROCEDURE sp_ObtenerBusPorId
    @IdBus INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdBus,
        Placa,
        Modelo,
        Capacidad,
        Pisos,
        Estado
    FROM Buses
    WHERE IdBus = @IdBus;
END;
GO

-- PA PARA CREAR RUTA
CREATE OR ALTER PROCEDURE sp_InsertarRuta
    @Origen VARCHAR(100),
    @Destino VARCHAR(100),
    @DuracionEstimada VARCHAR(50),
    @PrecioBase DECIMAL(10,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Rutas (Origen, Destino, DuracionEstimada, PrecioBase, Estado)
    VALUES (@Origen, @Destino, @DuracionEstimada, @PrecioBase, @Estado);
END;
GO

-- PA PARA LISTAR RUTA
CREATE OR ALTER PROCEDURE sp_ListarRutas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdRuta,
        Origen,
        Destino,
        DuracionEstimada,
        PrecioBase,
        Estado
    FROM Rutas
    ORDER BY IdRuta DESC;
END;
GO

-- PA PARA ACTUALIZAR RUTA
CREATE OR ALTER PROCEDURE sp_ActualizarRuta
    @IdRuta INT,
    @Origen VARCHAR(100),
    @Destino VARCHAR(100),
    @DuracionEstimada VARCHAR(50),
    @PrecioBase DECIMAL(10,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Rutas
    SET
        Origen = @Origen,
        Destino = @Destino,
        DuracionEstimada = @DuracionEstimada,
        PrecioBase = @PrecioBase,
        Estado = @Estado
    WHERE IdRuta = @IdRuta;
END;
GO

-- PA PARA ELIMINAR (CAMBIAR DE ESTADO)
CREATE OR ALTER PROCEDURE sp_EliminarRuta
    @IdRuta INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Rutas
    SET Estado = 0
    WHERE IdRuta = @IdRuta;
END;
GO

-- PA PARA OBTENER RUTA POR ID
CREATE OR ALTER PROCEDURE sp_ObtenerRutaPorId
    @IdRuta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdRuta,
        Origen,
        Destino,
        DuracionEstimada,
        PrecioBase,
        Estado
    FROM Rutas
    WHERE IdRuta = @IdRuta;
END;
GO

-- PA PARA CREAR HORARIOS
CREATE OR ALTER PROCEDURE sp_InsertarHorario
    @IdRuta INT,
    @IdBus INT,
    @FechaSalida DATE,
    @HoraSalida TIME,
    @HoraLlegada TIME,
    @Precio DECIMAL(10,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Horarios (IdRuta, IdBus, FechaSalida, HoraSalida, HoraLlegada, Precio, Estado)
    VALUES (@IdRuta, @IdBus, @FechaSalida, @HoraSalida, @HoraLlegada, @Precio, @Estado);
END;
GO

-- PA PARA LISTAR HORARIOS
CREATE OR ALTER PROCEDURE sp_ListarHorarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        h.IdHorario,
        r.Origen,
        r.Destino,
        b.Placa,
        h.FechaSalida,
        h.HoraSalida,
        h.HoraLlegada,
        h.Precio,
        h.Estado
    FROM Horarios h
    INNER JOIN Rutas r ON h.IdRuta = r.IdRuta
    INNER JOIN Buses b ON h.IdBus = b.IdBus
    ORDER BY h.IdHorario DESC;
END;
GO

-- PA PARA ACTUALIZAR HORARIO
CREATE OR ALTER PROCEDURE sp_ActualizarHorario
    @IdHorario INT,
    @IdRuta INT,
    @IdBus INT,
    @FechaSalida DATE,
    @HoraSalida TIME,
    @HoraLlegada TIME,
    @Precio DECIMAL(10,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Horarios
    SET
        IdRuta = @IdRuta,
        IdBus = @IdBus,
        FechaSalida = @FechaSalida,
        HoraSalida = @HoraSalida,
        HoraLlegada = @HoraLlegada,
        Precio = @Precio,
        Estado = @Estado
    WHERE IdHorario = @IdHorario;
END;
GO

-- PA PARA ELIMINAR HORARIO (CAMBIAR DE ESTADO)
CREATE OR ALTER PROCEDURE sp_EliminarHorario
    @IdHorario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Horarios
    SET Estado = 0
    WHERE IdHorario = @IdHorario;
END;
GO

-- PA PARA OBTENER HORARIO POR ID
CREATE OR ALTER PROCEDURE sp_ObtenerHorarioPorId
    @IdHorario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdHorario,
        IdRuta,
        IdBus,
        FechaSalida,
        HoraSalida,
        HoraLlegada,
        Precio,
        Estado
    FROM Horarios
    WHERE IdHorario = @IdHorario;
END;
GO

-- EJECUTAMOS 

EXEC sp_ListarBuses;

EXEC sp_ListarRutas;

EXEC sp_ListarHorarios;

-- GENERAMOS ASIENTOS
EXEC sp_GenerarAsientosPorBus @IdBus = 1, @Cantidad = 40, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 2;
EXEC sp_GenerarAsientosPorBus @IdBus = 3, @Cantidad = 45, @Piso = 1;
GO

SELECT * FROM Asientos;
GO