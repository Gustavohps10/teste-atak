import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

import { Loader } from './loader'

interface ConfirmationDialogProps {
  countIds: number
  open: boolean
  onClose: () => void
  onConfirm: () => void
  isLoading: boolean
}

export const ConfirmationDialog: React.FC<ConfirmationDialogProps> = ({
  countIds,
  open,
  onClose,
  onConfirm,
  isLoading,
}) => {
  return (
    <Dialog open={open} onOpenChange={onClose}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Confirmação</DialogTitle>
          <DialogDescription>
            Você tem certeza que deseja gerar o arquivo Excel com
            <b> {countIds} registros</b> e envia-lo para seu email?
          </DialogDescription>
        </DialogHeader>
        <div className="flex justify-end mt-4">
          <Button variant="outline" onClick={onClose} className="mr-2">
            Cancelar
          </Button>
          <Button onClick={onConfirm} disabled={isLoading}>
            {isLoading ? <Loader /> : 'Confirmar'}
          </Button>
        </div>
        {isLoading && (
          <span className="text-center font-semibold my-4">
            ❗ Isso pode demorar alguns segundos ¯\_(ツ)_/¯
          </span>
        )}
      </DialogContent>
    </Dialog>
  )
}
