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
        public bool isRollback { get; set; }

        public SC(string caminho_in, Pendencia pendencia_in, bool isRollback_in)
        {
            isRollback = isRollback_in;
            caminho = caminho_in;
            pendencia = pendencia_in;
            if (!existeSC()) //se não existe a SC ou está vazia
            {
                criarSC();
            }

        }

        public void criarSC()
        {
            if (isRollback == false)
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
                DevHelper.print("SC criada com sucesso!", "N");
            }
            else
            {
                DevHelper.print("Criando SC de Rollback...", "N");
                string carminhoArquivo = caminho + "SC_" + pendencia.pendenciaId + "_Rollback.sql";
                StreamWriter writer = new StreamWriter(carminhoArquivo);
                writer.WriteLine("IF EXISTS(SELECT FUNCAO_ID FROM FUNCAO WHERE FUNCAO_ID = " + pendencia.funcaoId + ")");
                writer.WriteLine("BEGIN");
                writer.WriteLine("    EXEC SP_UPDATE \'FUNCAO\', \'WHERE FUNCAO_ID = " + pendencia.funcaoId + "\'");
                writer.WriteLine("END");
                writer.Close();
                DevHelper.print("SC de rollback criada com sucesso!", "N");
            }

        }

        private Boolean existeSC()
        {
            string text = null;
            if (isRollback == false)
            {
                DevHelper.print("Verificando existência da SC...","N");
                if(File.Exists(caminho + "/SC_" + pendencia.pendenciaId + ".sql"))
                {
                    DevHelper.print("SC existe...", "N");
                    return true;
                }
                else
                {
                    DevHelper.print("SC não existe.", "N");
                    return false;
                }

            }
            else
            {
                DevHelper.print("Verificando existência da SC de Rollback...", "N");
                if (File.Exists(caminho + "/Pacote/SC_" + pendencia.pendenciaId + "_Rollback.sql"))
                {
                    DevHelper.print("SC de rollback existe...", "S");
                    return true;
                }
                else
                {
                    DevHelper.print("SC de rollback não existe.", "S");
                    return false;
                }
            }
        }

        public void alterarSC(string comando)
        {
            DevHelper.print("Alterando SC...", "N");
            string carminhoArquivo = caminho + "/Pacote/SC_" + pendencia.pendenciaId + ".sql";
            StreamReader reader = new StreamReader(carminhoArquivo);
            string text = reader.ReadToEnd();
            reader.Close();
            StreamWriter writer = new StreamWriter(carminhoArquivo);
            writer.WriteLine(text);
            writer.WriteLine("");
            writer.Write(comando);
            writer.Close();
            DevHelper.print("SC alterada com sucesso!", "N");
        }

        public void criarSCRollback()
        {

        }
    }
}
