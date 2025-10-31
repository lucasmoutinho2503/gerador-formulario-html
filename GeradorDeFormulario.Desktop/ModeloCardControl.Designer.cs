namespace GeradorDeFormulario.Desktop
{
    partial class ModeloCardControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picIcone = new PictureBox();
            lblTitulo = new Label();
            lblDescricao = new Label();
            flowPanelTags = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picIcone).BeginInit();
            SuspendLayout();
            // 
            // picIcone
            // 
            picIcone.BorderStyle = BorderStyle.FixedSingle;
            picIcone.Location = new Point(5, 5);
            picIcone.Name = "picIcone";
            picIcone.Size = new Size(90, 90);
            picIcone.SizeMode = PictureBoxSizeMode.Zoom;
            picIcone.TabIndex = 0;
            picIcone.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(100, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(190, 20);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Título do Modelo";
            // 
            // lblDescricao
            // 
            lblDescricao.ForeColor = Color.DimGray;
            lblDescricao.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            lblDescricao.Location = new Point(100, 35);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(350, 35);
            lblDescricao.TabIndex = 2;
            lblDescricao.Text = "Aqui vai uma breve descrição do que este modelo de formulário faz...";
            // 
            // flowPanelTags
            // 
            flowPanelTags.Location = new Point(100, 75);
            flowPanelTags.Name = "flowPanelTags";
            flowPanelTags.Size = new Size(350, 20);
            flowPanelTags.TabIndex = 3;
            flowPanelTags.WrapContents = false;
            // 
            // ModeloCardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            Controls.Add(flowPanelTags);
            Controls.Add(lblDescricao);
            Controls.Add(lblTitulo);
            Controls.Add(picIcone);
            Name = "ModeloCardControl";
            Size = new Size(492, 102);
            ((System.ComponentModel.ISupportInitialize)picIcone).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picIcone;
        private Label lblTitulo;
        private Label lblDescricao;
        private FlowLayoutPanel flowPanelTags;
    }
}
