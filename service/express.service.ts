import { bsData } from "../db/bsData";
import { Date, CreateData, UpdateData } from "../interface/interface"

export class ServiceExpress {

    async get(): Promise<Date[]> {
        try {
            const result = await bsData.query("SELECT * FROM dat_bas")
            return result.rows
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async getByID(id: number): Promise<Date> {
        try {
            const result = await bsData.query("SELECT * FROM dat_bas WHERE id = $1", [id])
            if (result.rows.length === 0) {
                throw new Error(`Date with id: ${id} not found`)
            }
            return result.rows[0]
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async create(data: CreateData): Promise<CreateData> {
        try {
            const result = await bsData.query(
                "INSERT INTO dat_bas (title, image, description) VALUES ($1, $2, $3 ) RETURNING *",
                [data.title, data.image, data.description]
            )
            return result.rows[0]
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async update(id: number, data: UpdateData): Promise<UpdateData> {
        try {
            const result = await bsData.query(
                "UPDATE dat_bas SET title = $1, image = $2, description = $3, update_at = NOW() WHERE id = $4 RETURNING *",
                [data.title, data.image, data.description, id]
            )
            if (result.rows.length === 0) {
                throw new Error(`Date with id: ${id} not found`)
            }
            return result.rows[0]
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async delete(id: number): Promise<Date> {
        try {
            const date = await bsData.query("DELETE FROM dat_bas WHERE id = $1", [id])
            if (date.rows.length === 0) {
                throw new Error(`Date with id: ${id} not found`)
            }
            return date;
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
}