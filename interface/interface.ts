export interface Date {
    readonly id: number;
    readonly title: string;
    readonly image: string;
    readonly description: string;
    readonly create_time: Date
    readonly update_time: Date
}
export interface CreateData extends Omit<Date, "id" | "create_time" | "update_time"> { }

export interface UpdateData extends Partial<CreateData> { }