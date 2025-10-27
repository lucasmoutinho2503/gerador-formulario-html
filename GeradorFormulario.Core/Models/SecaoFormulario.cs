using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Models
{
    public class SecaoFormulario
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        [Browsable(false)]
        public List<LinhaFormulario> Linhas { get; set; }

        public SecaoFormulario()
        {
            Linhas = new List<LinhaFormulario>();
        }
    }
}
