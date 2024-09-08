## Discription

Template to handle query operations for Express, using TypeScript. Making use of SOLID design principles. The code is organized into separate classes and modules, following the single responsibility principle to ensure that each component has a single, well-defined responsibility.

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
