using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevHelper.Helpers;
using DevHelper.Commands;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

namespace DevHelper
{
    public class DevHelper
    {

        public static List<Conexao> conexoes = new List<Conexao>();

        public static void exibeMsgBoasVindas()
        {
            string[] lines = {
            "-------------------------------------------------------------------------------\n" ,
            "-------------------------------------------------------------------------------\n" ,
            "                _____             _    _      _                 \n" ,
            "               |  __ \\           | |  | |    | |                \n" ,
            "               | |  | | _____   _| |__| | ___| |_ __   ___ _ __ \n" ,
            "               | |  | |/ _ \\ \\ / /  __  |/ _ \\ | '_ \\ / _ \\ '__|\n" ,
            "               | |__| |  __/\\ V /| |  | |  __/ | |_) |  __/ |   \n" ,
            "               |_____/ \\___| \\_/ |_|  |_|\\___|_| .__/ \\___|_|   \n" ,
            "                                               | |              \n" ,
            "                                               |_|              \n" ,
            "-------------------------------------------------------------------------------\n" ,
            "               ASSISTENTE DE DESENVOLVIMENTO            \n" ,
            "               Por Carlos Salles                        \n" ,
            "-------------------------------------------------------------------------------\n"
            };

            foreach (string line in lines)
            {
                Console.Write(line);
            }
        }

        public static void readCommands()
        {
            DevHelper.print("Iniciando DevHelper... ", "S");
            DevHelper.print("Criando conexões...");
            criarConexoes();
            padronizarNomeConexoes();
            DevHelper.print("Inicialização completada com sucesso!","S");
            DevHelper.print("\nBem vindo! Digite um comando para iniciar ou 'help' para ajuda.", "N");
            List<string> parametros = new List<string>();
            List<string> comando = command_input("[DEVHELPER]> ");
            Comando objComando;
            while (comando[0] != "sair" && comando[0] != "quit" && comando[0] != "q" && comando[0] != "exit" && comando[0] != "close")
            {
                switch (comando[0])
                {
                    case "verificaversaofuncao":
                    case "vvf":
                        criarConexoes();
                        objComando = new VerificaVersaoFuncao();
                        objComando.executar(comando);
                        break;

                    case "verificafuncao":
                    case "vf":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "verificasptemplate":
                    case "vspt":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "criarpacote":
                    case "cp":
                        criarConexoes();
                        objComando = new CriarPacote();
                        objComando.executar(comando);
                        break;


                    case "comparaSP":
                    case "csp":
                        criarConexoes();
                        objComando = new ComparaSP();
                        objComando.executar(comando);
                        break;

                    case "validarPacote":
                    case "vp":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "criarParticularidade":
                    case "cparticul":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "criarParametro":
                    case "cparam":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "prepararEnvioPacote":
                    case "pep":
                        DevHelper.print("Função não implementada, aguarde proxima versão...", "S");
                        break;

                    case "help":
                    case "commands":
                        DevHelper.print("", "N");
                        DevHelper.print("----- COMANDOS DISPONÍVEIS ---------------", "N");
                        DevHelper.print("LEGENDA: comando|abreviacao {entrada} \n", "N");

                        DevHelper.print("verificaVersaoFuncao|vvf {id da funcao}", "N");
                        DevHelper.print("- Retorna a versão da função em todos os ambientes do app.config\n", "N");

                        //DevHelper.print("verificaFuncao|vf {id da funcao}", "N");
                        //DevHelper.print("- Retorna dados da função nos ambientes do app.config\n", "N");

                        //DevHelper.print("verificaSPTemplate|vspt {id do template}", "N");
                        //DevHelper.print("- Retorna a SP utilizada no template nos ambientes do app.config\n", "N");

                        DevHelper.print("criarPacote|cp {id da pendencia}", "N");
                        DevHelper.print("- Cria pacote na pasta de pacotes informada no app.config utilizando os dados presentes " +
                            "no cadastro da pendência, assim como utilizará a função para criar a SC e etc.\n", "N");


                        DevHelper.print("comparaSP|csp {nome da SP}", "N");
                        DevHelper.print("- Verifica em todos os ambientes se a SP informada está igual ao Cargosol "); 

                        //DevHelper.print("validarPacote|vp {id da pendencia}", "N");
                        //DevHelper.print("- Valida o pacote verificando a estrutura, asps, SCs," +
                        //    "ADs, entre outros. Ao final da validação o resultado é colocado dentro da pasta do pacote em uma" +
                        //    "pasta chamada 'Validação' \n", "N");

                        //DevHelper.print("criarParticularidade|cparticul {id da pendencia}", "N");
                        //DevHelper.print("- Cria particularidade com os dados informados, mas não compila no banco, o " +
                        //    "comando é colocado dentro da SC do pacote informado, sendo necessário roda-lo manualmente" +
                        //    " por segurança.\n", "N");

                        //DevHelper.print("criarParametro|cparam {id da pendencia}", "N");
                        //DevHelper.print("- Cria parametro com os dados informados, mas não compila no banco, o " +
                        //    "comando é colocado dentro da SC do pacote informado, sendo necessário roda-lo manualmente" +
                        //    " por segurança.\n", "N");
                        break;
                    case "cls":
                        Console.Clear();
                        DevHelper.exibeCabecalho();
                        break;
                    default:
                        DevHelper.print("Comando não encontrado, digite 'help' para ver os comandos disponíveis", "N");
                        break;
                }
                comando = command_input("[DEVHELPER]> ");
            }
        }

