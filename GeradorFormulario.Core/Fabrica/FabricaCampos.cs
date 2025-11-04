using GeradorFormulario.Core.Enums;
using GeradorFormulario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFormulario.Core.Fabrica
{
    public static class FabricaCampos
    {
        public static CampoFormulario CriarTexto()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "texto",
                Rotulo = "Texto",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarEmail()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Email,
                Nome = "email",
                Rotulo = "Email",
                Obrigatorio = true,
                ValidacaoEspecial = "email"
            };
        }
        public static CampoFormulario CriarSenha()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Senha,
                Nome = "senha",
                Rotulo = "Senha",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarNumero()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Numero,
                Nome = "numero",
                Rotulo = "Número",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarAreaDeTexto()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.AreaDeTexto,
                Nome = "areaDeTexto",
                Rotulo = "Área de Texto",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarSelecao()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Selecao,
                Nome = "selecao",
                Rotulo = "Seleção",
                Obrigatorio = true,
                Opcoes = new List<string> { "", "Opção 1", "Opção 2", "Opção 3" }
            };
        }
        public static CampoFormulario CriarCaixaDeSelecao()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.CaixaDeSelecao,
                Nome = "caixaDeSelecao",
                Rotulo = "Caixa de Seleção",
                Obrigatorio = false,
            };
        }
        public static CampoFormulario CriarEnvioArquivo()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Arquivo,
                Nome = "envioArquivo",
                Rotulo = "Anexar Arquivo",
                Obrigatorio = true
            };
        }

        public static CampoFormulario CriarAssinatura()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Assinatura,
                Nome = "assiantura",
                Rotulo = "Assinatura",
                Obrigatorio = true,
                ValidacaoEspecial = "assinatura"
            };
        }
    }
}
