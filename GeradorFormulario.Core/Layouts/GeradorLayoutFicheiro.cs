using GeradorFormulario.Core.Enums;
using GeradorFormulario.Core.Models;
using System;
using System.Globalization; // Para a conversão de cor
using System.Linq;
using System.Text;

namespace GeradorFormulario.Core.Layouts
{
    /// <summary>
    /// Gera um formulário com layout de "Ficheiro" (Abas).
    /// Cada "Seção" do formulário se torna uma "Aba".
    /// </summary>
    public class GeradorLayoutFicheiro : IGeradorLayout
    {
        // --- MÉTODO PRINCIPAL DA INTERFACE ---
        public string GerarHtmlConfirmacao(DefinicaoFormulario definicao)
        {
            return "";
        }
        public string GerarHtml(DefinicaoFormulario definicao)
        {
            StringBuilder html = new StringBuilder();

            // Flags para os scripts (igual ao layout Clássico)
            bool precisaScriptCpf = false;
            bool precisaScriptData = false;
            bool precisaScriptCelular = false;
            bool precisaScriptEmail = false;
            bool precisaScriptAssinatura = false;

            // Flag para o 'enctype' do formulário (igual ao layout Clássico)
            bool precisaEnctype = definicao.Secoes.SelectMany(s => s.Linhas)
                                                 .SelectMany(l => l.Campos)
                                                 .Any(c => c.Tipo == TipoCampo.Arquivo) ||
                                  definicao.Conexao != null;

            // --- 1. CABEÇALHO HTML E CSS ---
            html.AppendLine("<!DOCTYPE html><html lang=\"pt-br\"><head>");
            html.AppendLine("  <meta charset=\"UTF-8\">");
            html.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine($"  <title>{definicao.NomeFormulario}</title>");
            html.AppendLine("  <style>");
            html.AppendLine(GerarCss(definicao.CorPrincipal)); // Gera o CSS deste layout
            html.AppendLine("  </style>");
            html.AppendLine("</head><body>");

            // --- 2. ESTRUTURA DO FORMULÁRIO ---
            html.AppendLine($"  <div class=\"form-container\">");

            // Header (Logo e Título) - Reutilizado do Clássico
            if (!string.IsNullOrEmpty(definicao.UrlLogo) || !string.IsNullOrEmpty(definicao.TituloHeader))
            {
                html.AppendLine("    <div class=\"form-header\">");
                if (!string.IsNullOrEmpty(definicao.UrlLogo))
                {
                    html.AppendLine("      <div class=\"form-logo\">");
                    html.AppendLine($"        <img src=\"{definicao.UrlLogo}\" alt=\"Logo\" style=\"width:140px; height:140px; object-fit:contain;\">");
                    html.AppendLine("      </div>");
                }
                html.AppendLine("      <div class=\"form-header-text\">");
                if (!string.IsNullOrEmpty(definicao.TituloHeader))
                {
                    html.AppendLine($"        <h2 class=\"form-title\">{definicao.TituloHeader}</h2>");
                }
                if (!string.IsNullOrEmpty(definicao.SubtituloHeader))
                {
                    html.AppendLine($"        <div class=\"form-subtitle-box\">{definicao.SubtituloHeader}</div>");
                }
                html.AppendLine("      </div>");
                html.AppendLine("    </div>");
            }

            // Tag <form> (Lógica do Clássico)
            string enctypeAttr = precisaEnctype ? " enctype=\"multipart/form-data\"" : "";
            string targetAttr = (definicao.Conexao != null && !string.IsNullOrEmpty(definicao.Conexao.Target))
                                ? $" target=\"{definicao.Conexao.Target}\"" : "";
            string actionAttr = (definicao.Conexao != null && !string.IsNullOrEmpty(definicao.Conexao.UrlAcao))
                                ? $" action=\"{definicao.Conexao.UrlAcao}\""
                                : "";

            html.AppendLine($"    <form method='{definicao.Metodo.ToLower()}'{enctypeAttr}{actionAttr}{targetAttr}>");

            // Campos Ocultos da API (Lógica do Clássico)
            if (definicao.Conexao != null)
            {
                html.AppendLine("      ");
                if (!string.IsNullOrEmpty(definicao.Conexao.Usuario))
                    html.AppendLine($"      <input type=\"hidden\" name=\"USUARIO\" value=\"{definicao.Conexao.Usuario}\">");
                if (!string.IsNullOrEmpty(definicao.Conexao.Senha))
                    html.AppendLine($"      <input type=\"hidden\" name=\"SENHA\" value=\"{definicao.Conexao.Senha}\">");
                if (!string.IsNullOrEmpty(definicao.Conexao.Organizacao))
                    html.AppendLine($"      <input type=\"hidden\" name=\"ORGANIZACAO\" value=\"{definicao.Conexao.Organizacao}\">");
                if (!string.IsNullOrEmpty(definicao.Conexao.IDFormulario))
                    html.AppendLine($"      <input type=\"hidden\" name=\"IDFORMULARIO\" value=\"{definicao.Conexao.IDFormulario}\">");
                if (!string.IsNullOrEmpty(definicao.Conexao.IDJanela))
                    html.AppendLine($"      <input type=\"hidden\" name=\"IDJANELA\" value=\"{definicao.Conexao.IDJanela}\">");
                html.AppendLine("      ");
            }

            // --- 3. O LAYOUT DE ABAS (O "FICHEIRO") ---

            // A. A barra de botões das abas
            html.AppendLine("      <div class=\"ficheiro-abas\">");
            bool primeiraAba = true;
            foreach (var secao in definicao.Secoes)
            {
                // Gera um botão para cada SEÇÃO
                string idAba = secao.Titulo.Replace(" ", "-").Replace("ç", "c").Replace("ã", "a"); // ID seguro
                string classeAtiva = primeiraAba ? " active" : "";
                html.AppendLine($"        <button type=\"button\" class=\"aba-link{classeAtiva}\" onclick=\"abrirAba(event, '{idAba}')\">{secao.Titulo}</button>");
                primeiraAba = false;
            }
            html.AppendLine("      </div>");

            // B. O conteúdo das abas
            html.AppendLine("      <div class=\"ficheiro-conteudo\">");
            primeiraAba = true;
            foreach (var secao in definicao.Secoes)
            {
                // Gera um painel para cada SEÇÃO
                string idAba = secao.Titulo.Replace(" ", "-").Replace("ç", "c").Replace("ã", "a"); // ID seguro
                string classeAtiva = primeiraAba ? " active" : "";
                html.AppendLine($"      <div id=\"{idAba}\" class=\"aba-painel{classeAtiva}\">");

                // --- Reutiliza a lógica de renderizar campos do Layout Clássico ---
                foreach (var linha in secao.Linhas)
                {
                    html.AppendLine("        <div class=\"form-row\">");
                    foreach (var campo in linha.Campos)
                    {
                        // Coleta flags de script
                        if (!string.IsNullOrEmpty(campo.ValidacaoEspecial))
                        {
                            if (campo.ValidacaoEspecial == "cpf") precisaScriptCpf = true;
                            if (campo.ValidacaoEspecial == "data") precisaScriptData = true;
                            if (campo.ValidacaoEspecial == "celular") precisaScriptCelular = true;
                            if (campo.ValidacaoEspecial == "email") precisaScriptEmail = true;
                            if (campo.ValidacaoEspecial == "assinatura") precisaScriptAssinatura = true;
                        }

                        // Lógica de renderização do campo
                        html.AppendLine($"        <div class=\"form-group\" style=\"flex: {campo.TamanhoColuna};\">");

                        string requiredAttr = campo.Obrigatorio ? " required" : "";
                        string placeholderAttr = !string.IsNullOrEmpty(campo.Placeholder) ? $" placeholder='{campo.Placeholder}'" : "";
                        string validacaoAttr = !string.IsNullOrEmpty(campo.ValidacaoEspecial) ? $" data-validacao='{campo.ValidacaoEspecial}'" : "";
                        string controlClass = "class=\"form-control\"";

                        if (campo.Tipo == TipoCampo.CaixaDeSelecao)
                        {
                            html.AppendLine("          <div class=\"checkbox-group\">");
                            html.AppendLine($"            <input type='checkbox' id='{campo.Nome}' name='{campo.Nome}'{requiredAttr} {controlClass} {validacaoAttr}>");
                            html.AppendLine($"            <label for='{campo.Nome}' class=\"form-label\">{campo.Rotulo}</label>");
                            html.AppendLine("          </div>");
                        }
                        else
                        {
                            html.AppendLine($"        <label for='{campo.Nome}' class=\"form-label\">{campo.Rotulo}:</label>");
                            switch (campo.Tipo)
                            {
                                case TipoCampo.AreaDeTexto:
                                    html.AppendLine($"        <textarea id='{campo.Nome}' name='{campo.Nome}'{placeholderAttr}{requiredAttr} {controlClass} {validacaoAttr}></textarea>");
                                    break;
                                case TipoCampo.Selecao:
                                    html.AppendLine($"        <select id='{campo.Nome}' name='{campo.Nome}'{requiredAttr} {controlClass} {validacaoAttr}>");
                                    foreach (var opcao in campo.Opcoes)
                                    {
                                        html.AppendLine($"          <option value='{opcao.ToLower()}'>{opcao}</option>");
                                    }
                                    html.AppendLine("        </select>");
                                    break;
                                case TipoCampo.Arquivo:
                                    html.AppendLine($"        <input type='file' id='{campo.Nome}' name='{campo.Nome}'{requiredAttr} {controlClass} {validacaoAttr}>");
                                    break;
                                case TipoCampo.Assinatura:
                                    string idCanvas = $"canvas_{campo.Nome}";
                                    string idBotaoLimpar = $"limpar_{campo.Nome}";

                                    html.AppendLine($"        <div class=\"campo-assinatura-wrapper\">");
                                    html.AppendLine($"          <canvas id=\"{idCanvas}\" class=\"campo-assinatura\"></canvas>");
                                    html.AppendLine($"          <input type=\"hidden\" id='{campo.Nome}' name='{campo.Nome}' {validacaoAttr} {requiredAttr} data-is-signed=\"false\">");
                                    html.AppendLine($"          <button type=\"button\" id=\"{idBotaoLimpar}\" class=\"btn-limpar-assinatura\">Limpar</button>");
                                    html.AppendLine($"        </div>");
                                    break;
                                default:
                                    string inputType = campo.Tipo.ToString().ToLower();
                                    if (campo.Tipo == TipoCampo.Senha) inputType = "password";
                                    if (campo.Tipo == TipoCampo.Email) inputType = "email";
                                    html.AppendLine($"        <input type='{inputType}' id='{campo.Nome}' name='{campo.Nome}'{placeholderAttr}{requiredAttr} {controlClass} {validacaoAttr}>");
                                    break;
                            }
                        }
                        html.AppendLine("        </div>"); // Fim .form-group
                    }
                    html.AppendLine("        </div>"); // Fim .form-row
                }
                html.AppendLine("      </div>"); // Fim .aba-painel
                primeiraAba = false;
            }
            html.AppendLine("      </div>"); // Fim .ficheiro-conteudo

            // --- 4. BOTÃO ENVIAR E SCRIPTS ---
            html.AppendLine("      <div class=\"form-group btn-submit-group\"><button type='submit' class=\"btn btn-primary\">Enviar Formulário</button></div>");
            html.AppendLine("    </form>");
            html.AppendLine("  </div>");

            // Adiciona o JS para as ABAS
            html.AppendLine(GerarScriptAbas());
            // Adiciona o JS para VALIDAÇÃO (igual ao Clássico)
            html.AppendLine(GerarScriptsJavaScript(precisaScriptCpf, precisaScriptData, precisaScriptCelular, precisaScriptEmail, precisaScriptAssinatura));

            html.AppendLine("</body></html>");
            return html.ToString();
        }

