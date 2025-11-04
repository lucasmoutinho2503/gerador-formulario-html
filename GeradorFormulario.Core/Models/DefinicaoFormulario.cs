using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorFormulario.Core.Models;

namespace GeradorFormulario.Core.Models
{
    public enum EstiloDeLayout
    {
        Classico,
        Ficheiro
        //Wizard,   
        //Simples   
    }

    public class DefinicaoFormulario
    {
        [Category("Formulário")]
        [DisplayName("Layout")]
        [Description("O estilo visual/estrutural do formulário (Clássico, Wizard, etc.)")]
        public EstiloDeLayout EstiloLayout { get; set; } = EstiloDeLayout.Classico;

        [Category("Cabeçalho")]
        [DisplayName("URL do Logo")]
        [Description("O caminho (URL ou local) para a imagem do logo.")]
        public string UrlLogo { get; set; }

        [Category("Cabeçalho")]
        [DisplayName("Título Principal")]
        [Description("O título do Formulário")]
        public string TituloHeader { get; set; }

        [Category("Cabeçalho")]
        [DisplayName("Subtítulo")]
        [Description("O texto que aparece na caixa colorida.")]
        public string SubtituloHeader { get; set; }

        [Category("Formulário")]
        [DisplayName("Título da Janela")]
        [Description("O texto que aparece na aba do navegador.")]
        public string NomeFormulario { get; set; }

        [Category("Formulário")]
        [DisplayName("Método de Envio")]
        [Description("Como os dados serão enviados (POST ou GET).")]
        public string Metodo { get; set; }

        [Category("Metadados")]
        [DisplayName("Tags de Categoria")]
        [Description("Lista de tags para filtrar (ex: RH, Hotel, PT-BR).")]
        public List<string> Tags { get; set; }

        [Browsable(false)]
        public List<SecaoFormulario> Secoes { get; set; }

        [Category("Aparência")]
        [DisplayName("Cor Principal")]
        [Description("A cor do tema do formulário (ex: #FFA500).")]
        public string CorPrincipal { get; set; } = "#007bff";

        [Browsable(false)]
        public ConexaoApi Conexao { get; set; }

        public DefinicaoFormulario()
        {
            Metodo = "POST";
            EstiloLayout = EstiloDeLayout.Classico;
            Secoes = new List<SecaoFormulario>();
            Conexao = null;
            Tags = new List<string>();
        }
    }
}
