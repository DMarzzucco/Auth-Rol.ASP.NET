import { Router } from "express";
import TaskControlls from "../controllers/task.controllers";

const AuthRouter = Router()
const task = new TaskControlls()

AuthRouter.get("/task", task.getTask)
AuthRouter.post("/task", task.createTask)

export default AuthRouter;