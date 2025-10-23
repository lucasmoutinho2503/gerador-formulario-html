using GeradorFormulario.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Models
{
    public class CampoFormulario
    {
        public TipoCampo Tipo { get; set; }
        public string Nome { get; set; }
        public string Rotulo { get; set; }
        public string Placeholder { get; set; }
        public bool Obrigatorio { get; set; } = false;
        public List<string> Opcoes { get; set; }
        public string ValidacaoEspecial { get; set; }
        public int TamanhoColuna { get; set; } = 1;

        public CampoFormulario()
        {
            Opcoes = new List<string>();
        }
    }
}
