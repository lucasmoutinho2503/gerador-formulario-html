using System.Collections.Generic;

namespace GeradorFormulario.Core.Models
{
    public class LinhaFormulario
    {
        public List<CampoFormulario> Campos { get; set; }

        public LinhaFormulario()
        {
            Campos = new List<CampoFormulario>();
        }

        public LinhaFormulario(params CampoFormulario[] campos)
        {
            Campos = new List<CampoFormulario>(campos);
        }
    }
}