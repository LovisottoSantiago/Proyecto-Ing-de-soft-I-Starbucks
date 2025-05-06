-- Crear la tabla Cliente
CREATE TABLE Cliente (
    ID_Cliente INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Telefono NVARCHAR(20)
);

-- Crear la tabla FormaPago
CREATE TABLE FormaPago (
    ID_FormaPago INT PRIMARY KEY IDENTITY,
    Descripcion NVARCHAR(150) NOT NULL
);

-- Crear la tabla Producto
CREATE TABLE Producto (
    ID_Producto INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(300),
    PrecioUnitario DECIMAL(10,2),
    Stock INT,
    Estado BIT
);

-- Crear la tabla Proveedor
CREATE TABLE Proveedor (
    ID_Proveedor INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(150)
);

-- Crear la tabla Factura
CREATE TABLE Factura (
    ID_Factura INT PRIMARY KEY IDENTITY,
    Fecha DATETIME,
    ID_Cliente INT,
    Total DECIMAL(10,2),
    ID_FormaPago INT,
    FOREIGN KEY (ID_Cliente) REFERENCES Cliente(ID_Cliente),
    FOREIGN KEY (ID_FormaPago) REFERENCES FormaPago(ID_FormaPago)
);

-- Crear la tabla DetalleFactura
CREATE TABLE DetalleFactura (
    ID_DetalleFactura INT PRIMARY KEY IDENTITY,
    ID_Factura INT,
    ID_Producto INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura),
    FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);

-- Crear la tabla Cobranza
CREATE TABLE Cobranza (
    ID_Cobranza INT PRIMARY KEY IDENTITY,
    ID_Factura INT,
    Fecha DATETIME,
    Monto DECIMAL(10,2),
    ID_FormaPago INT,
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura),
    FOREIGN KEY (ID_FormaPago) REFERENCES FormaPago(ID_FormaPago)
);

-- Crear la tabla OrdenCompra
CREATE TABLE OrdenCompra (
    ID_OrdenCompra INT PRIMARY KEY IDENTITY,
    Fecha DATETIME,
    ID_Proveedor INT,
    Total DECIMAL(10,2),
    FOREIGN KEY (ID_Proveedor) REFERENCES Proveedor(ID_Proveedor)
);

-- Crear la tabla DetalleOrdenCompra
CREATE TABLE DetalleOrdenCompra (
    ID_DetalleOrdenCompra INT PRIMARY KEY IDENTITY,
    ID_OrdenCompra INT,
    ID_Producto INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    FOREIGN KEY (ID_OrdenCompra) REFERENCES OrdenCompra(ID_OrdenCompra),
    FOREIGN KEY (ID_Producto) REFERENCES Producto(ID_Producto)
);




-- Insertar datos en Producto
INSERT INTO Producto (Nombre, Descripcion, PrecioUnitario, Stock, Estado) 
VALUES ('Café Americano', 'Café preparado al estilo americano', 2500, 70, 1),
       ('Café Latte', 'Café con leche vaporizada', 3000, 50, 1),
       ('Café Mocha', 'Café con chocolate y leche', 3500, 70, 1),
       ('Cappuccino', 'Café con espuma de leche', 3200, 60, 1),
       ('Té Verde', 'Infusión de té verde', 1800, 60, 1);




-- DATOS NUEVOS QUE AGREGO A LA BD
INSERT INTO Cliente (Nombre, Email, Telefono) VALUES
('Ana Gómez', 'ana.gomez@email.com', '3415123456'),
('Carlos Pérez', 'carlos.perez@email.com', '3415234567'),
('Lucía Martínez', 'lucia.martinez@email.com', '3415345678'),
('Martín Rodríguez', 'martin.rodriguez@email.com', '3415456789'),
('Sofía Fernández', 'sofia.fernandez@email.com', '3415567890');


INSERT INTO FormaPago (Descripcion) VALUES
('Tarjeta de Débito'), -- ID_FormaPago = 1
('Tarjea de Crédito'), -- ID_FormaPago = 2
('Codigo QR');         -- ID_FormaPago = 3

INSERT INTO Proveedor (Nombre, Email, Telefono, Direccion) VALUES
('Distribuidora de Café S.A.', 'contacto@cafedist.com', '3415678901', 'Av. Libertad 1234'),
('Té y Aromas SRL', 'ventas@teyaromas.com', '3415789012', 'Calle Primavera 567'),
('Proveedores Unidos', 'info@proveedoresunidos.com', '3415890123', 'Boulevard Central 789'),
('Café Selecto', 'contacto@cafeselecto.com', '3415901234', 'Ruta Nacional 9 Km 15');


INSERT INTO Factura (Fecha, ID_Cliente, Total, ID_FormaPago) VALUES
(GETDATE(), 1, 7500, 2),
(GETDATE(), 2, 5200, 1),
(GETDATE(), 3, 6800, 3),
(GETDATE(), 4, 4500, 3),
(GETDATE(), 5, 3200, 1);


INSERT INTO DetalleFactura (ID_Factura, ID_Producto, Cantidad, PrecioUnitario) VALUES
(1, 1, 2, 2500),
(1, 2, 1, 3000),
(2, 5, 3, 1800),
(3, 3, 2, 3500),
(4, 4, 1, 3200),
(5, 2, 1, 3000);


INSERT INTO Cobranza (ID_Factura, Fecha, Monto, ID_FormaPago) VALUES
(1, GETDATE(), 7500, 2),
(2, GETDATE(), 5200, 1),
(3, GETDATE(), 6800, 3),
(4, GETDATE(), 4500, 3),
(5, GETDATE(), 3200, 1);


INSERT INTO OrdenCompra (Fecha, ID_Proveedor, Total) VALUES
(GETDATE(), 1, 15000),
(GETDATE(), 2, 8500),
(GETDATE(), 3, 11200);


INSERT INTO DetalleOrdenCompra (ID_OrdenCompra, ID_Producto, Cantidad, PrecioUnitario) VALUES
(1, 1, 50, 2300),
(1, 2, 30, 2800),
(2, 5, 40, 1600),
(3, 3, 25, 3300),
(3, 4, 20, 3000);






-- CODIGO EXTRA, ayudas, comandos, etc

-- Ponele que hiciste macana insertando datos en la BD y queres vaciar una tabla
-- Primero se vacían tablas hijas (detalle) y luego padres para evitar errores de FK

DELETE FROM Cliente;
DELETE FROM Proveedor;

-- Obviamente esto aplica para la tabla que queres vaciar y su jerarquía.
-- Esto vacía las tablas pero NO las elimina.
-- Hay un detalle, el ID esta puesto automático, entonces no se va a reiniciar.

-- Si queremos reiniciarlo para que empiece desde 0:

DBCC CHECKIDENT ('Cliente', RESEED, 0);
DBCC CHECKIDENT ('Proveedor', RESEED, 0);