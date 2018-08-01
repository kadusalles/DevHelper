using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Parametro
    {
        public int tabParametroSistemaId { get; set; }
        public string descricao { get; set; }
        public string parDefault { get; set; }
        public string parAtual { get; set; }

        public Parametro(string descricao_in, string parDefault_in, string parAtual_in)
        {
            descricao = descricao_in;
            parDefault = parDefault_in;
            parAtual = parAtual_in;
            valorizaTabParametroSistemaIdLivre();
        }

        private void valorizaTabParametroSistemaIdLivre()
        {
            foreach (Conexao conexao in DevHelper.conexoes)
            {
                if (conexao.nome == "CARGOSOL")
                {
                    SqlConnection conn = conexao.objConexao;
                    string nome = conexao.nome;
                    using (conn)
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(@"  DECLARE @PARAMETRO_ID INT = 0
                                                                SELECT @PARAMETRO_ID = CASE
                                                            	    WHEN TAB_PARAMETRO_SISTEMA_ID - @PARAMETRO_ID = 1 THEN TAB_PARAMETRO_SISTEMA_ID
                                                            	    ELSE @PARAMETRO_ID
                                                            	END
                                                                FROM TAB_PARAMETRO_SISTEMA ORDER BY TAB_PARAMETRO_SISTEMA_ID
                                                                SELECT @PARAMETRO_ID + 1 AS TAB_PARAMETRO_SISTEMA_ID
                                                            ", conn); 

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    tabParametroSistemaId = Convert.ToInt32(reader.GetValue(0));
                                }
                            }
                        }
                    }
                }
               
            }
        }

    }
}
