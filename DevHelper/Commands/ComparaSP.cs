using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Helpers;

namespace DevHelper.Commands
{
    public class ComparaSP : Comando
    {
        string comandoCompleto = "comparaSP";
        string comandoEncurtado = "csp";
        string textoHelp = "Compara a SP de todos os ambientes com o Cargosol.";

        public override void executar(List<string> parametros)
        {
            if (parametros.Count() > 1)
            {
                List<string> textoSP = new List<string>();
                string nome_sp = parametros[1];
                foreach (Conexao conexao in DevHelper.conexoes)
                {
                    if (conexao.nome != "PENDENCIA")
                    {
                        SP sp = new SP(conexao, nome_sp);
                        textoSP.Add(sp.texto);
                    }
                }
                int i = 1;
                foreach (string item in textoSP)
                {
                    if (item == null || item == "")
                    {
                        DevHelper.print(DevHelper.conexoes[i].nomePadronizado + " sp não encontrada.", "N");
                        i++;
                    }
                    else
                    {
                        if (textoSP[0] != item)
                        {
                            DevHelper.print(DevHelper.conexoes[i].nomePadronizado + " está diferente.", "N");
                            i++;
                        }
                        else
                        {
                            DevHelper.print(DevHelper.conexoes[i].nomePadronizado + " está igual.", "N");
                            i++;
                        }
                    }

                }
            }
            else
            {
                DevHelper.print("Informe o nome da SP.");
            }
        }



        private string retornaTextoSP( Conexao conexao, string nome_sp )
        {
            SqlConnection conn = conexao.objConexao;
            string saida = null;
            using (conn)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select text from syscomments where id=(select id from sysobjects where name='" + nome_sp + "')", conn);
                command.CommandTimeout = 480;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            saida += Convert.ToString(reader.GetValue(0)).Replace(" ", "").Replace(System.Environment.NewLine,"").ToUpper();
                        }
                    }
                }
            }
            return saida;
        }
    }
}
