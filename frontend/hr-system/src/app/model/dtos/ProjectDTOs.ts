import { Project } from "../entities/Project";

export interface GetAllProjectsDto{
    projects: Project[],
    totalPages: number
}