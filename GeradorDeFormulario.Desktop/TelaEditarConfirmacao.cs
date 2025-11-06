using GeradorFormulario.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorDeFormulario.Desktop
{
    public partial class TelaEditarConfirmacao : Form
    {
        public PaginaConfirmacao ConfigConfirmacao { get; private set; }
        public TelaEditarConfirmacao(PaginaConfirmacao configAtual)
        {
            InitializeComponent();

            string jsonTemp = Newtonsoft.Json.JsonConvert.SerializeObject(configAtual);
            this.ConfigConfirmacao = Newtonsoft.Json.JsonConvert.DeserializeObject<PaginaConfirmacao>(jsonTemp);

            if (this.ConfigConfirmacao == null)
            {
                this.ConfigConfirmacao = new PaginaConfirmacao();
            }

            CarregarDadosNaTela();

            btnSalvarConfirmacao.DialogResult = DialogResult.OK;
            btnCancelarConfirmacao.DialogResult = DialogResult.Cancel;
        }
        private void CarregarDadosNaTela()
        {
            chkHabilitado.Checked = ConfigConfirmacao.Habilitado;
            txtTituloPagina.Text = ConfigConfirmacao.TituloPagina;
            txtConteudoHtml.Text = ConfigConfirmacao.ConteudoHtml;
        }

        private void SalvarDadosDoForm()
        {
            ConfigConfirmacao.Habilitado = chkHabilitado.Checked;
            ConfigConfirmacao.TituloPagina = txtTituloPagina.Text;
            ConfigConfirmacao.ConteudoHtml = txtConteudoHtml.Text;
        }

        private void btnSalvarConfirmacao_Click(object sender, EventArgs e)
        {
            SalvarDadosDoForm();

            this.Close();
        }

        private void btnCancelarConfirmacao_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
