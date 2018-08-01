using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class SC
    {
        public string caminho { get; set; }
        public Pendencia pendencia { get; set; }

        public SC(string caminho_in, Pendencia pendencia_in)
        {
            caminho = caminho_in;
            pendencia = pendencia_in;
        }

        public void criarSC()
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

        public void alterarSC(string comando)
        {
            DevHelper.print("Alterando SC...", "N");
            string carminhoArquivo = caminho + "SC_" + pendencia.pendenciaId + ".sql";
            StreamWriter writer = new StreamWriter(carminhoArquivo);
            writer.WriteLine("");
            writer.WriteLine(comando);
            writer.Close();
        }


    }
}