        // --- MÉTODO PARA O CSS DO "FICHEIRO" ---
        private string GerarCss(string corPrincipal)
        {
            // (Converte a cor para RGB, igual ao Clássico)
            string corRgb = "0, 123, 255";
            if (corPrincipal.Length == 7)
            {
                try
                {
                    int r = Convert.ToInt32(corPrincipal.Substring(1, 2), 16);
                    int g = Convert.ToInt32(corPrincipal.Substring(3, 2), 16);
                    int b = Convert.ToInt32(corPrincipal.Substring(5, 2), 16);
                    corRgb = $"{r}, {g}, {b}";
                }
                catch { /* Mantém o padrão */ }
            }

            // Retorna a string CSS completa
            return $$"""
                /* --- 1. RESET & GLOBAIS --- */
                * { box-sizing: border-box; margin: 0; padding: 0; }
                body {
                  font-family: Arial, sans-serif;
                  background-color: #f4f4f4;
                  color: #333;
                  margin: 20px;
                }

                /* --- 2. LAYOUT PRINCIPAL --- */
                .form-container {
                  max-width: 900px; 
                  margin: 0 auto;
                  padding: 25px;
                  background-color: #ffffff;
                  border-radius: 8px;
                  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
                }
                
                /* --- 3. CABEÇALHO (Reutilizado) --- */
                .form-header {
                  display: flex;
                  align-items: center;
                  gap: 24px; 
                  margin-bottom: 25px;
                  padding-bottom: 20px;
                  border-bottom: 1px solid #eee;
                }
                .form-logo { flex-shrink: 0; }
                .form-header-text { flex: 1; }
                .form-title {
                  text-align: left;
                  color: #333;
                  margin-bottom: 15px;
                  font-weight: bold;
                }
                .form-subtitle-box {
                  background-color: {{corPrincipal}};
                  color: #000;
                  padding: 10px 15px;
                  border-radius: 4px;
                  font-size: 0.9em;
                  line-height: 1.4;
                }

                /* --- 4. CAMPOS (Reutilizado) --- */
                .form-row {
                  display: flex;
                  flex-wrap: wrap; 
                  gap: 16px; 
                }
                .form-group {
                  margin-bottom: 20px;
                  flex-grow: 1;
                }
                .form-label {
                  display: block;
                  margin-bottom: 8px;
                  font-weight: bold;
                  color: #555;
                }
                .form-control {
                  width: 100%;
                  padding: 10px;
                  border: 1px solid #ccc;
                  border-radius: 4px;
                  transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
                }
                .form-control:focus {
                  border-color: {{corPrincipal}};
                  outline: 0;
                  box-shadow: 0 0 0 0.2rem rgba( {{corRgb}}, 0.25);
                }
                .checkbox-group {
                  display: flex;
                  align-items: center;
                }
                .checkbox-group .form-control { width: auto; margin-right: 10px; }
                .checkbox-group .form-label { margin-bottom: 0; font-weight: normal; }

                /* --- 5. BOTÃO ENVIAR (Reutilizado) --- */
                .btn {
                  width: 100%;
                  padding: 12px;
                  border: none;
                  border-radius: 4px;
                  cursor: pointer;
                  font-size: 16px;
                  font-weight: bold;
                  text-align: center;
                }
                .btn-primary {
                  background-color: {{corPrincipal}}; 
                  color: white;
                }
                .btn-primary:hover {
                  opacity: 0.85; 
                }
                .btn-submit-group {
                  margin-top: 25px; /* Espaço antes do botão de envio */
                }
                
                /* --- 6. VALIDAÇÃO (Reutilizado) --- */
                .form-control.invalid {
                  border-color: #dc3545; 
                  box-shadow: 0 0 0 0.2rem rgba(220, 53, 69, 0.25);
                }
                .validation-error {
                  color: #dc3545;
                  font-size: 0.875em;
                  margin-top: 5px;
                }
                .campo-assinatura.invalid {
                  border: 2px solid #dc3545; /* Borda vermelha de erro */
                  box-shadow: 0 0 0 0.2rem rgba(220, 53, 69, 0.25);
                }
                .campo-assinatura-wrapper {
                    position: relative;
                    width: 100%;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                }
                .campo-assinatura {
                    width: 100%;
                    height: 150px; /* Altura do campo */
                    cursor: crosshair;
                }
                .btn-limpar-assinatura {
                    position: absolute;
                    top: 5px;
                    right: 5px;
                    padding: 4px 8px;
                    font-size: 0.8em;
                    background: #f1f1f1;
                    border: 1px solid #ccc;
                    cursor: pointer;
                    border-radius: 4px;
                }
                .btn-limpar-assinatura:hover {
                    background: #e0e0e0;
                }

                /* --- 7. NOVOS ESTILOS DO LAYOUT FICHEIRO --- */
                
                /* A barra de abas */
                .ficheiro-abas {
                    overflow: hidden;
                    border: 1px solid #ccc;
                    background-color: #f1f1f1;
                    border-radius: 8px 8px 0 0; /* Arredonda o topo */
                }

                /* Os botões das abas */
                .ficheiro-abas .aba-link {
                    background-color: inherit;
                    float: left;
                    border: none;
                    border-right: 1px solid #ccc;
                    outline: none;
                    cursor: pointer;
                    padding: 14px 16px;
                    transition: 0.3s;
                    font-size: 1em;
                    font-weight: bold;
                    color: #555;
                }
                .ficheiro-abas .aba-link:last-child {
                    border-right: none;
                }

                /* Hover (mouse em cima) */
                .ficheiro-abas .aba-link:hover {
                    background-color: #ddd;
                }

                /* A aba ATIVA (selecionada) */
                .ficheiro-abas .aba-link.active {
                    background-color: {{corPrincipal}};
                    color: white;
                }

                /* O container do conteúdo (a "página" do ficheiro) */
                .ficheiro-conteudo {
                    border: 1px solid #ccc;
                    border-top: none;
                    padding: 20px;
                    border-radius: 0 0 8px 8px; /* Arredonda a base */
                }

                /* O painel de conteúdo de cada aba */
                .aba-painel {
                    display: none; /* Escondido por padrão */
                    animation: fadeIn 0.5s; /* Efeito de fade */
                }

                /* O painel ATIVO (selecionado) */
                .aba-painel.active {
                    display: block; /* Visível */
                }

                @keyframes fadeIn {
                    from { opacity: 0; }
                    to { opacity: 1; }
                }

                /* --- 8. RESPONSIVIDADE --- */
                @media (max-width: 768px) {
                  
                  body { margin: 0; }
                  .form-container { padding: 15px; border-radius: 0; box-shadow: none; }
                  .form-header { flex-direction: column; align-items: center; gap: 15px; }
                  .form-title { text-align: center; }
                  .form-subtitle-box { text-align: center; }
                  .form-row { flex-direction: column; gap: 0; }
                  
                  /* Abas responsivas */
                  .ficheiro-abas .aba-link {
                      float: none;
                      width: 100%;
                      display: block;
                      text-align: left;
                      border-right: none;
                      border-bottom: 1px solid #ccc;
                  }
                }
                """;
        }

