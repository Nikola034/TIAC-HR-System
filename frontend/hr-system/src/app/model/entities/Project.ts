import { Client } from "./Client";

export interface Project{
    id: string, 
    title: string,
    description: string,
    teamLeadId: string,
    client: Client
}