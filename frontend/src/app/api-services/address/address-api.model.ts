import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export class ListAddressesRequest extends BasePagedQuery {
  search?: string | null;
}

export interface GetAllAddressQueryDto {
  addressID: number;
  userID: number; 
  street: string;
  city?: string;
  country?: string;
  postalCode?: string;
  additionalInfo?: string | null;
}

export interface GetByIdAddressQueryDto {addressID: number;
  userID: number; 
  street: string;
  city?: string;
  country?: string;
  postalCode?: string;
  additionalInfo?: string | null;
}

export interface CreateAddressCommand {
  userID: number;
  street: string;
  city?: string;
  country?: string;
  postalCode?: string;
  additionalInfo?: string | null;
}

export interface UpdateAddressCommand {
  addressID: number;
  userID: number;
  street: string;
  city?: string;
  country?: string;
  postalCode?: string;
  additionalInfo?: string | null;
}


export interface CreateAddressCommandDto {
  addressID: number;
}


export type ListAddressesResponse = PageResult<GetAllAddressQueryDto>;
