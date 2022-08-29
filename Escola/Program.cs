using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Escola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Por favor digite uma das opções abaixo: ");
                Console.WriteLine("1 - Cadastrar aluno");
                Console.WriteLine("2 - Listar alunos");
                Console.WriteLine("3 - Sair");
                int opcao = 0;
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        cadastrarAluno();
                        break;
                    case 2:
                        listarAlunos();
                        break;
                    case 3:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }


        private static void listarAlunos()
        {
            Console.Clear();

            if (Aluno.Todos().Count == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado!");
                Thread.Sleep(2000);
                return;
            }

            foreach (var aluno in Aluno.Todos())
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Nome: " + aluno.Nome);
                Console.WriteLine("Matrícula: " + aluno.Matricula);
                Console.WriteLine("Notas: " + aluno.NotasFormatada());
                Console.WriteLine("Média: " + aluno.Media().ToString("#.##"));
                Console.WriteLine("Situação: " + aluno.Situacao());
                Console.WriteLine("------------------------");
            }

            Thread.Sleep(5000);
        }

        private static void cadastrarAluno()
        {
            var aluno = new Aluno();
            Console.Clear();
            Console.WriteLine("Digite o nome do aluno");
            aluno.Nome = Console.ReadLine();

            Console.WriteLine("Digite a matrícula do aluno");
            aluno.Matricula = Console.ReadLine();

            Console.WriteLine("Digite as notas do aluno, separadas por vírgola");
            var sNotas = Console.ReadLine();

            var sArrayNotas = sNotas.Split(',');
            var listaNotas = new List<double>();
            foreach (var sNota in sArrayNotas)
            {
                double nota = 0;
                double.TryParse(sNota, out nota);
                listaNotas.Add(nota);
            }

            aluno.Notas = listaNotas;
            Aluno.AddAluno(aluno);

            Console.Clear();
            Console.WriteLine("Aluno cadastrado com sucesso!");
            Thread.Sleep(2000);
        }
    }
}
