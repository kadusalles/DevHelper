using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Helpers;

namespace DevHelper.Commands
{
    public class CriarParametro : Comando
    {
        public override void executar(List<string> parametros)
        {
            if (parametros.Count() > 1)
            {
                Pendencia pendencia = new Pendencia(parametros[1]);
                string descricao_in = DevHelper.input("Digite a descrição do parâmetro: ");
                string parDefault_in = DevHelper.input("Digite a parâmetro padrão: ").ToUpper();
                string parAtual_in = DevHelper.input("Digite o parâmetro atual: ").ToUpper();
                string caminho = DevHelper.encontraPacote(pendencia.pendenciaId);


                Parametro parametro = new Parametro(descricao_in, parDefault_in, parAtual_in, pendencia.funcaoId);

                SC sc = new SC(caminho, pendencia,false);
                string comando = 
                "IF NOT EXISTS(SELECT 1 FROM TAB_PARAMETRO_SISTEMA WHERE TAB_PARAMETRO_SISTEMA_ID = " + parametro.tabParametroSistemaId + ")\n" +
                "BEGIN\n" +
                "	INSERT INTO TAB_PARAMETRO_SISTEMA(Tab_Parametro_Sistema_Id,Desc_Parametro_Sistema,Parametro,Funcao_Id,Msg_Parametro,Default_Parametro,Desc_Parametro)\n" +
                "	VALUES (" + parametro.tabParametroSistemaId + ",'" + parametro.descricao + "','" + parametro.parAtual + "'," + parametro.funcao_id + ",'" + parametro.descricao + "','" + parametro.parDefault + "','" + parametro.descricao + "')\n" +
                "END\n";
                sc.alterarSC(comando);

            }
            else
            {
                DevHelper.print("Informe o id da pendencia.");
            }
        }
    }
}
