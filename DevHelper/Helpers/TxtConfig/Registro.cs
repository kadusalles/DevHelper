using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper.Helpers
{
    public class Registro
    {
        public string codigo { get; set; }
        public List<Campo> campos { get; set; }

        public Registro()
        {
            campos = new List<Campo>();
        }
    }
}