        // --- MÉTODO PARA O JS DAS ABAS ---
        private string GerarScriptAbas()
        {
            return """
            <script>
            function abrirAba(evt, nomeAba) {
                var i, paineis, links;
                
                // 1. Esconde todos os painéis
                paineis = document.getElementsByClassName("aba-painel");
                for (i = 0; i < paineis.length; i++) {
                    paineis[i].style.display = "none";
                    paineis[i].classList.remove("active");
                }
                
                // 2. Remove a classe "active" de todos os botões (links)
                links = document.getElementsByClassName("aba-link");
                for (i = 0; i < links.length; i++) {
                    links[i].classList.remove("active");
                }
                
                // 3. Mostra o painel clicado e ativa o botão
                document.getElementById(nomeAba).style.display = "block";
                document.getElementById(nomeAba).classList.add("active");
                evt.currentTarget.classList.add("active");
            }
            </script>
            """;
        }


        // --- MÉTODOS DE VALIDAÇÃO JS (Reutilizados do Layout Clássico) ---

        private string GerarScriptsJavaScript(bool cpf, bool data, bool celular, bool email, bool assinatura)
        {
            if (!cpf && !data && !celular && !email && !assinatura)
                return string.Empty;

            var script = new StringBuilder();
            script.AppendLine("<script>");
            script.AppendLine("document.addEventListener('DOMContentLoaded', function() {");

            if (cpf || email || assinatura)
            {
                script.AppendLine(FuncaoMostrarErro);
                script.AppendLine(FuncaoRemoverErro);
            }
            if (cpf)
            {
                script.AppendLine(FuncaoValidarCPF);
                script.AppendLine(LogicaHookValidacao("cpf", "validarCPF", "'CPF inválido.'"));
            }
            if (email)
            {
                script.AppendLine(FuncaoValidarEmail);
                script.AppendLine(LogicaHookValidacao("email", "validarEmail", "'Formato de e-mail inválido.'"));
            }
            if (data)
            {
                script.AppendLine(LogicaMascaraData);
            }
            if (celular)
            {
                script.AppendLine(LogicaMascaraCelular);
            }
            if (assinatura)
            {
                script.AppendLine(LogicaAssinaturaCanvas);
                script.AppendLine(LogicaValidacaoSubmit);
            }

            script.AppendLine("});");
            script.AppendLine("</script>");
            return script.ToString();
        }

