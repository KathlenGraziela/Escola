using Escola.Entidades;
using Escola.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Repositorios
{
    public class AlunoRepositorio
    {
        private IRepositorio repo;
        public AlunoRepositorio(IRepositorio repo)
        {
            this.repo = repo;
        }

        public int Quantidade()
        {
            return repo.Quantidade();
        }
        public void Salvar(Aluno aluno)
        {
            repo.Salvar(aluno);
        }

        public List<Aluno> Todos()
        {
            return repo.Todos();
        }
    }
}
