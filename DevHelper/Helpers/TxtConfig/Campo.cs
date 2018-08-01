using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Campo
    {
        public string nome { get; set; }
        public string valor { get; set; }

        public Campo (string nome, string valor)
        {
            this.nome = nome;
            this.valor = valor;
        }
    }
}
