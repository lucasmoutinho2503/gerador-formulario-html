using GeradorFormulario.Core.Enums;
using GeradorFormulario.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeradorFormulario.Core.Layouts
{
    public class GeradorLayoutClassico : IGeradorLayout
    {
        public string GerarHtml(DefinicaoFormulario definicao)
        {
            StringBuilder html = new StringBuilder();
            bool precisaScriptCpf = false;
            bool precisaScriptData = false;
            bool precisaScriptCelular = false;
            bool precisaScriptEmail = false;
            bool precisaScriptAssinatura = false;

            bool precisaEnctype = definicao.Secoes.SelectMany(s => s.Linhas)
                                                 .SelectMany(l => l.Campos)
                                                 .Any(c => c.Tipo == TipoCampo.Arquivo) ||
                                  definicao.Conexao != null; ;

            html.AppendLine("<!DOCTYPE html><html lang=\"pt-br\"><head>");
            html.AppendLine("  <meta charset=\"UTF-8\">");
            html.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine($"  <title>{definicao.NomeFormulario}</title>");
            html.AppendLine("  <style>");
            html.AppendLine(GerarCss(definicao.CorPrincipal));
            html.AppendLine("  </style>");
            html.AppendLine("</head><body>");

            html.AppendLine($"  <div class=\"form-container\">");

            if (!string.IsNullOrEmpty(definicao.UrlLogo) || !string.IsNullOrEmpty(definicao.TituloHeader))
            {
                html.AppendLine("    <div class=\"form-header\">");

                if (!string.IsNullOrEmpty(definicao.UrlLogo))
                {
                    html.AppendLine("      <div class=\"form-logo\">");
                    html.AppendLine($"        <img src=\"{definicao.UrlLogo}\" alt=\"Logo da Organização\" style=\"width:140px; height:140px; object-fit:contain;\">");
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
            string enctypeAttr = precisaEnctype ? " enctype=\"multipart/form-data\"" : "";

            string targetAttr = definicao.Conexao != null && !string.IsNullOrEmpty(definicao.Conexao.Target)
                                ? $" target=\"{definicao.Conexao.Target}\""
                                : "";

            string actionAttr = definicao.Conexao != null && !string.IsNullOrEmpty(definicao.Conexao.UrlAcao)
                                ? $" action=\"{definicao.Conexao.UrlAcao}\""
                                : "";

            html.AppendLine($"    <form method='{definicao.Metodo.ToLower()}'{enctypeAttr}{actionAttr}{targetAttr}>");

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

            foreach (var secao in definicao.Secoes)
            {
                html.AppendLine("      <fieldset class=\"form-section\">");
                if (!string.IsNullOrEmpty(secao.Titulo))
                {
                    html.AppendLine($"        <legend class=\"form-section-title\">{secao.Titulo}</legend>");
                }

                foreach (var linha in secao.Linhas)
                {
                    html.AppendLine("        <div class=\"form-row\">");

                    foreach (var campo in linha.Campos)
                    {

                        html.AppendLine($"        <div class=\"form-group\" style=\"flex: {campo.TamanhoColuna};\">");

                        string requiredAttr = campo.Obrigatorio ? " required" : "";
                        string placeholderAttr = !string.IsNullOrEmpty(campo.Placeholder) ? $" placeholder='{campo.Placeholder}'" : "";

                        string validacaoAttr = "";
                        if (!string.IsNullOrEmpty(campo.ValidacaoEspecial))
                        {
                            validacaoAttr = $" data-validacao='{campo.ValidacaoEspecial}'";

                            if (campo.ValidacaoEspecial == "cpf") precisaScriptCpf = true;
                            if (campo.ValidacaoEspecial == "data") precisaScriptData = true;
                            if (campo.ValidacaoEspecial == "celular") precisaScriptCelular = true;
                            if (campo.ValidacaoEspecial == "email") precisaScriptEmail = true;
                            if (campo.ValidacaoEspecial == "assinatura") precisaScriptAssinatura = true;
                        }

                        if (campo.Tipo == TipoCampo.CaixaDeSelecao)
                        {
                            html.AppendLine("          <div class=\"checkbox-group\">");
                            html.AppendLine($"            <input type='checkbox' id='{campo.Nome}' name='{campo.Nome}'{requiredAttr} class=\"form-control\" {validacaoAttr}>");
                            html.AppendLine($"            <label for='{campo.Nome}' class=\"form-label\">{campo.Rotulo}</label>");
                            html.AppendLine("          </div>");
                        }
                        else
                        {
                            html.AppendLine($"        <label for='{campo.Nome}' class=\"form-label\">{campo.Rotulo}:</label>");

                            string controlClass = "class=\"form-control\"";

                            switch (campo.Tipo)
                            {
                                case TipoCampo.AreaDeTexto:
                                    html.AppendLine($"        <textarea id='{campo.Nome}' name='{campo.Nome}'{placeholderAttr}{requiredAttr} {controlClass} {validacaoAttr}></textarea>");
                                    break;
                                case TipoCampo.Selecao:
                                    html.AppendLine($"        <select id='{campo.Nome}' name='{campo.Nome}'{requiredAttr} {controlClass} {validacaoAttr}>");
                                    foreach (var opcao in campo.Opcoes)
                                    {
                                        if (string.IsNullOrEmpty(opcao))
                                        {
                                            html.AppendLine("          <option value='' selected hidden >Selecione...</option>");
                                            continue;
                                        }
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
                                    html.AppendLine($"        <input type='{inputType}' id='{campo.Nome}' name='{campo.Nome}'{placeholderAttr}{requiredAttr} {controlClass} {validacaoAttr}>");
                                    break;
                            }
                        }

                        html.AppendLine("        </div>"); // Fim de .form-group
                    }
                    html.AppendLine("        </div>"); // Fim de .form-row
                }
                html.AppendLine("      </fieldset>");
            }

            html.AppendLine("      <div class=\"form-group\"><button type='submit' class=\"btn btn-primary\">Enviar</button></div>");
            
            //Rodapé
            if (!string.IsNullOrEmpty(definicao.TextoRodape))
            {
                html.AppendLine("      <div class=\"form-footer\">");
                html.AppendLine($"        <p>{definicao.TextoRodape.Replace("\n", "<br />")}</p>");
                html.AppendLine("      </div>");
            }
            html.AppendLine("    </form>");
            html.AppendLine("  </div>");

            if (definicao.Termos != null && definicao.Termos.Habilitado)
            {
                html.AppendLine("");
                html.AppendLine("<div id=\"termosModalBackdrop\" class=\"modal-backdrop\">");
                html.AppendLine("  <div class=\"modal-content\">");
                html.AppendLine("    <div class=\"modal-header-container\">");
                // Imagem (se houver URL)
                if (!string.IsNullOrEmpty(definicao.Termos.UrlImagemTitulo))
                {
                    html.AppendLine($"      <img src=\"{definicao.Termos.UrlImagemTitulo}\" alt=\"\" class=\"modal-header-img\">");
                }
                html.AppendLine($"      <h2 class=\"modal-title\">{definicao.Termos.TituloModal}</h2>");
                html.AppendLine("    </div>"); // Fim do modal-header-container
                html.AppendLine("    <div class=\"modal-body\">");
                // Injeta o HTML que o usuário escreveu (com <b>, <img>, etc.)
                html.AppendLine(definicao.Termos.ConteudoHtml);
                html.AppendLine("    </div>");
                html.AppendLine("    <div class=\"modal-footer\">");
                html.AppendLine($"      <p class=\"modal-confirm-text\">{definicao.Termos.MensagemConfirmacao}</p>");
                html.AppendLine("      <div class=\"btn-container\">");
                html.AppendLine("        <button type=\"button\" id=\"btnDiscordoTermos\" class=\"btn btn-secondary\">Discordo</button>");
                html.AppendLine("        <button type=\"button\" id=\"btnConcordoTermos\" class=\"btn btn-primary\">Concordo e Enviar</button>");
                html.AppendLine("      </div>"); // Fim do btn-container
                html.AppendLine("    </div>");
                html.AppendLine("  </div>");
                html.AppendLine("</div>");
            }

            string scripts = GerarScriptsJavaScript(precisaScriptCpf, precisaScriptData, precisaScriptCelular, precisaScriptEmail, precisaScriptAssinatura, (definicao.Termos != null && definicao.Termos.Habilitado));
            if (!string.IsNullOrEmpty(scripts))
            {
                html.AppendLine(scripts);
            }

            html.AppendLine("</body></html>");
            return html.ToString();
        }
        public string GerarCss(string corPrincipal)
        {
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
                catch { }
            }
            return $$"""
                /* --- 1. RESET & GLOBAIS --- */
                * { box-sizing: border-box; margin: 0; padding: 0; }
                body {
                  font-family: Arial, sans-serif;
                  background-color: #f4f4f4;
                  color: #333;
                  margin: 20px;
                }

                /* --- 2. CABEÇALHO --- */
                .form-header {
                  display: flex;
                  align-items: center; 
                  gap: 24px; 
                  margin-bottom: 25px;
                  padding-bottom: 20px;
                  border-bottom: 1px solid #eee; 
                }
                .form-logo {
                  flex-shrink: 0; 
                }
                .form-header-text {
                  flex: 1; 
                }

                /* --- 3. CONTAINER PRINCIPAL --- */
                .form-container {
                  max-width: 900px;
                  margin: 0 auto;
                  padding: 25px;
                  background-color: #ffffff;
                  border-radius: 8px;
                  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
                }

                /* --- 4. TÍTULOS E SEÇÕES --- */
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
                .form-section {
                  border: 1px solid #ddd;
                  border-radius: 8px;
                  padding: 20px;
                  padding-top: 30px; /* Mais espaço para o <legend> "pular" para cima */
                  margin-bottom: 25px;
                }
                .form-section-title {
                  font-size: 1.2em;
                  font-weight: bold;
                  color: {{corPrincipal}}; 
                  padding: 0 10px;
                  margin-top: -30px; 
                  background: #fff;
                  display: inline-block;
                  margin-left: 10px;
                }

                /* --- 5. CAMPOS E LAYOUT --- */
                .form-row {
                  display: flex;
                  flex-wrap: wrap; 
                  gap: 16px; /* Espaçamento entre as colunas */
                }
                .form-group {
                  margin-bottom: 10px;
                  /* A largura é controlada pelo 'style="flex: X;"' no HTML */
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
                  outline: 0; /* Remove o outline padrão */
                  /* Adiciona o "brilho" da cor principal (estava faltando no seu) */
                  box-shadow: 0 0 0 0.2rem rgba( {{corRgb}}, 0.25); 
                }
                textarea.form-control {
                  min-height: 120px; 
                  resize: vertical; 
                }
                .checkbox-group {
                  display: flex;
                  align-items: center;
                }

                /* --- 6. CAMPO DE ASSINATURA --- */
                .campo-assinatura-wrapper {
                    position: relative;
                    width: 100%;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                }
                .campo-assinatura {
                    width: 100%;
                    height: 150px; 
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

                /* --- 7. BOTÕES --- */
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
                .btn-secondary {
                    background-color: #6c757d;
                    color: white;
                }
                .btn-secondary:hover {
                    background-color: #5a6268;
                }

                /* --- 8. VALIDAÇÃO DE ERRO --- */
                .form-control.invalid {
                  border-color: #dc3545; 
                  box-shadow: 0 0 0 0.2rem rgba(220, 53, 69, 0.25);
                }
                .campo-assinatura.invalid {
                  border: 2px solid #dc3545; /* Borda vermelha de erro */
                  box-shadow: 0 0 0 0.2rem rgba(220, 53, 69, 0.25);
                }
                .validation-error {
                  color: #dc3545;
                  font-size: 0.875em;
                  margin-top: 5px;
                }
                .form-footer {
                  text-align: center;
                  font-size: 0.9em;
                  color: #888; /* Cor cinza sutil */
                  margin-top: 30px;
                  padding-top: 20px;
                  border-top: 1px solid #eee; /* Linha divisória fina */
                }
                .form-footer p {
                  margin: 0;
                  line-height: 1.5;
                }
        
                /* --- 9. MODAL DE TERMOS --- */
                .modal-backdrop {
                    display: none; 
                    position: fixed; 
                    z-index: 1000;
                    left: 0;
                    top: 0;
                    width: 100%;
                    height: 100%;
                    overflow: auto;
                    background-color: rgba(0,0,0,0.5); 
                    justify-content: center;
                    align-items: center;
                }
                .modal-backdrop.show {
                    display: flex; 
                }
                .modal-content {
                    background-color: #fefefe;
                    margin: auto;
                    padding: 20px;
                    border: 1px solid #888;
                    width: 80%;
                    max-width: 900px;
                    border-radius: 8px;
                    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
                    animation: fadeIn 0.3s;
                }
                .modal-header-container {
                    display: flex; 
                    align-items: center; 
                    gap: 15px; 
                    border-bottom: 1px solid #eee;
                    padding-bottom: 10px;
                    margin-bottom: 10px; 
                }
                .modal-header-img {
                    max-width: 60px; 
                    height: auto;
                    flex-shrink: 0; 
                }
                .modal-title {
                    font-size: 1.5em;
                    font-weight: bold;
                    color: #333;
                    /* Limpo (a borda está no container) */
                    border-bottom: none;
                    padding-bottom: 0;
                }
                .modal-body {
                    padding: 15px 0;
                    max-height: 400px; 
                    overflow-y: auto; 
                    overflow-wrap: break-word;
                    word-wrap: break-word;
                }
                .modal-body img {
                    max-width: 100%;
                    height: auto;
                }
                .modal-footer {
                    display: flex; 
                    justify-content: space-between; 
                    align-items: center; 
                    border-top: 1px solid #eee;
                    padding-top: 15px;
                    margin-top: 15px; 
                }
                .modal-confirm-text {
                    font-size: 0.9em;
                    color: #555;
                    text-align: left;
                    flex: 1; 
                    margin-right: 20px; 
                }
                .modal-footer .btn-container {
                    flex-shrink: 0; 
                }
                .modal-footer .btn {
                    width: auto; 
                    display: inline-block;
                    margin-left: 10px;
                }

                @keyframes fadeIn {
                    from { opacity: 0; }
                    to { opacity: 1; }
                }


                /* --- 10. RESPONSIVIDADE (PONTO DE QUEBRA CORRIGIDO) --- */
        
                /* MUDANÇA: de 425px para 768px */
                @media (max-width: 768px) {
          
                  body {
                    margin: 0; 
                  }
                  .form-container {
                    padding: 15px; 
                    border-radius: 0;
                    box-shadow: none;
                    max-width: 100%; /* Ocupa a tela toda */
                  }

                  /* Empilha o cabeçalho */
                  .form-header {
                    flex-direction: column; 
                    align-items: center;   
                    gap: 15px;
                  }
                  .form-title {
                    text-align: center; 
                  }
                  .form-subtitle-box {
                    text-align: center;
                  }

                  /* Empilha as colunas */
                  .form-row {
                    flex-direction: column; 
                    gap: 0; 
                  }
          
                  /* Ajuste no título da seção para mobile */
                  .form-section {
                    padding: 20px 15px;
                  }
                  .form-section-title {
                    margin-top: 0;
                    margin-left: 0;
                    margin-bottom: 20px;
                    display: block; 
                    width: 100%;
                    background: transparent;
                    border-bottom: 2px solid {{corPrincipal}};
                    padding-bottom: 5px;
                  }
          
                  /* Empilha o footer do modal */
                  .modal-footer {
                    flex-direction: column; 
                    align-items: stretch; 
                    gap: 15px;
                  }
                  .modal-confirm-text {
                    text-align: center; 
                    margin-right: 0;
                  }
                  .modal-footer .btn-container {
                    display: flex;
                    flex-direction: column; 
                    gap: 10px;
                  }
                  .modal-footer .btn {
                    width: 100%; 
                    margin-left: 0;
                  }
                }
                """;
        }
        private string GerarScriptsJavaScript(bool cpf, bool data, bool celular, bool email, bool assinatura, bool termos)
        {
            if (!cpf && !data && !celular && !email && !assinatura && !termos)
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
                script.AppendLine(LogicaHookValidacao("cpf", "validarCPF"));
            }

            if (email)
            {
                script.AppendLine(FuncaoValidarEmail);
                script.AppendLine(LogicaHookValidacao("email", "validarEmail"));
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
            }
            if (assinatura || termos)
            {
                script.AppendLine(LogicaValidacaoSubmit(assinatura, termos));
            }

            script.AppendLine("});");
            script.AppendLine("</script>");
            return script.ToString();
        }

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

        private string LogicaHookValidacao(string seletor, string nomeFuncaoValidadora)
        {
            return $$"""
                document.querySelectorAll('input[data-validacao="{{seletor}}"]').forEach(input => {
                    input.addEventListener('blur', function() {
                        const ehValido = {{nomeFuncaoValidadora}}(input.value);
                        removerErro(input);
                        if (!ehValido && input.value.length > 0) {
                            mostrarErro(input, 'Valor inválido.');
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
                    ctx.closePath();
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

        private string LogicaValidacaoSubmit(bool checarAssinatura, bool checarTermos)
        {
            var sb = new StringBuilder();
            sb.AppendLine("document.querySelectorAll('form').forEach(form => {");
            sb.AppendLine("  form.addEventListener('submit', function(event) {");
            sb.AppendLine("    event.preventDefault(); // <-- 1. SEMPRE impede o envio padrão");

            // --- Lógica dos Termos (Prioridade 1) ---
            if (checarTermos)
            {
                sb.AppendLine("    const modal = document.getElementById('termosModalBackdrop');");
                sb.AppendLine("    if (modal && modal.dataset.concordado !== 'true') {");
                sb.AppendLine("        modal.style.display = 'flex';"); // Adicionado ';' (apesar de opcional, é boa prática)
                sb.AppendLine("        return;"); // Para o script aqui. Não valida nem envia.
                sb.AppendLine("    }");
            }

            // --- Lógica da Validação (Prioridade 2) ---
            sb.AppendLine("    let formularioEhValido = true;");
            if (checarAssinatura)
            {
                sb.AppendLine("    form.querySelectorAll('input[data-validacao=\"assinatura\"][required]').forEach(input => {");
                sb.AppendLine("        const canvas = input.closest('.campo-assinatura-wrapper').querySelector('canvas');");
                sb.AppendLine("        removerErro(canvas);");
                sb.AppendLine("        if (input.dataset.isSigned === 'false') {");
                sb.AppendLine("            formularioEhValido = false;");
                sb.AppendLine("            mostrarErro(canvas, 'A assinatura é obrigatória.');");
                sb.AppendLine("        }");
                sb.AppendLine("    });");
            }

            // --- Decisão da Validação ---
            sb.AppendLine("    if (!formularioEhValido) {");
            sb.AppendLine("        alert('Por favor, corrija os campos obrigatórios antes de enviar.');");
            sb.AppendLine("        return;"); // Para o script aqui. Não envia.
            sb.AppendLine("    }");

            sb.AppendLine("    if (!form.action || form.action === window.location.href) {");
            sb.AppendLine("        alert('Erro de Configuração: O formulário não tem uma URL de destino (API) definida.');");
            sb.AppendLine("        return; // Para o script aqui");
            sb.AppendLine("    }");

            // --- LÓGICA DE ENVIO (Prioridade 3) ---
            // (Este bloco foi MOVIDO para DENTRO do 'submit' listener)
            sb.AppendLine("    const formData = new FormData(form);");
            sb.AppendLine("    fetch(form.action, {");
            sb.AppendLine("        method: form.method,");
            sb.AppendLine("        body: formData,");
            sb.AppendLine("        headers: { 'Accept': 'application/json' }");
            sb.AppendLine("    })");
            sb.AppendLine("    .then(response => {");
            sb.AppendLine("        if (response.ok) {");
            // Correção do nome do arquivo
            sb.AppendLine("            window.location.href = 'confirmacao.html';");
            sb.AppendLine("        } else {");
            sb.AppendLine("            return response.json().then(err => { throw new Error(err.message || 'Erro desconhecido'); });");
            sb.AppendLine("        }");
            sb.AppendLine("    })");
            sb.AppendLine("    .catch(error => {");
            sb.AppendLine("        alert('Ocorreu um erro ao enviar o formulário: ' + error.message);");
            sb.AppendLine("    });");
            // (Fim do bloco movido)

            sb.AppendLine("  });"); // Fim do 'submit' event
            sb.AppendLine("});"); // Fim do 'forEach(form)'

            // --- Lógica dos Botões do Modal (Separado) ---
            if (checarTermos)
            {
                sb.AppendLine("const modal = document.getElementById('termosModalBackdrop');");
                sb.AppendLine("if (modal) {");
                sb.AppendLine("  const btnDiscordo = document.getElementById('btnDiscordoTermos');");
                sb.AppendLine("  const btnConcordo = document.getElementById('btnConcordoTermos');");
                sb.AppendLine("  const form = document.querySelector('form');");

                sb.AppendLine("  btnDiscordo.addEventListener('click', () => { modal.style.display = 'none'; });");

                sb.AppendLine("  btnConcordo.addEventListener('click', () => {");
                sb.AppendLine("      modal.style.display = 'none';");
                sb.AppendLine("      modal.dataset.concordado = 'true';");
                // CORREÇÃO: Usa requestSubmit() para re-disparar o evento 'submit'
                // (o que faz nossa validação rodar de novo)
                sb.AppendLine("      form.requestSubmit();");
                sb.AppendLine("  });");
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        public string GerarHtmlConfirmacao(DefinicaoFormulario definicao)
        {
            var config = definicao.Confirmacao;
            var corPrincipal = definicao.CorPrincipal; // Reusa a cor do tema

            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html><html lang=\"pt-br\"><head>");
            html.AppendLine("  <meta charset=\"UTF-8\">");
            html.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine($"  <title>{config.TituloPagina}</title>");
            html.AppendLine("  <style>");
            // CSS Simples para a página de "confirmacao"
            html.AppendLine($$"""
                body { font-family: Arial, sans-serif; background-color: #f4f4f4; display: flex; justify-content: center; align-items: center; min-height: 90vh; }
                .confirm-box { background-color: #fff; padding: 40px; border-radius: 8px; box-shadow: 0 4px 10px rgba(0,0,0,0.1); max-width: 600px; text-align: center; }
                .confirm-box h1 { color: {{corPrincipal}}; }
                .confirm-box p { color: #333; line-height: 1.6; }
                .confirm-box a { display: inline-block; margin-top: 20px; padding: 10px 20px; background-color: {{corPrincipal}}; color: white; text-decoration: none; border-radius: 4px; }
            """);
            html.AppendLine("  </style>");
            html.AppendLine("</head><body>");
            html.AppendLine("  <div class=\"confirm-box\">");
            // Injeta o HTML que o usuário escreveu
            html.AppendLine(config.ConteudoHtml);
            html.AppendLine("    <a href=\"javascript:history.back()\">Voltar</a>");
            html.AppendLine("  </div>");
            html.AppendLine("</body></html>");

            return html.ToString();
        }
    }
}

