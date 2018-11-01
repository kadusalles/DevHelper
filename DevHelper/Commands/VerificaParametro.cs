using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Helpers;

namespace DevHelper.Commands
{
    class VerificaParametro : Comando
    {
        public override void executar(List<string> parametros)
        {
            if (parametros.Count() > 1)
            {
                List<Parametro> listaParametros = new List<Parametro>();
                Parametro parametro;
                foreach (Conexao conexao in DevHelper.conexoes)
                {
                    if (conexao.nome != "PENDENCIA")
                    {
                        parametro = new Parametro(conexao, Convert.ToInt32(parametros[1]));
                        parametro.Consulta();
                        listaParametros.Add(parametro);
                    }
                }

                DevHelper.print("--INFO----------------------------- ", "N");
                DevHelper.print("Parametro: " + parametros[1], "N");
                DevHelper.print("Descrição: " + listaParametros[0].descricao, "N");
                DevHelper.print("Funçao   : " + listaParametros[0].funcao_id.ToString(), "N");
                DevHelper.print("", "N");
                DevHelper.print("--VALORES------------------------- ", "N");
                foreach (Parametro parametroItem in listaParametros)
                {
                    DevHelper.print(parametroItem.nomePadronizadoConexao + parametroItem.parAtual, "N");
                }
            }
            else
            {
                DevHelper.print("Informe o id do parametro.");
            }

            
        }
    }
}
