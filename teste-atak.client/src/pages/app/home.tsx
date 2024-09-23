'use client'

import { useMutation, useQuery } from '@tanstack/react-query'
import { useState } from 'react'
import { useSearchParams } from 'react-router-dom'
import { z } from 'zod'

import { getCustomers } from '@/api/getCustomers'
import { SendEmail } from '@/api/send-email'
import { columns } from '@/components/columns'
import { ConfirmationDialog } from '@/components/confirmation-dialog'
import { DataTable } from '@/components/data-table'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { toast } from '@/hooks/use-toast'
import { queryClient } from '@/lib/react-query'

export function Home() {
  const [searchParams, setSearchParams] = useSearchParams()
  const [dialogOpen, setDialogOpen] = useState(false)
  const [selectedIds, setSelectedIds] = useState<string[]>([])

  console.log(selectedIds)

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

  const { mutateAsync: sendEmailFn, isPending: isSending } = useMutation({
    mutationFn: (ids: string[]) => SendEmail(ids),
    onSuccess: () => {
      toast({
        title: 'E-mail enviado!',
        description: 'O e-mail com Excel em anexo foi enviado com sucesso.',
      })
      queryClient.invalidateQueries({ queryKey: ['customers'] })
    },
    onError: () => {
      toast({
        title: 'Erro ao enviar',
        description: 'Houve um problema ao enviar o e-mail.',
        variant: 'destructive',
      })
    },
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

  const handleSendEmail = async () => {
    if (selectedIds.length < 10 || selectedIds.length > 1000) {
      toast({
        title: 'Seleção inválida',
        description: 'Selecione entre 10 e 1000 registros para enviar.',
        variant: 'destructive',
      })
      return
    }
    await sendEmailFn(selectedIds)
    setDialogOpen(false)
  }

  return (
    <>
      <div className="flex flex-col items-center justify-center p-8 bg-primary max-w-[95vw] min-h-[380px] my-4 mx-auto rounded-lg shadow-lg">
        <h1 className="text-2xl font-bold">Customer List</h1>
      </div>
      <section className="py-4 max-w-[90rem] mx-auto">
        <Card className="shadow-md">
          <CardHeader>
            <CardTitle>Dados de Clientes</CardTitle>
          </CardHeader>
          <CardContent>
            <Button className="my-4" onClick={() => setDialogOpen(true)}>
              Enviar E-mail
            </Button>
            <DataTable
              columns={columns({ selectedIds, setSelectedIds })}
              data={result?.items || []}
              totalCount={result?.totalCount || 0}
              pageIndex={pageIndex}
              pageSize={10}
              onPageChange={handlePageChange}
              loading={isPending}
            />
          </CardContent>
        </Card>
        <ConfirmationDialog
          open={dialogOpen}
          onClose={() => setDialogOpen(false)}
          onConfirm={handleSendEmail}
          isLoading={isSending}
          countIds={selectedIds.length}
        />
      </section>
    </>
  )
}
