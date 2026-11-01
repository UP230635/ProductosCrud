CREATE DATABASE IF NOT EXISTS productos_db;
USE productos_db;

CREATE TABLE IF NOT EXISTS productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255) NOT NULL DEFAULT '',
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL DEFAULT 0
);

INSERT INTO productos (nombre, descripcion, precio, stock) VALUES
('Laptop', 'Laptop 15 pulgadas', 12999.99, 10),
('Mouse', 'Mouse inalambrico', 299.50, 50),
('Teclado', 'Teclado mecanico', 899.00, 25);
