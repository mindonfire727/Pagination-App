export interface DocumentDto {
  id: string;
  number: string;
  createdAt: string;
  author: string;
  type: number;
}

export class GetDocumentsRequest {
  public searchQuery;;

  public take: Take;

  public currentPage: number;

  constructor() {
    this.searchQuery = '';
    this.take = new Take();
    this.currentPage = 1;    
  }
}

class Take {
  amount = 10;
}

export class GetDocumentsResponse {
  public paginationList: PaginatedList<DocumentDto> = new PaginatedList();
}
export class PaginatedList<T> {
  public totalCount = 0;

  public currentPage = 1;

  public totalPages = 0;

  public items: Items = [];
}

type Items = DocumentDto[];


export interface NavigationButton { 
  value: number, 
  clicked: boolean
} 