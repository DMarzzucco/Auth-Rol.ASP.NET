DROP TABLE IF EXISTS dat_bas;
CREATE TABLE IF NOT EXISTS dat_bas (
    id SERIAL PRIMARY KEY,
    title VARCHAR (255) NOT NULL,
    image TEXT NOT NULL,
    description TEXT,
    create_at TIMESTAMP DEFAULT NOW(),    
    update_at TIMESTAMP DEFAULT NOW()    
) 