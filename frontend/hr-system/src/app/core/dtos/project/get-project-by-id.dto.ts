import { Client } from "../../models/client.model";
import { EmployeeForProjectDto } from "../employee/employee-for-project.dto";

export interface GetProjectByIdDto {
    id: string, 
    title: string,
    description: string,
    client: Client,
    teamLeadId: string,
    working : EmployeeForProjectDto[]
    notWorking : EmployeeForProjectDto[]
}