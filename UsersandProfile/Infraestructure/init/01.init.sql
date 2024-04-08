CREATE TABLE IF NOT EXISTS 
profiles (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    avatar_url VARCHAR(255),
    foto_url VARCHAR(255)
);

INSERT INTO profiles (nombre, avatar_url, foto_url)
VALUES ('Paula Misas','http://example.com/avatar/paula.jpg','http://example.com/foto/paula.jpg'),
('Juan Ospina','http://example.com/avatar/juan.jpg','http://example.com/foto/juan.jpg'),
('Sofía Ruiz', 'http://example.com/avatar/sofia.jpg', 'http://example.com/foto/sofia.jpg'),
('Carlos Vega', 'http://example.com/avatar/carlos.jpg', 'http://example.com/foto/carlos.jpg'),
('Lucía Gómez', 'http://example.com/avatar/lucia.jpg', 'http://example.com/foto/lucia.jpg'),
('Miguel Ángel Torres', 'http://example.com/avatar/miguel.jpg', 'http://example.com/foto/miguel.jpg'),
('Ana María López', 'http://example.com/avatar/ana.jpg', 'http://example.com/foto/ana.jpg');