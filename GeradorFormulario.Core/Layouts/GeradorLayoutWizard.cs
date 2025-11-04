using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorFormulario.Core.Models;

namespace GeradorFormulario.Core.Layouts
{
    public class GeradorLayoutWizard : IGeradorLayout
    {
        public string GerarHtml(DefinicaoFormulario definicao)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<html><head><title>" + definicao.NomeFormulario + "</title>");
            // (Aqui iria o CSS/JS de um Wizard, ex: Bootstrap)
            html.AppendLine("</head><body>");

            html.AppendLine($"<form action='{definicao.Conexao?.UrlAcao}' method='POST'>");

            // Transforma cada <fieldset> em um "passo" do wizard
            int passoNum = 1;
            foreach (var secao in definicao.Secoes)
            {
                html.AppendLine($"<div class='wizard-step' id='passo-{passoNum}'>");
                html.AppendLine($"<h3>{secao.Titulo} (Passo {passoNum})</h3>");

                // (Aqui iria a lógica para renderizar os campos da seção)

                html.AppendLine("<button type='button' class='btn-proximo'>Próximo</button>");
                html.AppendLine("</div>");
                passoNum++;
            }

            html.AppendLine("</form></body></html>");
            return html.ToString();
        }
    }
}
