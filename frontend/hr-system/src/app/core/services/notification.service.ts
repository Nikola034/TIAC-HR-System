import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://your-backend-url/notificationHub')
      .build();
  }

  startConnection() {
    this.hubConnection.start().catch(err => console.error(err));
  }

  onNotification(callback: (message: string) => void) {
    this.hubConnection.on('ReceiveNotification', callback);
  }
}
