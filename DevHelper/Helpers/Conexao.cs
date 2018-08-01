using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Conexao
    {
        public string nome { get; set; }
        public string nomePadronizado { get; set; }
        public string connectionString { get; set; }
        public string exibeVVF { get; set; }
        public SqlConnection objConexao;


        public Conexao(string nome_in, string connectionString)
        {
            objConexao = new SqlConnection();
            nome = nome_in;
            exibeVVF = (nome == "PENDENCIA") ? "N" : "S";
            objConexao.ConnectionString = connectionString;
        }

        public void padronizarNome(int tamanho)
        {
            nomePadronizado = nome;
            for (int i = nomePadronizado.Length; i < tamanho; i++)
            {
                nomePadronizado += " ";
            }
            nomePadronizado += ": ";
        }
    }
}
