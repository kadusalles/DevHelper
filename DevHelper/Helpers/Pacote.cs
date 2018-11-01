using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Pacote
    {
        public Pendencia pendencia { get; set; }
        public string caminho { get; set; }

        public Pacote(Pendencia pendencia_in, string caminho_in)
        {
            DevHelper.print("Verificando existência do pacote...", "N");
            pendencia = pendencia_in;
            caminho = caminho_in + '/' + "VXXXXXX_ID" + pendencia.pendenciaId + " - " + DevHelper.limparString(pendencia.titulo);
            if (!Directory.Exists(caminho)) //se o pacote não existe
            {
                criarPastaPacote();
            }

        }

        public void criarPastaPacote()
        {
            DevHelper.print("Criando pasta do pacote", "N");
            Directory.CreateDirectory(caminho);
            criarPastasInternas();
        }

        public void criarPastasInternas()
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

    }
}
