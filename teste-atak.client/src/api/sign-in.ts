import { api } from '@/lib/axios'

export type SignInRequest = {
  email: string
  password: string
}

export type SignInResponse = {
  token: string
  user: {
    name: string
    email: string
  }
}

export async function SignIn({ email, password }: SignInRequest) {
  const { data } = await api.post<SignInResponse>(`/login`, {
    email,
    password,
  })

  return data
}
