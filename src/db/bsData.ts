import { Pool } from "pg";

export const bsData = new Pool({
    user: "user",
    host: "db",
    database: "data_base",
    port: 5432,
    password:"password"
})