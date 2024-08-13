import express, { Application } from "express";
import { schema_base_data } from "./db/schema"
import { AuthRouter } from "./routes"

class App {
    private app: Application;

    constructor(private port?: string | number) {
        this.app = express()
        this.settings()
        this.Routers()
        this.middleware()
    }
    settings() {
        this.app.set('port', this.port || process.env.PORT || 3000)
    }
    private middleware() {
        this.app.use(express.json())
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
async function Main() {
    const app = new App;
    app.listen()
}
Main().catch(err => {
    console.error(`Error to start the server ${err}`)
    process.exit(1)
})