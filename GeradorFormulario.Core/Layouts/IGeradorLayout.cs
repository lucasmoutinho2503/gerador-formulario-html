using GeradorFormulario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Layouts
{
    public interface IGeradorLayout
    {
        string GerarHtml(DefinicaoFormulario definicao);
        string GerarHtmlConfirmacao(DefinicaoFormulario definicao);
    }
}
