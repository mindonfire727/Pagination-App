import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetDocumentsRequest, GetDocumentsResponse } from '../models/document-models';

@Injectable({ providedIn: 'root' })
export class DocumentsService {
  private baseUrl = 'https://localhost:7157/Documents';
  constructor(private httpClient: HttpClient) {}

  all(request: GetDocumentsRequest): Observable<GetDocumentsResponse> {
      return this.httpClient.post<GetDocumentsResponse>(this.baseUrl, request);
  }
}
