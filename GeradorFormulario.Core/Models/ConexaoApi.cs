using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GeradorFormulario.Core.Models
{

    [DisplayName("Configuração de Conexão")]
    public class ConexaoApi
    {
        [Category("Endpoint")]
        [DisplayName("URL de Ação (Action)")]
        [Description("A URL externa para onde o formulário será enviado.")]
        public string UrlAcao { get; set; }

        [Category("Endpoint")]
        [DisplayName("Alvo (Target)")]
        [Description("O nome do 'frame' que receberá a resposta (ex: formEnviadoFrame).")]
        public string Target { get; set; }

        [Category("Credenciais")]
        [Description("O 'USUARIO' a ser enviado para a API.")]
        public string Usuario { get; set; }

        [Category("Credenciais")]
        [Description("A 'SENHA' a ser enviada para a API.")]
        [PasswordPropertyText(true)] 
        public string Senha { get; set; }

        [Category("Credenciais")]
        [Description("A 'ORGANIZACAO' a ser enviada para a API.")]
        public string Organizacao { get; set; }

        [Category("Metadados")]
        [DisplayName("ID do Formulário")]
        [Description("O 'IDFORMULARIO' a ser enviado para a API.")]
        public string IDFormulario { get; set; }

        [Category("Metadados")]
        [DisplayName("ID da Janela")]
        [Description("O 'IDJANELA' a ser enviado para a API.")]
        public string IDJanela { get; set; }
    }
}
