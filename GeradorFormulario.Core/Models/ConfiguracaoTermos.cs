using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Models
{
    public class ConfiguracaoTermos
    {
        [Category("Conteúdo")]
        [DisplayName("Habilitar Modal de Termos")]
        [Description("Se marcado, o usuário deverá aceitar os termos antes de enviar.")]
        public bool Habilitado { get; set; } = false;

        [Category("Conteúdo")]
        [DisplayName("Título do Modal")]
        [Description("Ex: 'Termos de Uso e Condições'")]
        public string TituloModal { get; set; } = "Termos e Condições";

        [Category("Conteúdo")]
        [DisplayName("Mensagem de Confirmação")]
        [Description("O texto que aparece abaixo dos termos (ex: 'Ao continuar...')")]
        public string MensagemConfirmacao { get; set; } = "Ao continuar, você concorda com os termos de uso.";

        [Category("Conteúdo")]
        [DisplayName("URL da Imagem")]
        [Description("URL de uma imagem para aparecer ao lado do título do modal.")]
        public string UrlImagemTitulo { get; set; } = "";

        [Category("Conteúdo")]
        [DisplayName("Conteúdo")]
        [Description("O corpo principal dos termos. Pode ser usado HTML (<b>, <u>, <img>, <a href...>, etc).")]
        public string ConteudoHtml { get; set; }
    }
}
