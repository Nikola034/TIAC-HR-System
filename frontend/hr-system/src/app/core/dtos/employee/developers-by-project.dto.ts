import { EmployeeForProjectDto } from "./employee-for-project.dto";

export interface DevelopersByProject{
    working : EmployeeForProjectDto[],
    notWorking : EmployeeForProjectDto[]
}