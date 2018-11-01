using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class SP
    {
        public string nome;
        public string versao;
        public string texto;
        public Conexao conexao;




        public SP(Conexao conexao_in, string nome_sp)
        {
            nome = nome_sp;
            conexao = conexao_in;
            retornaTextoSP();
        }

        private void retornaTextoSP()
        {
            SqlConnection conn = conexao.objConexao;
            string saida = null;
            using (conn)
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select text from syscomments where id=(select id from sysobjects where name='" + nome + "')", conn);
                command.CommandTimeout = 480;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            saida += Convert.ToString(reader.GetValue(0)).Replace(" ", "").Replace(System.Environment.NewLine, "").ToUpper();
                        }
                    }
                }
            }
            texto = saida;
        }
    }
}
