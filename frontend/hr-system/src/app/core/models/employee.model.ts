
export interface Employee{
    id: string, 
    name: string,
    surname: string,
    daysOff: number,
    role: EmployeeRole,
    accountId: string
}

export enum EmployeeRole{ 
    Developer, 
    Manager
}
