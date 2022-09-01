using Escola.Entidades;
using Escola.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.Repositorios
{
    public class AlunoRepositorioJson : IRepositorio
    {
        private string caminhoJson()
        {
            return System.Configuration.ConfigurationManager.AppSettings["conexao_json"];
        }

        public int Quantidade()
        {
            return this.Todos().Count;
        }
        public List<Aluno> Todos()
        {
            var alunos = new List<Aluno>();
            if (File.Exists(caminhoJson()))
            {
                var conteudo = File.ReadAllText(caminhoJson());
                alunos = JsonConvert.DeserializeObject<List<Aluno>>(conteudo);
            }

            return alunos;
        }

        public void Salvar(Aluno aluno)
        {
            var alunos = Todos();
            alunos.Add(aluno);
            File.WriteAllText(caminhoJson(), JsonConvert.SerializeObject(alunos));
        }
    }
}
