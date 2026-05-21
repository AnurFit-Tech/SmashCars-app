import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListAddressesRequest,
  GetAllAddressQueryDto,
  ListAddressesResponse,
  GetByIdAddressQueryDto,
  CreateAddressCommand,
  CreateAddressCommandDto,
  UpdateAddressCommand
} from './address-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({ providedIn: 'root' })
export class AddressesApiService {
  private readonly baseUrl = `${environment.apiUrl}/Address`;
  private http = inject(HttpClient);

  list(p0: { paging: { page: number; pageSize: number; }; }): Observable<GetAllAddressQueryDto[]> {
    return this.http.get<GetAllAddressQueryDto[]>(`${this.baseUrl}/All`);
  }

  deleteById(addressId: number) {
  return this.http.delete<void>(`${this.baseUrl}/Delete`, {
    params: { addressId }
  });
}

create(request: CreateAddressCommand) {
  return this.http.post<CreateAddressCommandDto>(
    `${this.baseUrl}/Create`,
    request
  );
}

update(request: UpdateAddressCommand) {
  return this.http.put<void>(
    `${this.baseUrl}/Update`,
    request
  );
}


  getById(id: number) {
  return this.http.get<GetByIdAddressQueryDto>(`${this.baseUrl}/ById`, {
    params: { addressId: id } // šalje kao ?id=1
  });
}

}
