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

        public static CampoFormulario CriarNomeCompleto()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nomeCompleto",
                Rotulo = "Nome Completo",
                Obrigatorio = true
            };
        }

        public static CampoFormulario CriarNomeSocial()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nomeCompleto",
                Rotulo = "Nome Social (Opcional)"
            };
        }

        public static CampoFormulario CriarCelular()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "celular",
                Rotulo = "Celular",
                Placeholder = "(XX) 9XXXX-XXXX",
                Obrigatorio = true,
                ValidacaoEspecial = "celular"
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

        public static CampoFormulario CriarCPF()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "cpf",
                Rotulo = "CPF",
                Placeholder = "000.000.000-00",
                Obrigatorio = true,
                ValidacaoEspecial = "cpf"
            };
        }

        public static CampoFormulario CriarRG()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "rg",
                Rotulo = "RG",
                Obrigatorio = true
            };
        }

        public static CampoFormulario CriarBairro()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "endereco_bairro",
                Rotulo = "Bairro",
                Obrigatorio = true
            };
        }

        public static CampoFormulario CriarCidade()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "endereco_cidade",
                Rotulo = "Cidade",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarCEP()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "enderecoCep",
                Rotulo = "CEP",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarDataNascimento()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "data_nascimento",
                Rotulo = "Data de Nascimento",
                Placeholder = "dd/mm/aaaa",
                Obrigatorio = true,
                ValidacaoEspecial = "data"
            };
        }
        public static CampoFormulario CriarOrgaoExpedidor()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "orgaoExpedidor",
                Rotulo = "Órgão Expedidor",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarNacionalidade()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "nacionalidade",
                Rotulo = "Nacionalidade",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarNaturalidade()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "naturalidade",
                Rotulo = "Naturalidade",
                Obrigatorio = true
            };
        }
        public static CampoFormulario CriarSexoBiologico()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Selecao,
                Nome = "sexoBiologico",
                Rotulo = "Sexo Biológico",
                Obrigatorio = true,
                Opcoes = new List<string> { "", "Masculino", "Feminino", "Outro" }
            };
        }
        public static CampoFormulario CriarIdentidadeDegenero()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "identidadeDegenero",
                Rotulo = "Identidade de Gênero (Opcional)"
            };
        }
        public static CampoFormulario CriarEstadoCivil()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Selecao,
                Nome = "estadoCivil",
                Rotulo = "Estado Civil",
                Obrigatorio = true,
                Opcoes = new List<string> { "" , "Solteiro(a)", "Casado(a)", "Divorciado(a)", "Viúvo(a)", "União Estável" }
            };
        }
        public static CampoFormulario CriarEndereco()
        {
            return new CampoFormulario
            {
                Tipo = TipoCampo.Texto,
                Nome = "endereco",
                Rotulo = "Endereço Residencial",
                Obrigatorio = true
            };
        }
    }
}
