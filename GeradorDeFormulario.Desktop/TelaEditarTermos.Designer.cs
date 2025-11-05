namespace GeradorDeFormulario.Desktop
{
    partial class TelaEditarTermos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chkHabilitado = new CheckBox();
            labelTituloModal = new Label();
            txtTituloModal = new TextBox();
            txtMensagemConfirmacao = new TextBox();
            labelMensagemConfirmacao = new Label();
            txtConteudoHtml = new TextBox();
            labelConteudo = new Label();
            panel1 = new Panel();
            btnCancelarTermos = new Button();
            btnSalvarTermos = new Button();
            labelImagem = new Label();
            txtUrlImagemTitulo = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // chkHabilitado
            // 
            chkHabilitado.AutoSize = true;
            chkHabilitado.Location = new Point(319, 12);
            chkHabilitado.Name = "chkHabilitado";
            chkHabilitado.Size = new Size(204, 19);
            chkHabilitado.TabIndex = 0;
            chkHabilitado.Text = "Habilitar Modal de Termos de Uso";
            chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // labelTituloModal
            // 
            labelTituloModal.AutoSize = true;
            labelTituloModal.Location = new Point(12, 9);
            labelTituloModal.Name = "labelTituloModal";
            labelTituloModal.Size = new Size(40, 15);
            labelTituloModal.TabIndex = 1;
            labelTituloModal.Text = "Título:";
            // 
            // txtTituloModal
            // 
            txtTituloModal.Location = new Point(58, 6);
            txtTituloModal.Name = "txtTituloModal";
            txtTituloModal.Size = new Size(169, 23);
            txtTituloModal.TabIndex = 2;
            // 
            // txtMensagemConfirmacao
            // 
            txtMensagemConfirmacao.Location = new Point(87, 35);
            txtMensagemConfirmacao.Name = "txtMensagemConfirmacao";
            txtMensagemConfirmacao.Size = new Size(169, 23);
            txtMensagemConfirmacao.TabIndex = 4;
            // 
            // labelMensagemConfirmacao
            // 
            labelMensagemConfirmacao.AutoSize = true;
            labelMensagemConfirmacao.Location = new Point(12, 38);
            labelMensagemConfirmacao.Name = "labelMensagemConfirmacao";
            labelMensagemConfirmacao.Size = new Size(69, 15);
            labelMensagemConfirmacao.TabIndex = 3;
            labelMensagemConfirmacao.Text = "Mensagem:";
            // 
            // txtConteudoHtml
            // 
            txtConteudoHtml.AcceptsReturn = true;
            txtConteudoHtml.Dock = DockStyle.Fill;
            txtConteudoHtml.Location = new Point(0, 0);
            txtConteudoHtml.Multiline = true;
            txtConteudoHtml.Name = "txtConteudoHtml";
            txtConteudoHtml.ScrollBars = ScrollBars.Vertical;
            txtConteudoHtml.Size = new Size(722, 480);
            txtConteudoHtml.TabIndex = 5;
            // 
            // labelConteudo
            // 
            labelConteudo.AutoSize = true;
            labelConteudo.Location = new Point(12, 73);
            labelConteudo.Name = "labelConteudo";
            labelConteudo.Size = new Size(106, 15);
            labelConteudo.TabIndex = 6;
            labelConteudo.Text = "Conteúdo (HTML):";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtConteudoHtml);
            panel1.Location = new Point(12, 91);
            panel1.Name = "panel1";
            panel1.Size = new Size(722, 480);
            panel1.TabIndex = 8;
            // 
            // btnCancelarTermos
            // 
            btnCancelarTermos.Location = new Point(578, 589);
            btnCancelarTermos.Name = "btnCancelarTermos";
            btnCancelarTermos.Size = new Size(75, 23);
            btnCancelarTermos.TabIndex = 9;
            btnCancelarTermos.Text = "Cancelar";
            btnCancelarTermos.UseVisualStyleBackColor = true;
            btnCancelarTermos.Click += btnCancelarTermos_Click;
            // 
            // btnSalvarTermos
            // 
            btnSalvarTermos.Location = new Point(659, 589);
            btnSalvarTermos.Name = "btnSalvarTermos";
            btnSalvarTermos.Size = new Size(75, 23);
            btnSalvarTermos.TabIndex = 10;
            btnSalvarTermos.Text = "Salvar";
            btnSalvarTermos.UseVisualStyleBackColor = true;
            btnSalvarTermos.Click += btnSalvarTermos_Click;
            // 
            // labelImagem
            // 
            labelImagem.AutoSize = true;
            labelImagem.Location = new Point(319, 34);
            labelImagem.Name = "labelImagem";
            labelImagem.Size = new Size(144, 15);
            labelImagem.TabIndex = 11;
            labelImagem.Text = "URL da Imagem do Título:";
            // 
            // txtUrlImagemTitulo
            // 
            txtUrlImagemTitulo.Location = new Point(491, 34);
            txtUrlImagemTitulo.Name = "txtUrlImagemTitulo";
            txtUrlImagemTitulo.Size = new Size(197, 23);
            txtUrlImagemTitulo.TabIndex = 12;
            // 
            // TelaEditarTermos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(746, 624);
            Controls.Add(txtUrlImagemTitulo);
            Controls.Add(labelImagem);
            Controls.Add(btnSalvarTermos);
            Controls.Add(btnCancelarTermos);
            Controls.Add(panel1);
            Controls.Add(labelConteudo);
            Controls.Add(txtMensagemConfirmacao);
            Controls.Add(labelMensagemConfirmacao);
            Controls.Add(txtTituloModal);
            Controls.Add(labelTituloModal);
            Controls.Add(chkHabilitado);
            MaximizeBox = false;
            Name = "TelaEditarTermos";
            Text = "TelaEditarTermos";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkHabilitado;
        private Label labelTituloModal;
        private TextBox txtTituloModal;
        private TextBox txtMensagemConfirmacao;
        private Label labelMensagemConfirmacao;
        private TextBox txtConteudoHtml;
        private Label labelConteudo;
        private Panel panel1;
        private Button btnCancelarTermos;
        private Button btnSalvarTermos;
        private Label labelImagem;
        private TextBox txtUrlImagemTitulo;
    }
}