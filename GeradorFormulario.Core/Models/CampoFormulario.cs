using GeradorFormulario.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace GeradorFormulario.Core.Models
{
    public class CampoFormulario
    {
        [Category("Dados")]
        [DisplayName("Tipo de Campo")]
        [Description("O tipo de controle HTML a ser gerado.")]
        public TipoCampo Tipo { get; set; }

        [Category("Dados")]
        [DisplayName("Nome Interno ")]
        [Description("O 'name' interno do campo (para vinculação).")]
        public string Nome { get; set; }

        [Category("Aparência")]
        [DisplayName("Label")]
        [Description("O texto que aparece acima do campo (a <label>).")]
        public string Rotulo { get; set; }

        [Category("Aparência")]
        [DisplayName("Placeholder")]
        [Description("O texto de dica dentro do campo (o 'placeholder').")]
        public string Placeholder { get; set; }

        [Category("Comportamento")]
        [DisplayName("Obrigatório?")]
        [Description("Define se o campo é obrigatório ('required').")]
        public bool Obrigatorio { get; set; } = false;

        [Browsable(false)]
        public List<string> Opcoes { get; set; }

        [Category("Comportamento")]
        [DisplayName("Validação Especial")]
        [Description("Qual validador JS usar (ex: cpf, data, email, celular).")]
        public string ValidacaoEspecial { get; set; }

        [Category("Layout")]
        [DisplayName("Tamanho da Coluna")]
        [Description("Define o 'tamanho' da coluna. Ex: 2 para CPF, 1 para RG.")]
        public int TamanhoColuna { get; set; } = 1;

        public CampoFormulario()
        {
            Opcoes = new List<string>();
        }
    }
}
