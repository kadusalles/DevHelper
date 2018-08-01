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
            Pendencia pendencia = new Pendencia(parametros[1]);
            string caminhoPacote = criarPastaPacote(ConfigurationManager.AppSettings["CaminhoPacote"], pendencia);
            criarPastasInternas(caminhoPacote);
            criarSC(caminhoPacote+"/Pacote/", pendencia);
            criarSCRollback(caminhoPacote + "/Pacote/", pendencia);
            DevHelper.print("Sucesso!", "N");
            DevHelper.print("Pacote criado em: "+ caminhoPacote, "N");

        }
        public string criarPastaPacote(string caminho, Pendencia pendencia)
        {
            DevHelper.print("Criando pasta do pacote", "N");
            string caminhoPacote = caminho + '/' + "VXXXXXX_ID" + pendencia.pendenciaId + " - " + DevHelper.limparString(pendencia.titulo);
            Directory.CreateDirectory(caminhoPacote);

            return caminhoPacote;
        }
        public void criarPastasInternas(string caminho)
        {
            DevHelper.print("Criando pastas internas...", "N");
            List<string> pastas = new List<string>();
            pastas.Add(caminho + "/Pacote");
            pastas.Add(caminho + "/Definição");
            pastas.Add(caminho + "/Teste");
            pastas.Add(caminho + "/Errata");
            pastas.Add(caminho + "/Validacao");
            pastas.Add(caminho + "/Pacote/Rollback");
            pastas.Add(caminho + "/Pacote/AD");
            pastas.Add(caminho + "/Pacote/SP");
            pastas.Add(caminho + "/Pacote/SC");
            pastas.Add(caminho + "/Pacote/DLL");
            pastas.Add(caminho + "/Pacote/ASP");

            foreach (string pasta in pastas)
            {
                Directory.CreateDirectory(pasta);
            }
        }
        public void criarSC(string caminho, Pendencia pendencia)
        {
            DevHelper.print("Criando SC...", "N");
            string carminhoArquivo = caminho + "SC_" + pendencia.pendenciaId + ".sql";
            StreamWriter writer = new StreamWriter(carminhoArquivo);



            writer.WriteLine("--SELECT VERSAO FROM FUNCAO WHERE FUNCAO_ID = " + pendencia.funcaoId);
            writer.WriteLine("IF EXISTS(SELECT FUNCAO_ID FROM FUNCAO WHERE FUNCAO_ID = " + pendencia.funcaoId + ")");
            writer.WriteLine("BEGIN");
            writer.WriteLine("    UPDATE FUNCAO");
            writer.WriteLine("    SET VERSAO = X");
            writer.WriteLine("    WHERE FUNCAO_ID = " + pendencia.funcaoId);
            writer.WriteLine("END");
            writer.Close();
        }
        public void criarSCRollback(string caminho, Pendencia pendencia)
        {
            DevHelper.print("Criando SC de Rollback...", "N");
            string carminhoArquivo = caminho + "SC_" + pendencia.pendenciaId + "_Rollback.sql";
            StreamWriter writer = new StreamWriter(carminhoArquivo);
            writer.WriteLine("IF EXISTS(SELECT FUNCAO_ID FROM FUNCAO WHERE FUNCAO_ID = " + pendencia.funcaoId + ")");
            writer.WriteLine("BEGIN");
            writer.WriteLine("    EXEC SP_UPDATE \'FUNCAO\', \'WHERE FUNCAO_ID = " + pendencia.funcaoId + "\'");
            writer.WriteLine("END");
            writer.Close();
        }
    }
}
