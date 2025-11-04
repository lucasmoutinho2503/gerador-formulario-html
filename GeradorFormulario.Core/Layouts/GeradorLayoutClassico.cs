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
            html.AppendLine("    </form>");
            html.AppendLine("  </div>");

            string scripts = GerarScriptsJavaScript(precisaScriptCpf, precisaScriptData, precisaScriptCelular, precisaScriptEmail, precisaScriptAssinatura);
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

                .form-header {
                  display: flex;
                  align-items: center; /* Alinha logo e texto verticalmente */
                  gap: 24px; /* Espaço entre o logo e o texto */
                  margin-bottom: 25px;
                  padding-bottom: 20px;
                  border-bottom: 1px solid #eee; /* Linha divisória fina */
                }

                .form-logo {
                  flex-shrink: 0; /* Impede o logo de encolher */
                }

                .form-header-text {
                  flex: 1; /* Ocupa o espaço restante */
                }

                /* --- 2. COMPONENTES (com Tema) --- */
                .form-container {
                  max-width: 900px;
                  margin: 0 auto;
                  padding: 25px;
                  background-color: #ffffff;
                  border-radius: 8px;
                  box-shadow: 0 4px 10px rgba(0,0,0,0.1);
                }
                
                .form-title {
                  text-align: left; /* Alinhado à esquerda (como na imagem) */
                  color: #333; /* Cor escura, não a principal */
                  margin-bottom: 15px;
                  font-weight: bold;
                }

                .form-subtitle-box {
                  background-color: {{corPrincipal}}; /* Cor principal (laranja) */
                  color: #000; /* Texto escuro para bom contraste */
                  padding: 10px 15px;
                  border-radius: 4px;
                  font-size: 0.9em;
                  line-height: 1.4;
                }
                
                .form-section {
                  border: 1px solid #ddd;
                  border-radius: 8px;
                  padding: 20px;
                  margin-bottom: 25px;
                }
                
                .form-section-title {
                  font-size: 1.2em;
                  font-weight: bold;
                  color: {{corPrincipal}}; /* <-- MUDANÇA: Chaves duplas */
                  padding: 0 10px;
                  margin-top: -30px; 
                  background: #fff;
                  display: inline-block;
                  margin-left: 10px;
                }

                /* NOVO: Flexbox para as Linhas */
                .form-row {
                  display: flex;
                  flex-wrap: wrap; 
                  gap: 16px; /* Espaçamento entre as colunas */
                }

                .form-group {
                  margin-bottom: 10px;
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
                  border-color: {{corPrincipal}}; /* <-- MUDANÇA: Chaves duplas */
                  outline: none;
                }
                .checkbox-group {
                  display: flex;
                  align-items: center;
                }

                /* Botões (com Tema) */
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
                  background-color: {{corPrincipal}}; /* <-- MUDANÇA: Chaves duplas */
                  color: white;
                }
                
                .btn-primary:hover {
                  opacity: 0.85; /* Efeito de hover simples */
                }

                /* Estilos de Validação */
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

                @media (max-width: 425px) {
                  
                  body {
                    margin: 0; /* Remove margens no celular */
                  }

                  .form-container {
                    padding: 15px; /* Padding menor */
                    border-radius: 0;
                    box-shadow: none;
                  }

                  /* Empilha o cabeçalho no celular */
                  .form-header {
                    flex-direction: column; /* Empilha o logo e o texto */
                    align-items: center;    /* Centraliza */
                    gap: 15px;
                  }

                  .form-title {
                    text-align: center; /* Centraliza o título no celular */
                  }
                  
                  .form-subtitle-box {
                    text-align: center;
                  }

                  /* Empilha as colunas (CPF/RG, Bairro/Cidade/CEP) */
                  .form-row {
                    flex-direction: column; /* Empilha os campos */
                    gap: 0; /* Remove o gap, já que estão um sobre o outro */
                  }
                  
                  /* O 'style="flex: X;"' não afeta a direção de coluna, 
                     então os campos naturalmente ocupam 100% da largura. */

                  /* Ajuste no título da seção para mobile */
                  .form-section {
                    padding: 20px 15px;
                  }
                  
                  .form-section-title {
                    margin-top: 0;
                    margin-left: 0;
                    margin-bottom: 20px;
                    display: block; /* Vira um bloco normal */
                    width: 100%;
                    background: transparent;
                    border-bottom: 2px solid {{corPrincipal}};
                    padding-bottom: 5px;
                  }

                """;
        }
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
                script.AppendLine(LogicaValidacaoSubmit);
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

