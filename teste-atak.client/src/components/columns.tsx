import { CheckIcon, MinusIcon } from '@radix-ui/react-icons'
import { ColumnDef } from '@tanstack/react-table'

import { Customer } from '@/@types/Customer'
import { Checkbox } from '@/components/ui/checkbox'

interface ColumnProps {
  selectedIds: string[]
  setSelectedIds: React.Dispatch<React.SetStateAction<string[]>>
}

export const columns = ({
  selectedIds,
  setSelectedIds,
}: ColumnProps): ColumnDef<Customer>[] => [
  {
    id: 'select',
    header: ({ table }) => {
      const isAllSelected = table.getIsAllRowsSelected()
      const isSomeSelected = table.getIsSomeRowsSelected()

      return (
        <Checkbox
          checked={isAllSelected}
          onCheckedChange={(checked) => {
            const newSelectedIds = checked
              ? table
                  .getRowModel()
                  .rows.map((row) => row.getValue('id') as string)
              : []
            setSelectedIds(newSelectedIds)
            table
              .getRowModel()
              .rows.forEach((row) => row.toggleSelected(!!checked))
          }}
        >
          {isSomeSelected && !isAllSelected ? <MinusIcon /> : <CheckIcon />}
        </Checkbox>
      )
    },
    cell: ({ row }) => {
      const id = row.getValue('id') as string
      const isSelected = selectedIds.includes(id)

      return (
        <Checkbox
          checked={isSelected}
          onCheckedChange={(checked) => {
            row.toggleSelected(!!checked)
            setSelectedIds((prevIds) => {
              if (checked) {
                return [...new Set([...prevIds, id])]
              } else {
                return prevIds.filter((prevId) => prevId !== id)
              }
            })
          }}
        />
      )
    },
  },
  {
    accessorKey: 'id',
    header: 'ID',
    cell: ({ row }) => (
      <div className="font-mono tracking-tight">{row.getValue('id')}</div>
    ),
  },
  {
    accessorKey: 'imageUrl',
    header: () => <div className="flex justify-center">Imagem</div>,
    cell: ({ row }) => (
      <div className="flex justify-center">
        <img
          src={row.getValue('imageUrl')}
          alt={row.getValue('name')}
          className="h-10 w-10 rounded-md"
        />
      </div>
    ),
    size: 50,
  },
  {
    accessorKey: 'name',
    header: 'Nome',
  },
  {
    accessorKey: 'email',
    header: 'Email',
  },
  {
    accessorKey: 'phone',
    header: 'Telefone',
  },
]
