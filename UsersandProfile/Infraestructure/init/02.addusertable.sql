-- Crear la tabla 'users'
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password_hash TEXT NOT NULL,
    role VARCHAR(255) NOT NULL,
    registration_date TIMESTAMP WITH TIME ZONE NOT NULL,
    email_confirmation BOOLEAN NOT NULL
);
