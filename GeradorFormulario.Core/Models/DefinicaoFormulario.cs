using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Models
{
    public class DefinicaoFormulario
    {
        public string UrlLogo { get; set; }
        public string TituloHeader { get; set; }
        public string SubtituloHeader { get; set; }

        public string NomeFormulario { get; set; }
        public string UrlAcao { get; set; }
        public string Metodo { get; set; }
        public string NomeArquivoCss { get; set; }
        public List<SecaoFormulario> Secoes { get; set; }
        public string CorPrincipal { get; set; } = "#007bff";

        public DefinicaoFormulario()
        {
            Metodo = "POST";
            Secoes = new List<SecaoFormulario>();
        }
    }
}
