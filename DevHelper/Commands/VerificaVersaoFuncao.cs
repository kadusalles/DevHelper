using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Commands;
using DevHelper.Helpers;

namespace DevHelper.Commands
{
    public class VerificaVersaoFuncao : Comando
    {
        string comandoCompleto = "verificaVersaoFuncao";
        string comandoEncurtado = "vvf";
        string textoHelp = "Verifica a versão da função em todos os bancos configurados no arquivo config";
        public override void executar(List<string> parametros)
        {
            if (parametros.Count() > 1)
            {
                foreach (Conexao conexao in DevHelper.conexoes)
                {

                    if (conexao.exibeVVF == "S")
                    {
                        verificarVersaoFuncao(conexao, parametros[1]);
                    }
                }
            }
            else
            {
                DevHelper.print("Informe o id da função.");
            }
            
            
        }

        protected void verificarVersaoFuncao(Conexao conexao, string funcao)
        {
            SqlConnection conn = conexao.objConexao;
            string nome = conexao.nome;
            using (conn)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT versao FROM FUNCAO WHERE FUNCAO_ID = "+funcao, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DevHelper.print(conexao.nomePadronizado + Convert.ToString(reader.GetValue(0)), "N");
                        }
                    }
                    else
                    {
                        DevHelper.print("Função não encontrada.");
                    }
                }
            }
        }
    }
}
