import { Client } from "../../models/client.model";

export interface ClientWithNumberOfProjects{
    client: Client
    numberOfProjects : number
}