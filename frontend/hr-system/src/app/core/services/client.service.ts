import { Injectable } from '@angular/core';
import { GetAllClientsDto } from '../dtos/client/get-all-clients.dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Client } from '../models/client.model';
import { CreateClientDto } from '../dtos/client/create-client.dto';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private http: HttpClient) { }

  baseUrl = `${environment.apiUrl}/projects/clients/`;

  getAllClients(query : string): Observable<GetAllClientsDto> {
    return this.http.get<GetAllClientsDto>(`${this.baseUrl}${query}`);
  }

  createClient(dto : CreateClientDto): Observable<Client> {
    return this.http.post<Client>(`${this.baseUrl}`,dto);
  }

  updateClient(client : Client): Observable<Client> {
    return this.http.put<Client>(`${this.baseUrl}`,client);
  }

  deleteClient(id : string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}${id}`);
  }
}
