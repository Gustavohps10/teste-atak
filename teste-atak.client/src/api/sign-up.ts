import { api } from '@/lib/axios'

export type SignUpRequest = {
  email: string
  name: string
  password: string
}

export async function SignUp({ email, name, password }: SignUpRequest) {
  await api.post(`/users`, {
    email,
    name,
    passwordHash: password,
  })
}
