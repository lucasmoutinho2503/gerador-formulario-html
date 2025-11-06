namespace GeradorDeFormulario.Desktop
{
    partial class TelaEditarConfirmacao
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
            label1 = new Label();
            label2 = new Label();
            txtTituloPagina = new TextBox();
            panel1 = new Panel();
            txtConteudoHtml = new TextBox();
            btnSalvarConfirmacao = new Button();
            btnCancelarConfirmacao = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // chkHabilitado
            // 
            chkHabilitado.AutoSize = true;
            chkHabilitado.Location = new Point(12, 12);
            chkHabilitado.Name = "chkHabilitado";
            chkHabilitado.Size = new Size(71, 19);
            chkHabilitado.TabIndex = 0;
            chkHabilitado.Text = "Habilitar";
            chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 34);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 1;
            label1.Text = "Título da Janela";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 2;
            label2.Text = "Conteúdo (HTML):";
            // 
            // txtTituloPagina
            // 
            txtTituloPagina.Location = new Point(106, 31);
            txtTituloPagina.Name = "txtTituloPagina";
            txtTituloPagina.Size = new Size(270, 23);
            txtTituloPagina.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtConteudoHtml);
            panel1.Location = new Point(12, 77);
            panel1.Name = "panel1";
            panel1.Size = new Size(722, 480);
            panel1.TabIndex = 9;
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
            // btnSalvarConfirmacao
            // 
            btnSalvarConfirmacao.Location = new Point(659, 579);
            btnSalvarConfirmacao.Name = "btnSalvarConfirmacao";
            btnSalvarConfirmacao.Size = new Size(75, 23);
            btnSalvarConfirmacao.TabIndex = 12;
            btnSalvarConfirmacao.Text = "Salvar";
            btnSalvarConfirmacao.UseVisualStyleBackColor = true;
            btnSalvarConfirmacao.Click += this.btnSalvarConfirmacao_Click;
            // 
            // btnCancelarConfirmacao
            // 
            btnCancelarConfirmacao.Location = new Point(578, 579);
            btnCancelarConfirmacao.Name = "btnCancelarConfirmacao";
            btnCancelarConfirmacao.Size = new Size(75, 23);
            btnCancelarConfirmacao.TabIndex = 11;
            btnCancelarConfirmacao.Text = "Cancelar";
            btnCancelarConfirmacao.UseVisualStyleBackColor = true;
            btnCancelarConfirmacao.Click += this.btnCancelarConfirmacao_Click;
            // 
            // TelaEditarConfirmacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(746, 616);
            Controls.Add(btnSalvarConfirmacao);
            Controls.Add(btnCancelarConfirmacao);
            Controls.Add(panel1);
            Controls.Add(txtTituloPagina);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(chkHabilitado);
            Name = "TelaEditarConfirmacao";
            Text = "FormEditarConfirmacao";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkHabilitado;
        private Label label1;
        private Label label2;
        private TextBox txtTituloPagina;
        private Panel panel1;
        private TextBox txtConteudoHtml;
        private Button btnSalvarConfirmacao;
        private Button btnCancelarConfirmacao;
    }
}