        // --- Funções Utilitárias de Erro ---
        private readonly string FuncaoMostrarErro = """
            function mostrarErro(input, mensagem) {
                removerErro(input); // Remove erros antigos
                input.classList.add('invalid');

                const formGroup = input.closest('.form-group');

                const divErro = document.createElement('div');
                divErro.className = 'validation-error';
                divErro.textContent = mensagem;

                input.parentNode.insertBefore(divErro, input.nextSibling);
                formGroup.appendChild(divErro);
            }
        """;

        private readonly string FuncaoRemoverErro = """
            function removerErro(input) {
                input.classList.remove('invalid');
                const formGroup = input.closest('.form-group')
                const erroAntigo = formGroup.querySelector('.validation-error');
                if (erroAntigo) {
                    erroAntigo.remove();
                }
            }
        """;

        private string LogicaHookValidacao(string seletor, string nomeFuncaoValidadora, string mensagemErro)
        {
            return $$"""
                document.querySelectorAll('input[data-validacao="{{seletor}}"]').forEach(input => {
                    input.addEventListener('blur', function() {
                        const ehValido = {{nomeFuncaoValidadora}}(input.value);
                        removerErro(input);
                        if (!ehValido && input.value.length > 0) {
                            mostrarErro(input, {{mensagemErro}});
                        }
                    });
                });
            """;
        }

