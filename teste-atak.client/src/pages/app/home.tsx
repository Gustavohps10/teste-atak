'use client'

import { useQuery } from '@tanstack/react-query'
import { useSearchParams } from 'react-router-dom'
import { z } from 'zod'

import { getCustomers } from '@/api/getCustomers'
import { columns } from '@/components/columns'
import { DataTable } from '@/components/data-table'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'

export function Home() {
  const [searchParams, setSearchParams] = useSearchParams()

  const name = searchParams.get('name')
  const phone = searchParams.get('phone')
  const sortBy = searchParams.get('sortBy') as 'name' | 'phone' | undefined
  const sortDescending = searchParams.get('sortDescending') === 'true'

  const pageIndex = z.coerce
    .number()
    .parse(searchParams.get('pageNumber') ?? '1')

  const { data: result, isPending } = useQuery({
    queryKey: ['customers', pageIndex],
    queryFn: () =>
      getCustomers({
        pageNumber: pageIndex,
        name,
        phone,
        sortBy,
        sortDescending,
      }),
  })

  const handlePageChange = (newPageIndex: number) => {
    setSearchParams(
      (prev) => {
        prev.set('pageNumber', newPageIndex.toString())
        return prev
      },
      { replace: true },
    )
  }

  return (
    <>
      <div className="flex flex-col items-center justify-center p-8 bg-primary max-w-[95vw] min-h-[380px] my-4 mx-auto rounded-lg shadow-lg">
        <h1 className="text-2xl font-bold">Customer List</h1>
      </div>
      <section className="py-4 max-w-[90rem] mx-auto">
        <Card className="shadow-md">
          <CardHeader>
            <CardTitle>Customer Data</CardTitle>
          </CardHeader>
          <CardContent>
            <DataTable
              columns={columns}
              data={result?.items || []}
              totalCount={result?.totalCount || 0}
              pageIndex={pageIndex}
              pageSize={10}
              onPageChange={handlePageChange}
              loading={isPending}
            />
          </CardContent>
        </Card>
      </section>
    </>
  )
}
