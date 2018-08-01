using System;
using System.Collections.Generic;
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
            Pendencia pendencia = new Pendencia(parametros[1]);
            string descricao_in = DevHelper.input("Digite a descrição do parâmetro: ");
            string parDefault_in = DevHelper.input("Digite a parâmetro padrão: ");
            string parAtual_in = DevHelper.input("Digite o parâmetro atual: ");
            
        }

        private void criarParametro(string descricao_in, string parDefault_in, string parAtual_in)
        {
            Parametro parametro = new Parametro(descricao_in, parDefault_in, parAtual_in);
            
        }
    }
}
