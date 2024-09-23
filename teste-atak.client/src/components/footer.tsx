import { FiGithub } from 'react-icons/fi'
import { Link } from 'react-router-dom'

import { Button } from '@/components/ui/button'

export function Footer() {
  return (
    <footer className="bg-gray-100 dark:bg-gray-900 text-gray-300 py-8">
      <div className="max-w-[90rem] mx-auto px-4">
        <div className="flex flex-col items-center">
          <h2 className="text-3xl font-bold text-foreground mb-4">
            Exportador de Planilhas Excel
          </h2>
          <p className="text-lg mb-4 text-muted-foreground text-center">
            Simplifique a exportação de dados. Envie suas planilhas Excel
            diretamente para seu e-mail!
          </p>
          <div className="flex flex-col md:flex-row md:justify-center gap-4">
            <Link
              to="/"
              className="text-gray-800 hover:text-gray-950 dark:text-gray-300 dark:hover:text-gray-100"
            >
              Início
            </Link>
            <Link
              to="/"
              className="text-gray-800 hover:text-gray-950 dark:text-gray-300 dark:hover:text-gray-100"
            >
              Sobre
            </Link>
            <Link
              to="https://gustavohenrique.vercel.app"
              className="text-gray-800 hover:text-gray-950 dark:text-gray-300 dark:hover:text-gray-100"
            >
              Contato
            </Link>
          </div>
          <div className="mt-6">
            <Button variant="secondary" asChild>
              <a
                href="https://github.com/Gustavohps10/teste-atak"
                target="_blank"
                rel="noreferrer"
              >
                <FiGithub className="mr-2 w-5 h-5" />
                Contribua para o Código
              </a>
            </Button>
          </div>
          <div className="mt-8 text-sm text-gray-500 text-center">
            &copy; {new Date().getFullYear()} QuickExcel. Todos os direitos
            reservados.
            <br /> Made with 💜 by Gustavo Henrique.
          </div>
        </div>
      </div>
    </footer>
  )
}