        private readonly string FuncaoValidarCPF = """
            function validarCPF(cpf) {
                cpf = cpf.replace(/[^\d]+/g, '');
                if (cpf == '' || cpf.length != 11 || /^(\d)\1{10}$/.test(cpf)) return false;
                let add = 0, rev;
                for (let i = 0; i < 9; i++) add += parseInt(cpf.charAt(i)) * (10 - i);
                rev = 11 - (add % 11);
                if (rev == 10 || rev == 11) rev = 0;
                if (rev != parseInt(cpf.charAt(9))) return false;
                add = 0;
                for (let i = 0; i < 10; i++) add += parseInt(cpf.charAt(i)) * (11 - i);
                rev = 11 - (add % 11);
                if (rev == 10 || rev == 11) rev = 0;
                if (rev != parseInt(cpf.charAt(10))) return false;
                return true;
            }
        """;

        private readonly string FuncaoValidarEmail = """
            function validarEmail(email) {
                const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(String(email).toLowerCase());
            }
        """;

        private readonly string LogicaMascaraData = """
            document.querySelectorAll('input[data-validacao="data"]').forEach(input => {
                input.setAttribute('maxlength', '10');
                input.addEventListener('input', function(e) {
                    let v = e.target.value.replace(/\D/g, '').substring(0, 8);
                    if (v.length > 4) v = v.replace(/(\d{2})(\d{2})(\d{1,4})/, '$1/$2/$3');
                    else if (v.length > 2) v = v.replace(/(\d{2})(\d{1,2})/, '$1/$2');
                    e.target.value = v;
                });
            });
        """;

