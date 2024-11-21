import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { JwtService } from './jwt.service';
import { AlertService } from './alert.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  
  private hubConnection: signalR.HubConnection;

  constructor(private jwtService: JwtService) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://employee-service:8082/notificationHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this.jwtService.getToken() || ''
      })
      .build();
  }

  startConnection() {
    this.hubConnection.start().catch(err => console.error(err));
  }

  onNotification(callback: (message: string) => void) {
    this.hubConnection.on('ReceiveNotification', callback);
  }
}
