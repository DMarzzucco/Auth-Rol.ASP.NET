import fs from "fs"
import path from "path"
import { bsData } from "./bsData"

export const schema_base_data = async () =>{
    const bd_path = path.join(__dirname, "sql", "./db.sql")
    const schema = fs.readFileSync(bd_path, "utf8");
    try{
        const response = await bsData.query(schema)
        return response
    }catch(error:any){
        throw new Error (error.message)
    }
}