        private readonly string LogicaMascaraCelular = """
            document.querySelectorAll('input[data-validacao="celular"]').forEach(input => {
                input.setAttribute('maxlength', '15'); // (XX) 9XXXX-XXXX
                input.addEventListener('input', function(e) {
                    let v = e.target.value.replace(/\D/g, '').substring(0, 11);
                    if (v.length > 10) v = v.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
                    else if (v.length > 6) v = v.replace(/(\d{2})(\d{4})(\d{0,4})/, '($1) $2-$3');
                    else if (v.length > 2) v = v.replace(/(\d{2})(\d{0,5})/, '($1) $2');
                    else if (v.length > 0) v = v.replace(/(\d{0,2})/, '($1');
                    e.target.value = v;
                });
            });
        """;
        private readonly string LogicaAssinaturaCanvas = """
            function inicializarAssinatura(inputOculto) {
                const wrapper = inputOculto.closest('.campo-assinatura-wrapper');
                const canvas = wrapper.querySelector('canvas');
                const botaoLimpar = wrapper.querySelector('.btn-limpar-assinatura');
                const ctx = canvas.getContext('2d');
                let desenhando = false;
                
                // Define o tamanho do canvas (importante para o 'toDataURL')
                // (O CSS controla o tamanho de exibição, isto controla a resolução)
                canvas.width = canvas.offsetWidth;
                canvas.height = canvas.offsetHeight;
                
                ctx.strokeStyle = '#000'; // Cor da caneta
                ctx.lineWidth = 2;

                function getPos(e) {
                    const rect = canvas.getBoundingClientRect();
                    let scaleX = canvas.width / rect.width;
                    let scaleY = canvas.height / rect.height;
                    
                    if (e.touches && e.touches.length > 0) {
                        return {
                            x: (e.touches[0].clientX - rect.left) * scaleX,
                            y: (e.touches[0].clientY - rect.top) * scaleY
                        };
                    }
                    return {
                        x: (e.clientX - rect.left) * scaleX,
                        y: (e.clientY - rect.top) * scaleY
                    };
                }

                function iniciarDesenho(e) {
                    e.preventDefault();
                    desenhando = true;
                    const pos = getPos(e);
                    ctx.beginPath();
                    ctx.moveTo(pos.x, pos.y);
                }

                function desenhar(e) {
                    if (!desenhando) return;
                    e.preventDefault();
                    const pos = getPos(e);
                    ctx.lineTo(pos.x, pos.y);
                    ctx.stroke();
                }

                function pararDesenho() {
                    if (!desenhando) return;
                    desenhando = false;
                    // ATUALIZA O INPUT OCULTO
                    inputOculto.value = canvas.toDataURL('image/png');

                    inputOculto.dataset.isSigned = "true";
                }

                function limparCanvas() {
                    ctx.clearRect(0, 0, canvas.width, canvas.height);
                    inputOculto.value = ''; // Limpa o valor
                }

                // Eventos de Mouse
                canvas.addEventListener('mousedown', iniciarDesenho);
                canvas.addEventListener('mousemove', desenhar);
                canvas.addEventListener('mouseup', pararDesenho);
                canvas.addEventListener('mouseout', pararDesenho);
                
                // Eventos de Toque (para celular)
                canvas.addEventListener('touchstart', iniciarDesenho);
                canvas.addEventListener('touchmove', desenhar);
                canvas.addEventListener('touchend', pararDesenho);
                
                // Evento do Botão Limpar
                botaoLimpar.addEventListener('click', limparCanvas);
            }

            // Inicia todos os campos de assinatura
            document.querySelectorAll('input[data-validacao="assinatura"]').forEach(inicializarAssinatura);
        """;

