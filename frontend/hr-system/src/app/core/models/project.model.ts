import { Client } from "./client.model";

export interface Project{
    id: string, 
    title: string,
    description: string,
    teamLeadId: string,
    client: Client
}
