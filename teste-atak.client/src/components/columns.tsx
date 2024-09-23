import { CheckIcon, MinusIcon } from '@radix-ui/react-icons'
import { ColumnDef } from '@tanstack/react-table'

import { Customer } from '@/@types/Customer'
import { Checkbox } from '@/components/ui/checkbox'

export const columns: ColumnDef<Customer>[] = [
  {
    id: 'select',
    header: ({ table }) => {
      const isAllSelected = table.getIsAllRowsSelected()
      const isSomeSelected = table.getIsSomeRowsSelected()

      return (
        <Checkbox
          checked={isAllSelected}
          onCheckedChange={(checked) => {
            table.getRowModel().rows.forEach((row) => {
              row.toggleSelected(!!checked)
            })
          }}
        >
          {isSomeSelected && !isAllSelected ? <MinusIcon /> : <CheckIcon />}
        </Checkbox>
      )
    },
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(checked) => row.toggleSelected(!!checked)}
      />
    ),
  },
  {
    accessorKey: 'id',
    header: 'ID',
    cell: ({ row }) => (
      <div
        className="font-mono tracking-tight"
        style={{
          maxWidth: '300px',
          overflow: 'hidden',
          textOverflow: 'ellipsis',
          whiteSpace: 'nowrap',
        }}
      >
        {row.getValue('id')}
      </div>
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
    cell: ({ row }) => (
      <div className="font-mono truncate" style={{ maxWidth: '150px' }}>
        {row.getValue('phone')}
      </div>
    ),
  },
]
