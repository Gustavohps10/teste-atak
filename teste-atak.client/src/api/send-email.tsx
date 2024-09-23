import { api } from '@/lib/axios'

export async function SendEmail(ids: string[]) {
  await api.post(`/email/send`, ids)
}
