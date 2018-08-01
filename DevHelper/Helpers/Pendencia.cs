using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Pendencia
    {
        public string pendenciaId { get; set; }
        public string titulo { get; set; }
        public string funcaoId { get; set; }
        
        public Pendencia(string pendencia)
        {
            consultaPendencia(pendencia);
        }
       

        public void consultaPendencia(string pendencia)
        {
            Conexao conexao = DevHelper.conexoes[0];
            SqlConnection conn = conexao.objConexao;
            string nome = conexao.nome;
            string comando;
            using (conn)
            {
                conn.Open();
                comando = "";
                comando += "SELECT  pendencia.pendencia_id, ";
                comando += "        pendencia.Titulo, ";
                comando += "        pendencia.funcao_id ";
                comando += "FROM pendencia ";
                comando += "WHERE pendencia_id =" + pendencia + " ;";

                SqlCommand command = new SqlCommand(comando, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pendenciaId = Convert.ToString(reader.GetValue(0));
                            titulo = Convert.ToString(reader.GetValue(1));
                            funcaoId = Convert.ToString(reader.GetValue(2));
                        }
                    }
                    else
                    {
                        DevHelper.print("Pendência não encontrada.");
                    }
                }
            }
        }
    }
}
