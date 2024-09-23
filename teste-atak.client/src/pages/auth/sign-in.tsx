import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation } from '@tanstack/react-query'
import { AxiosError } from 'axios'
import { useEffect } from 'react'
import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { z } from 'zod'

import { SignIn as UserSignIn, SignInRequest } from '@/api/sign-in'
import { Loader } from '@/components/loader'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { toast } from '@/hooks/use-toast'
import { useAuth } from '@/hooks/useAuth'

const formSchema = z.object({
  email: z.string().email({ message: 'Email inválido.' }),
  password: z
    .string()
    .min(8, { message: 'A senha deve ter pelo menos 8 caracteres.' }),
})

type FormValues = z.infer<typeof formSchema>

export function SignIn() {
  const { isAuthenticated, login } = useAuth()
  const navigate = useNavigate()

  useEffect(() => {
    if (isAuthenticated) {
      navigate('/')
    }
  }, [isAuthenticated, navigate])

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>({
    resolver: zodResolver(formSchema),
  })

  const { mutate, isPending } = useMutation({
    mutationFn: (data: SignInRequest) => UserSignIn(data),
    onSuccess: (data) => {
      toast({
        title: 'Login bem-sucedido!',
        description: 'Você foi logado com sucesso.',
      })
      login(data.token)
      navigate('/') // Redireciona após o login
    },
    onError: (error: AxiosError) => {
      const statusCode = error.response?.status
      const description =
        statusCode === 404 ? 'Usuário não encontrado' : 'Falha no login'

      toast({
        title: 'Ooops!',
        description,
        variant: 'destructive',
      })
    },
  })

  const onSubmit = (data: FormValues) => {
    mutate(data)
  }

  return (
    <div className="flex items-center justify-center my-36">
      <Card className="max-w-sm w-full p-6 rounded-lg">
        <CardContent>
          <CardTitle className="text-2xl font-bold">Entrar</CardTitle>
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 my-4">
            <div>
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                type="email"
                placeholder="Digite seu email"
                className="mt-1"
                {...register('email')}
              />
              {errors.email && (
                <p className="text-red-500 text-sm">{errors.email.message}</p>
              )}
            </div>
            <div>
              <Label htmlFor="password">Senha</Label>
              <Input
                id="password"
                type="password"
                placeholder="Digite sua senha"
                className="mt-1"
                {...register('password')}
              />
              {errors.password && (
                <p className="text-red-500 text-sm">
                  {errors.password.message}
                </p>
              )}
            </div>
            <Button type="submit" className="w-full">
              {isPending ? <Loader /> : 'Entrar'}
            </Button>
          </form>
          <span className="mt-6 font-xs">
            Não possui uma conta?{' '}
            <Button variant="link" className="p-0" asChild>
              <Link to="/sign-up">Cadastre-se</Link>
            </Button>
          </span>
        </CardContent>
      </Card>
    </div>
  )
}
