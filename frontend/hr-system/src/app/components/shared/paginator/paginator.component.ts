import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrl: './paginator.component.css'
})
export class PaginatorComponent {

  @Input({required: true}) pageNumber !: number;
  @Input({required: true}) totalPages !: number;

  @Output() loadNewPageEmmiter = new EventEmitter<number>();

  loadNewPage(page:number){
    this.loadNewPageEmmiter.emit(page);
  }

  generatePagination(currentPage: number, totalPages: number): number[] {
    const maxVisiblePages = 5; // Number of page links to display at a time
    const paginationNumbers: number[] = [];
  
    let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = startPage + maxVisiblePages - 1;
  
    if (endPage > totalPages) {
      endPage = totalPages;
      startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }
  
    for (let i = startPage; i <= endPage; i++) {
      paginationNumbers.push(i);
    }
  
    return paginationNumbers;
  }
}
