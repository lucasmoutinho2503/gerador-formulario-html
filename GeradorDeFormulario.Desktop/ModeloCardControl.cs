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
    public partial class ModeloCardControl : UserControl
    {
        [Category("Dados do Template")]
        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }

        [Category("Dados do Template")]
        public string Descricao
        {
            get { return lblDescricao.Text; }
            set { lblDescricao.Text = value; }
        }

        [Category("Dados do Template")]
        public Image Icone
        {
            get { return picIcone.Image; }
            set { picIcone.Image = value; }
        }

        [Category("Dados do Template")]
        public string CaminhoJsonTemplate { get; set; }

        private List<string> _tags = new List<string>();

        [Category("Dados do Template")]
        [Description("Lista de tags para exibir (ex: RH, Hotel, PT-BR)")]
        public List<string> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                AtualizarTags(); // Chama o método para criar as labels
            }
        }

        public ModeloCardControl()
        {
            InitializeComponent();

            this.lblTitulo.Click += EncaminharClique;
            this.lblDescricao.Click += EncaminharClique;
            this.picIcone.Click += EncaminharClique;
            this.flowPanelTags.Click += EncaminharClique;
        }
        private void EncaminharClique(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        private void AtualizarTags()
        {
            flowPanelTags.Controls.Clear(); // Limpa tags antigas

            if (_tags == null) return;

            foreach (string tagTexto in _tags)
            {
                Label lblTag = new Label();
                lblTag.Text = tagTexto;
                lblTag.BackColor = Color.LightGray; // Cor de fundo da tag
                lblTag.ForeColor = Color.Black;     // Cor do texto
                lblTag.Font = new Font(this.Font.FontFamily, 7f); // Fonte pequena
                lblTag.Padding = new Padding(3, 2, 3, 2);
                lblTag.Margin = new Padding(0, 0, 4, 0); // Espaço entre as tags
                lblTag.AutoSize = true; // Deixa a label se ajustar ao texto

                // Encaminha o clique da nova tag também
                lblTag.Click += EncaminharClique;

                flowPanelTags.Controls.Add(lblTag);
            }
        }

        private bool _estaSelecionado = false;

        public bool EstaSelecionado { get; }

        public void Selecionar()
        {
            _estaSelecionado = true;
            this.BackColor = Color.LightSkyBlue; // Cor de destaque
            this.Padding = new Padding(2);
            this.Paint += PintarBordaSelecionada;
            this.Invalidate();
        }
        public void Desselecionar()
        {
            _estaSelecionado = false;
            this.BackColor = Color.White; // Cor padrão
            this.BorderStyle = BorderStyle.FixedSingle;
        }
        private void PintarBordaSelecionada(object sender, PaintEventArgs e)
        {
            if (_estaSelecionado)
            {
                // Desenha um retângulo azul (ou sua CorPrincipal) ao redor do controle
                // (System.Drawing.ColorTranslator.FromHtml("#FFA500") para usar seu laranja)
                Color corBorda = Color.CornflowerBlue;

                // Desenha a borda
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    corBorda, 2, ButtonBorderStyle.Solid, // Esquerda
                    corBorda, 2, ButtonBorderStyle.Solid, // Cima
                    corBorda, 2, ButtonBorderStyle.Solid, // Direita
                    corBorda, 2, ButtonBorderStyle.Solid); // Baixo
            }
        }
    }
}
