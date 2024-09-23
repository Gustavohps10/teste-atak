import { Customer } from '@/@types/Customer'
import { api } from '@/lib/axios'

export interface GetCustomersQuery {
  pageNumber?: number
  name?: string | null
  phone?: string | null
  sortBy?: 'name' | 'phone'
  sortDescending?: boolean
}

export interface GetCustomersResponse {
  items: Customer[]
  pageIndex: number
  totalPages: number
  totalCount: number
  hasPreviousPage: boolean
  hasNextPage: boolean
}

export async function getCustomers({
  pageNumber,
  name,
  phone,
  sortBy,
  sortDescending = false,
}: GetCustomersQuery) {
  const response = await api.get<GetCustomersResponse>('/customers', {
    params: {
      pageNumber,
      name,
      phone,
      sortBy,
      sortDescending,
    },
  })

  return response.data
}
