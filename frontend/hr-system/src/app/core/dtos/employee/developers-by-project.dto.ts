import { Employee } from "../../models/employee.model";

export interface DevelopersByProject{
    working : Employee[],
    notWorking : Employee[]
}