import { ClientWithNumberOfProjects } from "./client-with-number-of-projects.dto";

export interface GetAllClientsDto{
    clients: ClientWithNumberOfProjects[],
    totalPages: number,
    page: number,
    itemsPerPage : number,
}