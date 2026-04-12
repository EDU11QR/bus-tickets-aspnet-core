USE BusTicketSystemDB;
GO

-- VERIFICAR DATOS INSERTADOS

SELECT * FROM Roles;
GO

SELECT * FROM Usuarios;
GO

SELECT * FROM Buses;
GO

SELECT * FROM Rutas;
GO

SELECT * FROM Horarios;
GO


-- PROBAR STORED PROCEDURES

EXEC sp_ListarBuses;
GO

EXEC sp_ListarRutas;
GO

EXEC sp_ListarHorarios;
GO


-- GENERAR ASIENTOS PARA BUSES

EXEC sp_GenerarAsientosPorBus @IdBus = 1, @Cantidad = 40, @Piso = 1;
GO

EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 1;
GO

EXEC sp_GenerarAsientosPorBus @IdBus = 2, @Cantidad = 25, @Piso = 2;
GO

EXEC sp_GenerarAsientosPorBus @IdBus = 3, @Cantidad = 45, @Piso = 1;
GO


-- VERIFICAR ASIENTOS GENERADOS

SELECT * FROM Asientos;
GO


-- PRUEBA DE INSERTAR BUS

EXEC sp_InsertarBus
    @Placa = 'JKL-999',
    @Modelo = 'Volvo 9800',
    @Capacidad = 50,
    @Pisos = 2,
    @Estado = 1;
GO

EXEC sp_ListarBuses;
GO