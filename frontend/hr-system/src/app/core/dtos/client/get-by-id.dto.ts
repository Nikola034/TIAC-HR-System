import { Project } from "../../models/project.model"

export interface ClientByIdDto{
    id: string,
    name: string
    country: string
    projects: Project[]
}