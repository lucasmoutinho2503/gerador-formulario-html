using GeradorFormulario.Core;
using GeradorFormulario.Core.Enums;
using GeradorFormulario.Core.Fabrica;
using GeradorFormulario.Core.Models;
using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        var meuFormulario = new DefinicaoFormulario
        {
            NomeFormulario = "Cadastro de Estagiário(a)",
            UrlLogo = "C:\\Users\\mouti\\OneDrive\\Desktop\\cicti\\logo.png",
            TituloHeader = "Cadastro do(a) Estagiário(a)",
            SubtituloHeader = "Preencha <b>TODOS os campos</b> corretamente, para evitar <b>ATRASOS</b> no pagamento da bolsa auxílio.",
            UrlAcao = "/registrar",
            CorPrincipal = "#FFA500"
        };

        var secaoPessoal = new SecaoFormulario
        {
            Titulo = "Dados Pessoais"
        };

        // Linha 1: Nome Completo (1 campo, 100% da largura)
        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarNomeCompleto()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
        FabricaCampos.CriarNomeSocial()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarCPF(), 
            FabricaCampos.CriarRG()
            ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarOrgaoExpedidor(),
            FabricaCampos.CriarDataNascimento()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarNacionalidade(),
            FabricaCampos.CriarNaturalidade()
        ));
        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarSexoBiologico(),
            FabricaCampos.CriarIdentidadeDegenero()
        ));
        secaoPessoal.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nomeCompletoPai",
                Rotulo = "Nome Completo do Pai",
                Obrigatorio = false
            }
        ));
        secaoPessoal.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nomeCompletoMae",
                Rotulo = "Nome Completo da Mãe",
                Obrigatorio = true
            }
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarEstadoCivil()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarEndereco()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarBairro(),
            FabricaCampos.CriarCidade(),
            FabricaCampos.CriarCEP()
        ));

        secaoPessoal.Linhas.Add(new LinhaFormulario(
            FabricaCampos.CriarCelular(),
            FabricaCampos.CriarEmail()
        ));

        var secaoInstitucional = new SecaoFormulario
        {
            Titulo = "Dados Institucionais"
        };

        secaoInstitucional.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nomeInsituticao",
                Rotulo = "Instituição de Ensino",
                Obrigatorio = true
            }
        ));
        secaoInstitucional.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "numeroMatricula",
                Rotulo = "N° de Matrícula",
                Obrigatorio = true
            },
            new CampoFormulario
            {
                Tipo = TipoCampo.Numero,
                Nome = "semestre",
                Rotulo = "Semestre",
                Obrigatorio = true
            },
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "curso",
                Rotulo = "Curso",
                Obrigatorio = true
            }
        ));

        var secaoDadosBancarios = new SecaoFormulario
        {
            Titulo = "Dados Bancários"
        };

        secaoDadosBancarios.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Numero,
                Nome = "agencia",
                Rotulo = "Agência",
                Obrigatorio = true
            },
            new CampoFormulario
            {
                Tipo = TipoCampo.Numero,
                Nome = "conta",
                Rotulo = "N° da Conta",
                Obrigatorio = true
            },
            new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "chavePix",
                Rotulo = "Chave Pix",
                Obrigatorio = true
            }
        ));

        var secaoArquivos = new SecaoFormulario
        {
            Titulo = "Envio de Documentos"
        };

        secaoArquivos.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Arquivo,
                Nome = "documentosComprovacao",
                Rotulo = "Comprovante ou atestado de matrícula",
                Obrigatorio = true
            }
        ));

        secaoArquivos.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Arquivo,
                Nome = "documentosComprovacao",
                Rotulo = "Identidade",
                Obrigatorio = true
            }
        ));
        secaoArquivos.Linhas.Add(new LinhaFormulario(
            new CampoFormulario
            {
                Tipo = TipoCampo.Arquivo,
                Nome = "documentosComprovacao",
                Rotulo = "Comprovante de residência",
                Obrigatorio = true
            }
        ));

        meuFormulario.Secoes.Add(secaoPessoal);
        meuFormulario.Secoes.Add(secaoInstitucional);
        meuFormulario.Secoes.Add(secaoDadosBancarios);
        meuFormulario.Secoes.Add(secaoArquivos);

        var gerador = new GeradorFormularioHtml();
        string htmlResultante = gerador.GerarHtml(meuFormulario);

        // O CSS está agora "embutido" no HTML, então só precisamos salvar 1 arquivo.
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string htmlFilePath = Path.Combine(desktopPath, "cicti/formulario_estagiario.html");

        File.WriteAllText(htmlFilePath, htmlResultante);

        Console.WriteLine("--- GERAÇÃO CONCLUÍDA ---");
        Console.WriteLine($"Arquivo HTML (com CSS embutido) salvo em: {htmlFilePath}");
    }
}