using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper
{
    public abstract class Comando
    {
        public string comandoCompleto;
        public string comandoEncurtado;
        public string textoHelp;

        public abstract void executar(List<string> parametros);

    }
}
