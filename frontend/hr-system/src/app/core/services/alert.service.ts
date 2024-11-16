import { Injectable } from '@angular/core';
import  Swal  from 'sweetalert2'

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() { }

  fireSwalError(message : string, title : string = 'Error') : void {
    Swal.fire({
      icon: 'error',
      title: title,
      text: message,
      showConfirmButton: false,
      position: 'bottom-right',
      timer: 3000,
      timerProgressBar: true,
      backdrop: 'none',
      width: 300,
      background: '#c62828',
      color: 'white',
    });
  }

  fireSwalSuccess(message : string, title : string = 'Success') : void {
    Swal.fire({
      icon: 'success',
      title: title,
      text: message,
      showConfirmButton: false,
      position: 'bottom-right',
      timer: 3000,
      timerProgressBar: true,
      backdrop: 'none',
      width: 300,
      background: '#c62828',
      color: 'white',
    });
  }
}
