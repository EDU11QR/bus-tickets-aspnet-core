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


--DESDE AQUI INSERTE MIS PROPIOS REGISTROS

-- INSERTAMOS RUTAS actualizado
INSERT INTO Rutas (Origen, Destino, DuracionEstimada, PrecioBase, Estado)
VALUES
('Lima', 'Arequipa', '16:00:00', 80.00, 1),
('Lima', 'Cusco', '20:00:00', 120.00, 1),
('Arequipa', 'Tacna', '06:00:00', 45.00, 1),
('Cusco', 'Puno', '08:00:00', 55.00, 1);
GO

-- INSERTAMOS HORARIOS
INSERT INTO Horarios (IdRuta, IdBus, FechaSalida, HoraSalida, HoraLlegada, Precio, Estado)
VALUES
(1, 1, '2026-04-20', '08:00:00', '23:59:00', 85.00, 1),
(2, 2, '2026-04-21', '07:30:00', '23:30:00', 125.00, 1),
(3, 3, '2026-04-22', '09:00:00', '15:00:00', 50.00, 1),
(4, 1, '2026-04-23', '06:00:00', '14:00:00', 60.00, 1);
GO



INSERT INTO Departamentos (Nombre) VALUES
('Lima'),
('Arequipa'),
('Cusco'),
('Puno'),
('La Libertad');
GO


INSERT INTO Provincias (Nombre, IdDepartamento) VALUES
('Lima', 1),
('Huaral', 1),
('Arequipa', 2),
('Cusco', 3),
('Puno', 4),
('Trujillo', 5);


INSERT INTO Distritos (Nombre, CodigoPostal, IdProvincia) VALUES
('Miraflores', '15074', 1),
('San Isidro', '15073', 1),
('Huaral', '15201', 2),
('Cercado Arequipa', '04001', 3),
('Cusco', '08001', 4),
('Puno', '21001', 5),
('Trujillo', '13001', 6);


INSERT INTO Terminales (Nombre, IdDistrito, Direccion) VALUES
('Terminal Plaza Norte', 1, 'Av. T pac Amaru'),
('Terminal Javier Prado', 2, 'Av. Javier Prado'),
('Terminal Huaral', 3, 'Centro Huaral'),
('Terminal Terrestre Arequipa', 4, 'Av. Arturo Ib  ez'),
('Terminal Cusco', 5, 'Av. Industrial'),
('Terminal Puno', 6, 'Centro Puno'),
('Terminal Trujillo', 7, 'Av. Am rica Norte');


INSERT INTO Departamentos (Nombre) VALUES
('Lima'), ('Arequipa'), ('Cusco'), ('Puno'), ('La Libertad'),
('Piura'), ('Lambayeque'), ('Tacna'), ('Ica'), ('Jun n'),
('Ayacucho'), ('Hu nuco'), ('Tumbes'), ('Cajamarca'), ('Amazonas'),
('San Mart n'), ('Ucayali'), ('Loreto'), ('Pasco'), ('Moquegua'),
('Apur mac'), ('Huancavelica'), ('Madre de Dios'), ('Ancash'), ('Callao');
GO



INSERT INTO Provincias (Nombre, IdDepartamento) VALUES
('Lima', 1),
('Arequipa', 2),
('Cusco', 3),
('Puno', 4),
('Trujillo', 5),
('Piura', 6),
('Chiclayo', 7),
('Tacna', 8),
('Ica', 9),
('Huancayo', 10),
('Ayacucho', 11),
('Hu nuco', 12),
('Tumbes', 13),
('Cajamarca', 14),
('Chachapoyas', 15),
('Tarapoto', 16),
('Pucallpa', 17),
('Iquitos', 18),
('Pasco', 19),
('Ilo', 20),
('Abancay', 21),
('Huancavelica', 22),
('Puerto Maldonado', 23),
('Huaraz', 24),
('Callao', 25);
GO



INSERT INTO Distritos (Nombre, CodigoPostal, IdProvincia) VALUES
('Miraflores', '15074', 1),
('Cercado Arequipa', '04001', 2),
('Cusco', '08001', 3),
('Puno', '21001', 4),
('Trujillo', '13001', 5),
('Piura', '20001', 6),
('Chiclayo', '14001', 7),
('Tacna', '23001', 8),
('Ica', '11001', 9),
('Huancayo', '12001', 10),
('Ayacucho', '05001', 11),
('Hu nuco', '10001', 12),
('Tumbes', '24001', 13),
('Cajamarca', '06001', 14),
('Chachapoyas', '01001', 15),
('Tarapoto', '22001', 16),
('Pucallpa', '25001', 17),
('Iquitos', '16001', 18),
('Pasco', '19001', 19),
('Ilo', '18001', 20),
('Abancay', '03001', 21),
('Huancavelica', '09001', 22),
('Puerto Maldonado', '17001', 23),
('Huaraz', '02001', 24),
('Callao', '07001', 25);
GO


INSERT INTO Terminales (Nombre, IdDistrito, Direccion) VALUES
('Terminal Plaza Norte', 1, 'Av. T pac Amaru'),
('Terminal Arequipa Central', 2, 'Av. Arturo Ib  ez'),
('Terminal Cusco', 3, 'Av. Industrial'),
('Terminal Puno', 4, 'Centro Puno'),
('Terminal Trujillo', 5, 'Av. Am rica Norte'),
('Terminal Piura', 6, 'Av. Grau'),
('Terminal Chiclayo', 7, 'Av. Balta'),
('Terminal Tacna', 8, 'Av. Bolognesi'),
('Terminal Ica', 9, 'Av. San Mart n'),
('Terminal Huancayo', 10, 'Av. Ferrocarril'),
('Terminal Ayacucho', 11, 'Av. Mariscal C ceres'),
('Terminal Hu nuco', 12, 'Av. Universitaria'),
('Terminal Tumbes', 13, 'Av. Tumbes'),
('Terminal Cajamarca', 14, 'Av. Per '),
('Terminal Chachapoyas', 15, 'Centro'),
('Terminal Tarapoto', 16, 'Av. Aviaci n'),
('Terminal Pucallpa', 17, 'Av. Centenario'),
('Terminal Iquitos', 18, 'Puerto Principal'),
('Terminal Pasco', 19, 'Centro Pasco'),
('Terminal Ilo', 20, 'Av. Costanera'),
('Terminal Abancay', 21, 'Av. N  ez'),
('Terminal Huancavelica', 22, 'Centro'),
('Terminal Puerto Maldonado', 23, 'Av. Le n Velarde'),
('Terminal Huaraz', 24, 'Av. Luzuriaga'),
('Terminal Callao', 25, 'Av. Faucett');
GO