import { Router } from "express";
import TaskControlls from "../controllers/task.controllers";

const AuthRouter = Router()
const task = new TaskControlls()

AuthRouter.get("/", task.getTask)
AuthRouter.post("/", task.createTask)

export default AuthRouter;