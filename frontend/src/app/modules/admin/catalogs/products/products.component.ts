// products.component.ts

import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  ListProductsRequest,
  ListProductsQueryDto,
  UpdateInline
} from '../../../../api-services/products/products-api.models';
import { ProductsApiService } from '../../../../api-services/products/products-api.service';
import { BaseListPagedComponent } from '../../../../core/components/base-classes/base-list-paged-component';
import { ToasterService } from '../../../../core/services/toaster.service';
import { DialogHelperService } from '../../../shared/services/dialog-helper.service';
import { DialogButton } from '../../../shared/models/dialog-config.model';

@Component({
  selector: 'app-products',
  standalone: false,
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent
  extends BaseListPagedComponent<ListProductsQueryDto, ListProductsRequest>
  implements OnInit {

  private api = inject(ProductsApiService);
  private router = inject(Router);
  private toaster = inject(ToasterService);
  private dialogHelper = inject(DialogHelperService);

  displayedColumns: string[] = [
    'name',
    'categoryName',
    'price',
    'stockQuantity',
    'isEnabled',
    'actions'
  ];

  constructor() {
    super();
    this.request = new ListProductsRequest();
  }

  ngOnInit(): void {
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Failed to load products');
        console.error('Load products error:', err);
      }
    });
  }

  // === UI Actions ===

  onCreate(): void {
    this.router.navigate(['/admin/products/add']);
  }

  onEdit(product: ListProductsQueryDto): void {
    this.router.navigate(['/admin/products', product.id, 'edit']);
  }

  onDelete(product: ListProductsQueryDto): void {
    this.dialogHelper.product.confirmDelete(product.name).subscribe(result => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(product);
      }
    });
  }

  private performDelete(product: ListProductsQueryDto): void {
    this.startLoading();

    this.api.delete(product.id).subscribe({
      next: () => {
        this.dialogHelper.product.showDeleteSuccess().subscribe();
        this.loadPagedData();
      },
      error: (err) => {
        this.stopLoading();

        this.dialogHelper.showError(
          'DIALOGS.TITLES.ERROR',
          'PRODUCTS.DIALOGS.ERROR_DELETE'
        ).subscribe();

        console.error('Delete product error:', err);
      }
    });
  }

  onSearch(): void {
    this.request.paging.page = 1;
    this.loadPagedData();
  }

public editRow: number | null = null;

// Cache za promjene dok se ne sačuvaju
public editCache: { name: string; price: number } = { name: '', price: 0 };

  startEdit(product: ListProductsQueryDto): void {
  this.editRow = product.id;
  this.editCache = {
    name: product.name,
    price: product.price,
  };
}

cancelEdit(): void {
  this.editRow = null;
  this.editCache = { name: '', price: 0 };
}

saveEdit(product: ListProductsQueryDto): void {
  if (!this.editRow) return;

  const payload: UpdateInline = {
    name: this.editCache.name.trim(),
    price: this.editCache.price,
  };

  this.api.updateInline(product.id, payload).subscribe({ // <-- ovdje je api, a ne apiService
    next: () => {
      product.name = payload.name;
      product.price = payload.price;

      this.cancelEdit();
    },
    error: err => {
      console.error(err);
      alert('Greška pri spremanju promjena.');
    }
  });
}



}
