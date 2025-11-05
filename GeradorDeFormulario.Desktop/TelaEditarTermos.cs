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
    public partial class TelaEditarTermos : Form
    {
        public ConfiguracaoTermos ConfigTermos { get; private set; }

        public TelaEditarTermos(ConfiguracaoTermos termosAtuais)
        {
            InitializeComponent();

            this.ConfigTermos = termosAtuais ?? new ConfiguracaoTermos();

            chkHabilitado.Checked = ConfigTermos.Habilitado;
            txtTituloModal.Text = ConfigTermos.TituloModal;
            txtMensagemConfirmacao.Text = ConfigTermos.MensagemConfirmacao;
            txtUrlImagemTitulo.Text = ConfigTermos.UrlImagemTitulo;
            txtConteudoHtml.Text = ConfigTermos.ConteudoHtml;

            btnSalvarTermos.DialogResult = DialogResult.OK;
            btnCancelarTermos.DialogResult = DialogResult.Cancel;

        }

        private void btnSalvarTermos_Click(object sender, EventArgs e)
        {
            ConfigTermos.Habilitado = chkHabilitado.Checked;
            ConfigTermos.TituloModal = txtTituloModal.Text;
            ConfigTermos.MensagemConfirmacao = txtMensagemConfirmacao.Text;
            ConfigTermos.UrlImagemTitulo = txtUrlImagemTitulo.Text;
            ConfigTermos.ConteudoHtml = txtConteudoHtml.Text;
        }

        private void btnCancelarTermos_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
