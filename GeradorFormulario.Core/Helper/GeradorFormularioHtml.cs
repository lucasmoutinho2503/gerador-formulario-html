using GeradorFormulario.Core.Layouts;
using GeradorFormulario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Helper
{
    public class GeradorFormularioHtml
    {
        public string GerarHtml(DefinicaoFormulario definicao)
        {
            IGeradorLayout geradorLayout;

            // 2. O 'switch' que lê a nova propriedade
            switch (definicao.EstiloLayout)
            {
                // (Aqui você pode adicionar os outros layouts, como 'Wizard')
                case EstiloDeLayout.Ficheiro:
                    geradorLayout = new GeradorLayoutFicheiro();
                    break;

                case EstiloDeLayout.Classico:
                default:
                    // 3. Escolhe o "trabalhador" (o que tem seu código antigo)
                    geradorLayout = new GeradorLayoutClassico();
                    break;
            }

            // 4. Delega todo o trabalho para o "trabalhador" escolhido
            return geradorLayout.GerarHtml(definicao);
        }
    }
}
