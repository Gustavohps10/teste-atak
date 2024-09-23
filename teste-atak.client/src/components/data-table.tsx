'use client'

import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from '@tanstack/react-table'

import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'
import { Skeleton } from '@/components/ui/skeleton'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[]
  data: TData[]
  totalCount: number
  pageIndex: number
  pageSize: number
  onPageChange: (newPageIndex: number) => void
  loading: boolean // Adiciona a prop loading
}

export function DataTable<TData, TValue>({
  columns,
  data,
  totalCount,
  pageIndex,
  pageSize,
  onPageChange,
  loading, // Recebe a prop loading
}: DataTableProps<TData, TValue>) {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
  })

  const pageCount = Math.ceil(totalCount / pageSize)

  const getPageNumbers = () => {
    const pages = new Set<number>()
    if (pageCount <= 4) {
      for (let i = 1; i <= pageCount; i++) {
        pages.add(i)
      }
    } else {
      pages.add(1)
      pages.add(pageCount)
      pages.add(pageIndex + 1)
      if (pageIndex > 1) pages.add(pageIndex)
      if (pageIndex < pageCount) pages.add(pageIndex + 2)
      if (pageIndex > 1) pages.add(pageIndex - 1)
      if (pageIndex < pageCount - 1) pages.add(pageIndex + 3)
    }
    return Array.from(pages)
      .sort((a, b) => a - b)
      .filter((page) => page > 0 && page <= pageCount)
  }

  const pageNumbers = getPageNumbers()

  return (
    <div className="rounded-md border">
      <Table>
        <TableHeader>
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableHead key={header.id}>
                  {header.isPlaceholder
                    ? null
                    : flexRender(
                        header.column.columnDef.header,
                        header.getContext(),
                      )}
                </TableHead>
              ))}
            </TableRow>
          ))}
        </TableHeader>
        <TableBody>
          {loading ? (
            Array.from({ length: 10 }).map((_, index) => (
              <TableRow key={index}>
                {columns.map((column) => (
                  <TableCell key={column.id}>
                    <Skeleton className="h-2 w-[90%]" />
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : table.getRowModel().rows.length ? (
            table.getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={columns.length} className="h-24 text-center">
                Sem resultados.
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>

      <Pagination>
        <PaginationContent className="flex justify-between items-center p-4">
          <PaginationItem>
            <PaginationPrevious
              onClick={() => {
                if (pageIndex > 1) {
                  onPageChange(pageIndex - 1)
                }
              }}
              className={`${
                pageIndex <= 1
                  ? 'opacity-50 cursor-not-allowed'
                  : 'cursor-pointer'
              }`}
            >
              Anterior
            </PaginationPrevious>
          </PaginationItem>
          {pageNumbers.map((page) => (
            <PaginationItem key={page}>
              <PaginationLink
                onClick={(e) => {
                  e.preventDefault()
                  if (page !== pageIndex) {
                    onPageChange(page)
                  }
                }}
                isActive={pageIndex === page}
                className="cursor-pointer"
              >
                {page}
              </PaginationLink>
            </PaginationItem>
          ))}
          <PaginationItem>
            <PaginationNext
              onClick={() => {
                if (pageIndex < pageCount) {
                  onPageChange(pageIndex + 1)
                }
              }}
              className={`${
                pageIndex >= pageCount
                  ? 'opacity-50 cursor-not-allowed'
                  : 'cursor-pointer'
              }`}
            />
          </PaginationItem>
        </PaginationContent>
      </Pagination>
    </div>
  )
}
