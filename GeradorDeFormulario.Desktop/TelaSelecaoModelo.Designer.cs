namespace GeradorDeFormulario.Desktop
{
    partial class TelaSelecaoModelo
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
            components = new System.ComponentModel.Container();
            imageListPreviews = new ImageList(components);
            listBoxModelos = new ListBox();
            txtBusca = new TextBox();
            comboBoxIdioma = new ComboBox();
            comboBoxTipo = new ComboBox();
            comboBoxCategoria = new ComboBox();
            flowPanelModelos = new FlowLayoutPanel();
            btnSelecionar = new Button();
            panel1 = new Panel();
            btnCancelar = new Button();
            btnNovoEmBranco = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // imageListPreviews
            // 
            imageListPreviews.ColorDepth = ColorDepth.Depth32Bit;
            imageListPreviews.ImageSize = new Size(200, 200);
            imageListPreviews.TransparentColor = Color.Transparent;
            // 
            // listBoxModelos
            // 
            listBoxModelos.Dock = DockStyle.Left;
            listBoxModelos.FormattingEnabled = true;
            listBoxModelos.ItemHeight = 15;
            listBoxModelos.Location = new Point(0, 0);
            listBoxModelos.Name = "listBoxModelos";
            listBoxModelos.Size = new Size(233, 561);
            listBoxModelos.TabIndex = 0;
            // 
            // txtBusca
            // 
            txtBusca.Location = new Point(307, 30);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Busque um modelo de formulário";
            txtBusca.Size = new Size(245, 23);
            txtBusca.TabIndex = 1;
            // 
            // comboBoxIdioma
            // 
            comboBoxIdioma.FormattingEnabled = true;
            comboBoxIdioma.Location = new Point(307, 72);
            comboBoxIdioma.Name = "comboBoxIdioma";
            comboBoxIdioma.Size = new Size(160, 23);
            comboBoxIdioma.TabIndex = 2;
            // 
            // comboBoxTipo
            // 
            comboBoxTipo.FormattingEnabled = true;
            comboBoxTipo.Location = new Point(639, 72);
            comboBoxTipo.Name = "comboBoxTipo";
            comboBoxTipo.Size = new Size(160, 23);
            comboBoxTipo.TabIndex = 3;
            // 
            // comboBoxCategoria
            // 
            comboBoxCategoria.FormattingEnabled = true;
            comboBoxCategoria.Location = new Point(473, 72);
            comboBoxCategoria.Name = "comboBoxCategoria";
            comboBoxCategoria.Size = new Size(160, 23);
            comboBoxCategoria.TabIndex = 4;
            // 
            // flowPanelModelos
            // 
            flowPanelModelos.AutoScroll = true;
            flowPanelModelos.Location = new Point(307, 116);
            flowPanelModelos.Name = "flowPanelModelos";
            flowPanelModelos.Size = new Size(500, 400);
            flowPanelModelos.TabIndex = 5;
            // 
            // btnSelecionar
            // 
            btnSelecionar.Location = new Point(715, 529);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(75, 23);
            btnSelecionar.TabIndex = 6;
            btnSelecionar.TabStop = false;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = true;
            btnSelecionar.Click += btnSelecionar_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnNovoEmBranco);
            panel1.Controls.Add(btnCancelar);
            panel1.Location = new Point(307, 519);
            panel1.Name = "panel1";
            panel1.Size = new Size(492, 42);
            panel1.TabIndex = 7;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(327, 10);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 7;
            btnCancelar.TabStop = false;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnNovoEmBranco
            // 
            btnNovoEmBranco.Location = new Point(246, 10);
            btnNovoEmBranco.Name = "btnNovoEmBranco";
            btnNovoEmBranco.Size = new Size(75, 23);
            btnNovoEmBranco.TabIndex = 8;
            btnNovoEmBranco.Text = "Novo";
            btnNovoEmBranco.UseVisualStyleBackColor = true;
            btnNovoEmBranco.Click += btnNovoEmBranco_Click;
            // 
            // TelaSelecaoModelo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 561);
            Controls.Add(btnSelecionar);
            Controls.Add(flowPanelModelos);
            Controls.Add(comboBoxCategoria);
            Controls.Add(comboBoxTipo);
            Controls.Add(comboBoxIdioma);
            Controls.Add(txtBusca);
            Controls.Add(listBoxModelos);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "TelaSelecaoModelo";
            Text = "TelaSelecaoModelo";
            Load += TelaSelecaoModelo_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ImageList imageListPreviews;
        private ListBox listBoxModelos;
        private TextBox txtBusca;
        private ComboBox comboBoxIdioma;
        private ComboBox comboBoxTipo;
        private ComboBox comboBoxCategoria;
        private FlowLayoutPanel flowPanelModelos;
        private Button btnSelecionar;
        private Panel panel1;
        private Button btnCancelar;
        private Button btnNovoEmBranco;
    }
}