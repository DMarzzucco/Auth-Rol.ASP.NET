import express, { Application, NextFunction, Request, Response } from "express";
import { schema_base_data } from "../db/schema"
import { AuthRouter } from "../routes"

export default class App {
    private app: Application;

    constructor(private port?: string | number) {
        this.app = express()
        this.settings()
        this.middleware()
        this.Routers()
    }
    settings() {
        this.app.set('port', this.port || process.env.PORT || 3000)
    }
    private middleware() {
        this.app.use (express.json())

        this.app.use((req:Request, _res:Response, next:NextFunction) => {
            console.log('Incoming request:', req.headers, req.body);
            next();
        });
    }
    private Routers() {
        this.app.use("/api", AuthRouter)
    }
    async listen() {
        try {
            await schema_base_data()
            console.log('Base data executed successfully')

            await this.app.listen(this.app.get('port'))
            console.log(`Server listen in port: ${this.app.get('port')} `)
        } catch (error: any) {
            console.error(error.message)
        }
    }
}