using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public List<double> Notas { get; set; }

        public double Media()
        {
            double somaNotas = 0;
            foreach (var nota in this.Notas)
            {
                somaNotas += nota;
            }

            return somaNotas / this.Notas.Count;
        }

        public string Situacao()
        {
            return (this.Media() > 6 ? "Aprovado" : "Reprovado");
        }

        public string NotasFormatada()
        {
            return string.Join(",", this.Notas);
        }

        private static List<Aluno> alunos = new List<Aluno>();
        public static List<Aluno> Todos()
        {
            string caminho = @"C:\GitHub\Stone Tech\alunos.json";
            if(File.Exists(caminho))
            {
                var conteudo = File.ReadAllText(caminho);
                Aluno.alunos = JsonConvert.DeserializeObject<List<Aluno>>(conteudo);
            }
            return Aluno.alunos;
        }

        public static void AddAluno(Aluno aluno)
        {
            Aluno.alunos = Aluno.Todos();
            Aluno.alunos.Add(aluno);
            string caminho = @"C:\GitHub\Stone Tech\alunos.json";
            File.WriteAllText(caminho, JsonConvert.SerializeObject(Aluno.alunos));
        }
    }
}
