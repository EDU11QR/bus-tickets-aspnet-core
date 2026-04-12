-- entramos a la bd
USE BusTicketSystemDB;
GO

-- INSERTAMOS ROLES
INSERT INTO Roles (NombreRol, Estado)
VALUES 
('Administrador', 1),
('Cliente', 1);
GO

-- INSERTAMOS BUSES
INSERT INTO Buses (Placa, Modelo, Capacidad, Pisos, Estado)
VALUES
('ABC-123', 'Mercedes Benz O500', 40, 1, 1),
('DEF-456', 'Volvo 9800', 50, 2, 1),
('GHI-789', 'Scania K410', 45, 1, 1);
GO

-- INSERTAMOS RUTAS
INSERT INTO Rutas (Origen, Destino, DuracionEstimada, PrecioBase, Estado)
VALUES
('Lima', 'Arequipa', '16 horas', 80.00, 1),
('Lima', 'Cusco', '20 horas', 120.00, 1),
('Arequipa', 'Tacna', '6 horas', 45.00, 1),
('Cusco', 'Puno', '8 horas', 55.00, 1);
GO

-- INSERTAMOS RUTAS
INSERT INTO Horarios (IdRuta, IdBus, FechaSalida, HoraSalida, HoraLlegada, Precio, Estado)
VALUES
(1, 1, '2026-04-20', '08:00:00', '23:59:00', 85.00, 1),
(2, 2, '2026-04-21', '07:30:00', '23:30:00', 125.00, 1),
(3, 3, '2026-04-22', '09:00:00', '15:00:00', 50.00, 1),
(4, 1, '2026-04-23', '06:00:00', '14:00:00', 60.00, 1);
GO