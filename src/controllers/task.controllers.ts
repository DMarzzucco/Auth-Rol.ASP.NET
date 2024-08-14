import { Request, Response } from "express";
import { QueryHanlder } from "../service/handler_queries";
import { ProducIt } from "../interface/interface";

const db = new QueryHanlder()
export default class TaskControlls {

    public async getTask(_req: Request, res: Response) {
        try {
            const result = await db.get()
            if (result.length === 0) {
                return res.status(400).json({ message: "no task" })
            }
            return res.status(200).json(result)
        } catch (error: any) {
            return res.status(500).json(error.message)
        }
    }

    public async createTask(req: Request, res: Response) {
        const object: ProducIt = req.body;
        try {
            if (!object.image || !object.description || !object.title) {
                return res.status(401).json({ message: "title, image and description must be required" })
            }
            const result = await db.post(object)
            return res.status(200).json(result)
        } catch (error: any) {
            return res.status(500).json(error.message)
        }
    }
}