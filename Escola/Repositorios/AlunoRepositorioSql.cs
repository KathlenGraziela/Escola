using Escola.Entidades;
using Escola.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Escola.Repositorios
{
    public class AlunoRepositorioSql : IRepositorio
    {
        private string stringConexaoSql()
        {
            return System.Configuration.ConfigurationManager.AppSettings["conexao_sql"];
        }
        public List<Aluno> Todos()
        {
            var alunos = new List<Aluno>();
            using (var cnn = new SqlConnection(stringConexaoSql()))
            {
                cnn.Open();
                using (var cmd = new SqlCommand("select * from alunos", cnn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var aluno = new Aluno();
                            aluno.Id = Convert.ToInt32(dr["id"]);
                            aluno.Nome = dr["nome"].ToString();
                            aluno.Matricula = dr["matricula"].ToString();

                            alunos.Add(aluno);
                        }
                    }

                    foreach (var aluno in alunos)
                    {
                        using (var cmdNotas = new SqlCommand("select * from notas where aluno_id =" + aluno.Id, cnn))
                        {
                            using (SqlDataReader drNotas = cmdNotas.ExecuteReader())
                            {
                                aluno.Notas = new List<double>();
                                while (drNotas.Read())
                                {
                                    aluno.Notas.Add(Convert.ToDouble(drNotas["nota"]));
                                }
                            }
                        }
                    }
                }
                cnn.Close();
            }
            return alunos;
        }

        public void Salvar(Aluno aluno)
        {
            using (var cnn = new SqlConnection(stringConexaoSql()))
            {
                cnn.Open();
                var cmd = new SqlCommand("insert into alunos(nome, matricula) values (@nome, @matricula);select @@identity", cnn); ;
                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
                int aluno_id = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var nota in aluno.Notas)
                {
                    var cmdNota = new SqlCommand("insert into notas(aluno_id, nota) values (@aluno_id, @nota)", cnn); ;
                    cmdNota.Parameters.AddWithValue("@aluno_id", aluno_id);
                    cmdNota.Parameters.AddWithValue("@nota", nota);
                    cmdNota.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        public int Quantidade()
        {
            int qtd = 0;
            using (var cnn = new SqlConnection(this.stringConexaoSql()))
            {
                cnn.Open();
                var cmd = new SqlCommand("select count(1) from alunos", cnn);
                qtd = Convert.ToInt32(cmd.ExecuteScalar());
                cnn.Close();
            }

            return qtd;
        }
    }
}
