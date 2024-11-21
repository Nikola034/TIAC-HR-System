import { Employee } from "../../models/employee.model";

export interface AllEmployeesDto{
    employees : Employee[],
    totalPages: number,
    page: number,
    itemsPerPage : number,
}