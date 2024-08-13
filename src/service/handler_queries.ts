import { bsData } from "../db/bsData";
import { Proms, ProducIt } from "../interface/interface"

export class QueryHanlder {

    async get(): Promise<Proms[]> {
        try {
            const result = await bsData.query("SELECT * FROM dat_bas")
            return result.rows
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async post(data: ProducIt): Promise<Proms> {
        try {
            const object: ProducIt = { ...data }
            const result = await bsData.query(
                "INSERT INTO dat_bas (title, image, description) VALUES ($1, $2, $3 ) RETURNING *",
                [object.title, object.image, object.description]
            )
            return result.rows[0]
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async put(data: ProducIt): Promise<Proms> {
        try {
            const object: ProducIt = { ...data }
            if (object.id === undefined) {
                throw new Error(' Id is required for update')
            }
            const result = await bsData.query(
                "UPDATE dat_bas SET title = $1, image = $2, description = $3, update_at = NOW() WHERE id = $4 RETURNING *",
                [object.title, object.image, object.description, object.id]
            )
            return result.rows[0]
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async delete(id: number): Promise<void> {
        try {
            await bsData.query("DELETE FROM dat_bas WHERE id = $1", [id])
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
}