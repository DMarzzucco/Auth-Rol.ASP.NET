## Discription

This repository contains a SQL database request manager for Express applications using TypeScript. The purpose of this manager is to provide greater flexibility and scalability in the handling of CRUD operations within the application, allowing cleaner and more organized management of interactions with the database.

## Examples

```TS
/* get operation */
    public async  getTask(_req: Request, res: Response) {
        const result = await db.get()
        return res.status(200).json(result)
    }
/* post operation */
  public async createTask(req: Request, res: Response) {
         const object: ProducIt = req.body;
         const result = await db.post(object)
         return res.status(200).json(result)
    }
```

## Author

Made by Dario Marzzucco (@darmarzz)