        public static void exibeCabecalho()
        {
            DevHelper.print("-------------------------------------------------------------------------------", "N");
            DevHelper.print("  D E V  H E L P E R  --- ASSISTENTE DE DESENVOLVIMENTO Por Carlos Salles ", "N");
            DevHelper.print("-------------------------------------------------------------------------------", "N");
        }

        public static string input(string msg)
        {
            if (msg == null || msg == "")
            {
                msg = "[DEVHELPER]: ";
            }
            string command = null;
            Console.Write("\n" + msg);
            command = Console.ReadLine();
            return command.ToLower();
        }

        public static List<string> command_input(string msg)
        {
            List<string> comandoParametrizado = new List<string>();
            string[] parametros;

            if (msg == null || msg == "")
            {
                msg = "[DEVHELPER]> ";
            }

            string command = null;
            Console.Write("\n" + msg);
            command = Console.ReadLine();
            parametros = command.Split(' ');
            foreach (string parametro in parametros)
            {
                comandoParametrizado.Add(parametro.ToLower());
            }

            return comandoParametrizado;
        }

        public static void print(string msg, string exibeDevHelper = "S")
        {
            if (exibeDevHelper == "S")
            {
                Console.WriteLine("[DEVHELPER]: " + msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void criarConexoes()
        {
            conexoes.Clear();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                conexoes.Add(new Conexao(css.Name, css.ConnectionString));
            }
            padronizarNomeConexoes("N");
        }

        public static int maiorConexao()
        {
            int maior = 0;
            foreach (Conexao conexao in conexoes)
            {
                if (maior < conexao.nome.Length)
                {
                    maior = conexao.nome.Length;
                }
            }
            return maior;
        }

        public static void padronizarNomeConexoes(string exibePrint = "S")
        {
            List<string> resultado = new List<string>();
            int maior = maiorConexao();
            if (exibePrint != "N")
            {
                DevHelper.print("Padronizando nome das conexões...");
            }
            foreach (Conexao conexao in conexoes)
            {
                conexao.padronizarNome(maior);
            }
        }

        public static string limparString(string entrada)
        {
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string resultado = rgx.Replace(entrada, replacement);
            return resultado;
        }

        public static string encontraPacote(string pendencia)
        {
            string caminhoPastaPacote = ConfigurationManager.AppSettings["CaminhoPacote"];
            string[] pacotes = Directory.GetDirectories(caminhoPastaPacote);
            int inicio;
            int fim;
            string pendenciaAux;
            string retorno;
            for (int i = 0; i < pacotes.Length; i++)
            {
                inicio = pacotes[i].IndexOf("ID")+2;
                fim = (pacotes[i].IndexOf("") + 2) + 6;
                pendenciaAux = pacotes[0].Substring(inicio, fim);
                if (pendenciaAux == pendencia)
                {
                    retorno = caminhoPastaPacote + "\\" + pacotes[0];
                }

            }
            return "";
        }
    }
}
