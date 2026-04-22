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
    @Pagina INT,
    @FilasPorPagina INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Saltos INT;
    SET @Saltos = (@Pagina - 1) * @FilasPorPagina;

    SELECT 
        IdBus,
        Placa,
        Modelo,
        Capacidad,
        Pisos,
        Estado
    FROM Buses
    ORDER BY IdBus ASC
    OFFSET @Saltos ROWS
    FETCH NEXT @FilasPorPagina ROWS ONLY;
END;
GO

-- PA PARA CONTAR BUSES
CREATE OR ALTER PROCEDURE sp_ContarBuses
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS TotalRegistros
    FROM Buses;
END;
GO

CREATE OR ALTER PROCEDURE sp_ListarBusesCombo
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
    WHERE Estado = 1
    ORDER BY IdBus ASC;
END;
GO

exec sp_ListarBuses 1,30;

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
    @DuracionEstimada TIME,
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
    @DuracionEstimada TIME,
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

CREATE OR ALTER PROCEDURE sp_ListarHorarios
    @VerEliminados BIT
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

    WHERE (@VerEliminados = 1 OR h.Estado = 1)

    ORDER BY h.IdHorario DESC;
END
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



-- GENERAMOS ASIENTOS
EXEC sp_GenerarAsientosPorBus @IdBus = 1, @Cantidad = 40, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 1;
EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 2;
EXEC sp_GenerarAsientosPorBus @IdBus = 3, @Cantidad = 45, @Piso = 1;
GO

SELECT * FROM Asientos;
GO


--SP para volver activo o restaurar un horario (ponerlo para seleccionar)

CREATE OR ALTER PROCEDURE sp_RestaurarHorario
    @IdHorario INT
AS
BEGIN
    UPDATE Horarios
    SET Estado = 1
    WHERE IdHorario = @IdHorario
END

GO



USE BusTicketSystemDB;
GO

--Primero se hizo con js, sin embargo lo migre al back para que sea menos carga visualmente
-- esto es para la paginacion
CREATE OR ALTER PROCEDURE dbo.sp_ListarHorariosPaginado
    @Pagina INT,
    @Cantidad INT,
    @VerEliminados BIT,
    @Fecha DATE = NULL
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
    WHERE 
        (@VerEliminados = 1 OR h.Estado = 1)
        AND (@Fecha IS NULL OR h.FechaSalida = @Fecha)

    ORDER BY h.IdHorario DESC
    OFFSET (@Pagina - 1) * @Cantidad ROWS
    FETCH NEXT @Cantidad ROWS ONLY;
END

GO

--esto hace el conteo por registros

CREATE OR ALTER PROCEDURE sp_TotalHorarios
    @VerEliminados BIT,
    @Fecha DATE = NULL
AS
BEGIN
    SELECT COUNT(*)
    FROM Horarios
    WHERE 
        (@VerEliminados = 1 OR Estado = 1)
        AND (@Fecha IS NULL OR FechaSalida = @Fecha)
END
GO


--podemos listar terminales con sus forenkey , por el momento solo lo uso para rutas 
--pero se puede usar en otros modulos
CREATE OR ALTER PROCEDURE sp_ListarTerminales
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.IdTerminal,
        t.Nombre AS Terminal,
        d.Nombre AS Distrito,
        p.Nombre AS Provincia,
        dep.Nombre AS Departamento,
        d.CodigoPostal,
        t.Direccion,
        t.Estado
    FROM Terminales t
    INNER JOIN Distritos d ON t.IdDistrito = d.IdDistrito
    INNER JOIN Provincias p ON d.IdProvincia = p.IdProvincia
    INNER JOIN Departamentos dep ON p.IdDepartamento = dep.IdDepartamento
    ORDER BY dep.Nombre, p.Nombre, d.Nombre, t.Nombre;
END
GO

EXEC sp_ListarTerminales;

--igualmente, solo que en este caso solo nos aayudara para el combo y seleccionar que provincias tenemos por departamente
CREATE OR ALTER PROCEDURE sp_ProvinciasPorDepartamento
@IdDepartamento INT
AS
BEGIN
    SELECT IdProvincia, Nombre
    FROM Provincias
    WHERE IdDepartamento = @IdDepartamento
END
GO

--igual que el de arriba
CREATE OR ALTER PROCEDURE sp_DistritosPorProvincia
@IdProvincia INT
AS
BEGIN
    SELECT IdDistrito, Nombre
    FROM Distritos
    WHERE IdProvincia = @IdProvincia
END
GO


--igual para filtar terminales por istrito
CREATE OR ALTER PROCEDURE sp_TerminalesPorDistrito
@IdDistrito INT
AS
BEGIN
    SELECT IdTerminal, Nombre
    FROM Terminales
    WHERE IdDistrito = @IdDistrito AND Estado = 1
END
GO

--igual 
CREATE OR ALTER PROCEDURE sp_ListarDepartamentos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdDepartamento, Nombre
    FROM Departamentos
    ORDER BY Nombre
END
GO


exec sp_ListarRutas;