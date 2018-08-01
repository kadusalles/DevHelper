using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    /*DEPRECATED*/
    public class Config
    {
        public string caminho { get; set; }
        public List<Registro> registros { get; set; }

        public void lerConfig()
        {
            registros = new List<Registro>();
            DevHelper.print("Lendo config...","S");
            string[] arquivoConfig = System.IO.File.ReadAllLines("config.txt");

            foreach (string linha in arquivoConfig)
            {
                string[] itemsLinha;
                itemsLinha = linha.Split('|');
                Registro registro = new Registro();

                if (linha.Slice(0, 1) != "#")
                {
                    switch (linha.Slice(0, 3))
                    {
                        case "002":
                            registro.codigo = itemsLinha[0];
                            registro.campos.Add(new Campo("nome", itemsLinha[1]));
                            registro.campos.Add(new Campo("ip", itemsLinha[2]));
                            registro.campos.Add(new Campo("banco", itemsLinha[3]));
                            registro.campos.Add(new Campo("usuario", itemsLinha[4]));
                            registro.campos.Add(new Campo("senha", itemsLinha[5]));
                            registro.campos.Add(new Campo("exibevvf", itemsLinha[6].ToUpper()));
                            registros.Add(registro);
                            break;
                        case "001":
                            registro = new Registro();
                            registro.codigo = itemsLinha[0];
                            registro.campos.Add(new Campo("caminho", itemsLinha[1]));
                            registros.Add(registro);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public List<Registro> retornaRegistros(string codigo)
        {
            List<Registro> registrosSelecionados = new List<Registro>();
            foreach (Registro registro in registros)
            {
                if (registro.codigo == codigo)
                {
                    registrosSelecionados.Add(registro);
                }
            }
            return registrosSelecionados;
        }


    }
}

public static class Extensions
{
    /// <summary>
    /// Get the string slice between the two indexes.
    /// Inclusive for start index, exclusive for end index.
    /// </summary>
    public static string Slice(this string source, int start, int end)
    {
        if (end < 0) // Keep this for negative end support
        {
            end = source.Length + end;
        }
        int len = end - start;               // Calculate length
        return source.Substring(start, len); // Return Substring of length
    }
}