        private readonly string LogicaValidacaoSubmit = """
            document.querySelectorAll('form').forEach(form => {
                form.addEventListener('submit', function(event) {
                    let formularioEhValido = true;
                    
                    // --- Validação de Assinatura Obrigatória ---
                    // Encontra todos os inputs de assinatura que têm o atributo 'required'
                    form.querySelectorAll('input[data-validacao="assinatura"][required]').forEach(input => {
                        
                        // Encontra o canvas (o elemento visível)
                        const canvas = input.closest('.campo-assinatura-wrapper').querySelector('canvas');
                        removerErro(canvas); // Limpa erros antigos (do canvas)
                        
                        if (input.dataset.isSigned === 'false') {
                            // O campo oculto está vazio!
                            formularioEhValido = false;
                            
                            // Mostra o erro no canvas (o elemento visível)
                            mostrarErro(canvas, 'A assinatura é obrigatória.');
                        }
                    });
                    
                    // (Aqui você pode adicionar mais validações de 'submit' no futuro)

                    // --- Decisão Final ---
                    if (!formularioEhValido) {
                        event.preventDefault(); // PARA O ENVIO DO FORMULÁRIO!
                        alert('Por favor, corrija os campos obrigatórios antes de enviar.');
                    }
                });
            });
        """;
    }
}