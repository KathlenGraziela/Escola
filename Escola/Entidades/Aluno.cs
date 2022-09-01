using System.Collections.Generic;


namespace Escola.Entidades
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public List<double> Notas { get; set; }

        public double Media()
        {
            double somaNotas = 0;
            foreach (var nota in Notas)
            {
                somaNotas += nota;
            }

            return somaNotas / Notas.Count;
        }

        public string Situacao()
        {
            return Media() > 6 ? "Aprovado" : "Reprovado";
        }

        public string NotasFormatada()
        {
            return string.Join(",", Notas);
        }
    }
}
