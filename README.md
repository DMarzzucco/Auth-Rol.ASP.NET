## Discription

This repository contains a SQL database request manager for Express applications using TypeScript. The purpose of this manager is to provide greater flexibility and scalability in the handling of CRUD operations within the application, allowing cleaner and more organized management of interactions with the database.

##Service
The queries manager is located in the ``./service`` folder, there you will find the file ``handler_queries.ts``.

## ORM
The example found in the file is using requests with the SQL language, but if you want you can use an ORM, such as Prisma.

````TS
    async get(): Promise<Proms[]> {
        try {
            const result = await prisma.base_data.findMany();
            return result;
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
    async post(data: ProducIt): Promise<Proms> {
        try {
            const result = await prisma.base_date.create({ data: data });
            return result;
        } catch (error: any) {
            throw new Error(error.message)
        }
    }
````

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
