using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Helpers;
using System.Configuration;

namespace DevHelper.Commands
{
    public class CriarPacote : Comando
    {
        string comandoCompleto = "criarPacote";
        string comandoEncurtado = "cp";
        string textoHelp = "Verifica a versão da função em todos os bancos configurados no arquivo config";

        public override void executar(List<string> parametros)
        {
            if (parametros.Count() > 1)
            {
                Pendencia pendencia = new Pendencia(parametros[1]);
                Pacote pacote = new Pacote(pendencia, ConfigurationManager.AppSettings["CaminhoPacote"]);
                SC sc = new SC(pacote.caminho + "/Pacote/", pendencia,false);
                SC scRollback = new SC(pacote.caminho + "/Pacote/", pendencia,true);
                DevHelper.print("Sucesso!", "N");
                DevHelper.print("Pacote criado em: " + pacote.caminho, "N");
            }
            else
            {
                DevHelper.print("Informe o id da pendencia.");
            }
        }

       
       
    }
}
