import { Component, inject, OnInit } from '@angular/core';
import { AddressesApiService } from '../../../api-services/address/address-api.service';  
import { GetAllAddressQueryDto } from '../../../api-services/address/address-api.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-address',
  standalone: false,
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss'],
})
export class AddressComponent implements OnInit {
  private apiService = inject(AddressesApiService);
  public addressesList: GetAllAddressQueryDto[] = [];
  private allAddresses: GetAllAddressQueryDto[]=[];
  public searchAddressId: number | null=null;
  public deleteAddressId: number | null = null;
  public editingAddress: GetAllAddressQueryDto | null = null;


private readonly ADDRESS_DRAFT_KEY = 'address-create-draft';

public showCreateForm = false;

startEdit(address: GetAllAddressQueryDto): void {
  this.editingAddress = { ...address }; // clone
  this.showCreateForm = false;
}

  public search = {
    street: '',
    city: '',
    country: '',
    postalCode: '',
    additionalInfo: ''
  }

  public newAddress = {
  userID: 0,
  street: '',
  city: '',
  country: '',
  postalCode: '',
  additionalInfo: ''
};


  ngOnInit(): void {
    const draft = localStorage.getItem(this.ADDRESS_DRAFT_KEY);
  if (draft) {
    this.newAddress = JSON.parse(draft);
  }
  this.loadAll();
}

 loadAll(): void {
    this.apiService.list({
      paging: {
        page: 1,
        pageSize: 1000
      }
    }).subscribe({
      next: res => {
        this.addressesList = res;
        this.allAddresses = res;
      },
      error: () => {
        this.addressesList = [];
        this.allAddresses = [];
      }
    });
  }

createAddress(): void {
  if (!this.newAddress.userID || !this.newAddress.street) {
    alert('UserID i Street su obavezni');
    return;
  }

  this.apiService.create(this.newAddress).subscribe({
    next: res => {
      console.log('Created address ID:', res.addressID);

      // reload liste
      this.loadAll();

      localStorage.removeItem(this.ADDRESS_DRAFT_KEY);

      // reset forme
      this.newAddress = {
        userID: 0,
        street: '',
        city: '',
        country: '',
        postalCode: '',
        additionalInfo: ''
      };
       this.showCreateForm = false;
    },
    error: err => {
      console.error(err);
      alert('Greška pri kreiranju adrese');
    }
  });
  this.showCreateForm = false;
}

updateAddress(): void {
  if (!this.editingAddress) return;

  this.apiService.update(this.editingAddress).subscribe({
    next: () => {
      this.loadAll();
      this.editingAddress = null;
    },
    error: err => {
      console.error(err);
      alert('Greška pri update adrese');
    }
  });
}


searchById(): void {

    if (this.searchAddressId) {
      this.apiService.getById(this.searchAddressId).subscribe({
        next: res => {
          this.addressesList = [res];
        },
        error: () => {
          this.addressesList = [];
        }
      });
      return;
    }

    this.addressesList = this.allAddresses.filter(a =>
      (!this.search.street || a.street?.toLowerCase().includes(this.search.street.toLowerCase())) &&
      (!this.search.city || a.city?.toLowerCase().includes(this.search.city.toLowerCase())) &&
      (!this.search.country || a.country?.toLowerCase().includes(this.search.country.toLowerCase())) &&
      (!this.search.postalCode || a.postalCode?.toString().includes(this.search.postalCode)) &&
      (!this.search.additionalInfo || a.additionalInfo?.toLowerCase().includes(this.search.additionalInfo.toLowerCase()))
    );
  }

  deleteById(): void {
  if (!this.deleteAddressId) {
    alert('Unesi Address ID za brisanje');
    return;
  }

  if (!confirm(`Obrisati adresu ID ${this.deleteAddressId}?`)) {
    return;
  }

  this.apiService.deleteById(this.deleteAddressId).subscribe({
    next: () => {
      this.allAddresses = this.allAddresses.filter(
        a => a.addressID !== this.deleteAddressId
      );

      this.addressesList = this.addressesList.filter(
        a => a.addressID !== this.deleteAddressId
      );

      this.deleteAddressId = null;
    },
    error: err => {
      console.error(err);
      alert('Greška pri brisanju adrese');
    }
  });
}


resetSearch(): void {
    this.searchAddressId = null;

    this.search = {
      street: '',
      city: '',
      country: '',
      postalCode: '',
      additionalInfo: ''
    };

    this.addressesList = [...this.allAddresses];
  }

sortColumn: keyof GetAllAddressQueryDto | null = null;
sortDirection: 'asc' | 'desc' = 'asc';

sort(column: keyof GetAllAddressQueryDto): void {
  if (this.sortColumn === column) {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  } else {
    this.sortColumn = column;
    this.sortDirection = 'asc';
  }

  this.addressesList = [...this.addressesList].sort((a, b) => {
    const valueA = a[column] as any;
    const valueB = b[column] as any;

    if (valueA == null) return 1;
    if (valueB == null) return -1;

    if (typeof valueA === 'number' && typeof valueB === 'number') {
      return this.sortDirection === 'asc'
        ? valueA - valueB
        : valueB - valueA;
    }

    return this.sortDirection === 'asc'
      ? valueA.toString().localeCompare(valueB.toString())
      : valueB.toString().localeCompare(valueA.toString());
  });
}

autoSave(): void {
  localStorage.setItem(
    this.ADDRESS_DRAFT_KEY,
    JSON.stringify(this.newAddress)
  );
}

toggleCreateForm(): void {
  this.showCreateForm = !this.showCreateForm;

  // Ako otvaramo formu, pokušaj učitati draft iz localStorage
  if (this.showCreateForm) {
    const draft = localStorage.getItem(this.ADDRESS_DRAFT_KEY);
    if (draft) {
      this.newAddress = JSON.parse(draft);
    }
  }
}

// Hardkodirane opcije za Autocomplete
public cities: string[] = ['Sarajevo', 'Split', 'Zagreb', 'Belgrade', 'Mostar'];
public countries: string[] = ['Bosnia', 'Croatia', 'Serbia', 'Slovenia'];

// Filteri koji će se koristiti za prikaz u inputu
public filteredCities: string[] = [...this.cities];
public filteredCountries: string[] = [...this.countries];
filterCities(): void {
  const value = this.newAddress.city.toLowerCase();
  this.filteredCities = this.cities.filter(c => c.toLowerCase().includes(value));
}

filterCountries(): void {
  const value = this.newAddress.country.toLowerCase();
  this.filteredCountries = this.countries.filter(c => c.toLowerCase().includes(value));
}


}
