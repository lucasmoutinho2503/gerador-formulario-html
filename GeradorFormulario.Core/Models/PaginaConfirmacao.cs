using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Models
{
    public class PaginaConfirmacao
    {
        [DisplayName("Habilitar Página")]
        public bool Habilitado { get; set; } = true;

        [DisplayName("Título da Janela")]
        public string TituloPagina { get; set; } = "Envio Confirmado";

        [DisplayName("Conteúdo (Pode usar HTML)")]
        public string ConteudoHtml { get; set; } = "<h1>Obrigado!</h1><p>Seu formulário foi enviado com sucesso.</p>";
    }
}
