import { Injectable } from '@angular/core';
import { NgModule } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
@Injectable({
  providedIn: 'root'
})
export class CustomPaginatorIntlService extends MatPaginatorIntl {

  constructor() {
    super();
  }
 override itemsPerPageLabel = 'Items per page:';
 override nextPageLabel = 'Next page';
 override previousPageLabel = 'Previous page';
}
