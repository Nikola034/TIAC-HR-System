import { Project } from "../../models/project.model";

export interface GetAllProjectsDto{
    projects: Project[],
    totalPages: number,
    page: number,
    itemsPerPage : number,
}
