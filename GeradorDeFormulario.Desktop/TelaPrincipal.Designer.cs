namespace GeradorDeFormulario.Desktop
{
    partial class TelaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            salvarArquivoToolStripMenuItem = new ToolStripMenuItem();
            modeloToolStripMenuItem = new ToolStripMenuItem();
            salvarModeloToolStripMenuItem = new ToolStripMenuItem();
            carregarModeloToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            tabControlEditor = new TabControl();
            tabPropriedades = new TabPage();
            propertyGridItem = new PropertyGrid();
            tabOpcoes = new TabPage();
            btnRemoverOpcao = new Button();
            btnAdicionarOpcao = new Button();
            txtNovaOpcao = new TextBox();
            lstOpcoes = new CheckedListBox();
            splitContainer2 = new SplitContainer();
            treeViewFormulario = new TreeView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            cmbCamposDisponiveis = new ComboBox();
            btnAdicionarCampo = new Button();
            btnAdicionarSecao = new Button();
            btnAdicionarLinha = new Button();
            btnRemoverItem = new Button();
            btnConexao = new Button();
            visualizadorFormulario = new Microsoft.Web.WebView2.WinForms.WebView2();
            dialogoSalvarArquivo = new SaveFileDialog();
            dialogoAbrirModelo = new OpenFileDialog();
            dialogoSalvarModelo = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControlEditor.SuspendLayout();
            tabPropriedades.SuspendLayout();
            tabOpcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)visualizadorFormulario).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1058, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salvarArquivoToolStripMenuItem, modeloToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(61, 20);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // salvarArquivoToolStripMenuItem
            // 
            salvarArquivoToolStripMenuItem.Name = "salvarArquivoToolStripMenuItem";
            salvarArquivoToolStripMenuItem.Size = new Size(166, 22);
            salvarArquivoToolStripMenuItem.Text = "Salvar Formulário";
            salvarArquivoToolStripMenuItem.Click += salvarArquivoToolStripMenuItem_Click;
            // 
            // modeloToolStripMenuItem
            // 
            modeloToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salvarModeloToolStripMenuItem, carregarModeloToolStripMenuItem });
            modeloToolStripMenuItem.Name = "modeloToolStripMenuItem";
            modeloToolStripMenuItem.Size = new Size(166, 22);
            modeloToolStripMenuItem.Text = "Modelo";
            // 
            // salvarModeloToolStripMenuItem
            // 
            salvarModeloToolStripMenuItem.Name = "salvarModeloToolStripMenuItem";
            salvarModeloToolStripMenuItem.Size = new Size(163, 22);
            salvarModeloToolStripMenuItem.Text = "Salvar Modelo";
            salvarModeloToolStripMenuItem.Click += salvarModeloToolStripMenuItem_Click;
            // 
            // carregarModeloToolStripMenuItem
            // 
            carregarModeloToolStripMenuItem.Name = "carregarModeloToolStripMenuItem";
            carregarModeloToolStripMenuItem.Size = new Size(163, 22);
            carregarModeloToolStripMenuItem.Text = "Carregar Modelo";
            carregarModeloToolStripMenuItem.Click += carregarModeloToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControlEditor);
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(visualizadorFormulario);
            splitContainer1.Size = new Size(1058, 760);
            splitContainer1.SplitterDistance = 388;
            splitContainer1.TabIndex = 1;
            // 
            // tabControlEditor
            // 
            tabControlEditor.Controls.Add(tabPropriedades);
            tabControlEditor.Controls.Add(tabOpcoes);
            tabControlEditor.Location = new Point(3, 438);
            tabControlEditor.Name = "tabControlEditor";
            tabControlEditor.SelectedIndex = 0;
            tabControlEditor.Size = new Size(382, 319);
            tabControlEditor.TabIndex = 2;
            // 
            // tabPropriedades
            // 
            tabPropriedades.Controls.Add(propertyGridItem);
            tabPropriedades.Location = new Point(4, 24);
            tabPropriedades.Name = "tabPropriedades";
            tabPropriedades.Padding = new Padding(3);
            tabPropriedades.Size = new Size(374, 291);
            tabPropriedades.TabIndex = 0;
            tabPropriedades.Text = "Propriedades";
            tabPropriedades.UseVisualStyleBackColor = true;
            // 
            // propertyGridItem
            // 
            propertyGridItem.Dock = DockStyle.Fill;
            propertyGridItem.Location = new Point(3, 3);
            propertyGridItem.Name = "propertyGridItem";
            propertyGridItem.Size = new Size(368, 285);
            propertyGridItem.TabIndex = 0;
            propertyGridItem.PropertyValueChanged += propertyGridItem_PropertyValueChanged;
            // 
            // tabOpcoes
            // 
            tabOpcoes.Controls.Add(btnRemoverOpcao);
            tabOpcoes.Controls.Add(btnAdicionarOpcao);
            tabOpcoes.Controls.Add(txtNovaOpcao);
            tabOpcoes.Controls.Add(lstOpcoes);
            tabOpcoes.Location = new Point(4, 24);
            tabOpcoes.Name = "tabOpcoes";
            tabOpcoes.Padding = new Padding(3);
            tabOpcoes.Size = new Size(374, 291);
            tabOpcoes.TabIndex = 1;
            tabOpcoes.Text = "Opções (Dropdown)";
            tabOpcoes.UseVisualStyleBackColor = true;
            // 
            // btnRemoverOpcao
            // 
            btnRemoverOpcao.Location = new Point(3, 179);
            btnRemoverOpcao.Name = "btnRemoverOpcao";
            btnRemoverOpcao.Size = new Size(368, 23);
            btnRemoverOpcao.TabIndex = 3;
            btnRemoverOpcao.Text = "Remover Selecionado";
            btnRemoverOpcao.UseVisualStyleBackColor = true;
            btnRemoverOpcao.Click += btnRemoverOpcao_Click;
            // 
            // btnAdicionarOpcao
            // 
            btnAdicionarOpcao.Location = new Point(3, 156);
            btnAdicionarOpcao.Name = "btnAdicionarOpcao";
            btnAdicionarOpcao.Size = new Size(368, 23);
            btnAdicionarOpcao.TabIndex = 2;
            btnAdicionarOpcao.Text = "Adicionar Opção";
            btnAdicionarOpcao.UseVisualStyleBackColor = true;
            btnAdicionarOpcao.Click += btnAdicionarOpcao_Click;
            // 
            // txtNovaOpcao
            // 
            txtNovaOpcao.Location = new Point(3, 133);
            txtNovaOpcao.Name = "txtNovaOpcao";
            txtNovaOpcao.Size = new Size(368, 23);
            txtNovaOpcao.TabIndex = 1;
            // 
            // lstOpcoes
            // 
            lstOpcoes.FormattingEnabled = true;
            lstOpcoes.Location = new Point(3, 3);
            lstOpcoes.Name = "lstOpcoes";
            lstOpcoes.Size = new Size(368, 130);
            lstOpcoes.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Location = new Point(3, 93);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeViewFormulario);
            splitContainer2.Size = new Size(382, 664);
            splitContainer2.SplitterDistance = 338;
            splitContainer2.TabIndex = 1;
            // 
            // treeViewFormulario
            // 
            treeViewFormulario.Dock = DockStyle.Fill;
            treeViewFormulario.Location = new Point(0, 0);
            treeViewFormulario.Name = "treeViewFormulario";
            treeViewFormulario.Size = new Size(382, 338);
            treeViewFormulario.TabIndex = 0;
            treeViewFormulario.AfterSelect += treeViewFormulario_AfterSelect;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(cmbCamposDisponiveis);
            flowLayoutPanel1.Controls.Add(btnConexao);
            flowLayoutPanel1.Controls.Add(btnAdicionarSecao);
            flowLayoutPanel1.Controls.Add(btnAdicionarLinha);
            flowLayoutPanel1.Controls.Add(btnAdicionarCampo);
            flowLayoutPanel1.Controls.Add(btnRemoverItem);
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(385, 87);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 15);
            label1.TabIndex = 0;
            label1.Text = "Adicionar Componente:";
            // 
            // cmbCamposDisponiveis
            // 
            cmbCamposDisponiveis.FormattingEnabled = true;
            cmbCamposDisponiveis.Location = new Point(143, 3);
            cmbCamposDisponiveis.Name = "cmbCamposDisponiveis";
            cmbCamposDisponiveis.Size = new Size(234, 23);
            cmbCamposDisponiveis.TabIndex = 1;
            // 
            // btnAdicionarCampo
            // 
            btnAdicionarCampo.Location = new Point(246, 32);
            btnAdicionarCampo.Name = "btnAdicionarCampo";
            btnAdicionarCampo.Size = new Size(75, 23);
            btnAdicionarCampo.TabIndex = 2;
            btnAdicionarCampo.Text = "Campo";
            btnAdicionarCampo.UseVisualStyleBackColor = true;
            btnAdicionarCampo.Click += btnAdicionarCampo_Click;
            // 
            // btnAdicionarSecao
            // 
            btnAdicionarSecao.Location = new Point(84, 32);
            btnAdicionarSecao.Name = "btnAdicionarSecao";
            btnAdicionarSecao.Size = new Size(75, 23);
            btnAdicionarSecao.TabIndex = 3;
            btnAdicionarSecao.Text = "Secao";
            btnAdicionarSecao.UseVisualStyleBackColor = true;
            btnAdicionarSecao.Click += button2_Click;
            // 
            // btnAdicionarLinha
            // 
            btnAdicionarLinha.Location = new Point(165, 32);
            btnAdicionarLinha.Name = "btnAdicionarLinha";
            btnAdicionarLinha.Size = new Size(75, 23);
            btnAdicionarLinha.TabIndex = 4;
            btnAdicionarLinha.Text = "Linha";
            btnAdicionarLinha.UseVisualStyleBackColor = true;
            btnAdicionarLinha.Click += btnAdicionarLinha_Click;
            // 
            // btnRemoverItem
            // 
            btnRemoverItem.Location = new Point(3, 61);
            btnRemoverItem.Name = "btnRemoverItem";
            btnRemoverItem.Size = new Size(75, 23);
            btnRemoverItem.TabIndex = 5;
            btnRemoverItem.Text = "Remover";
            btnRemoverItem.UseVisualStyleBackColor = true;
            btnRemoverItem.Click += btnRemoverItem_Click;
            // 
            // btnConexao
            // 
            btnConexao.Location = new Point(3, 32);
            btnConexao.Name = "btnConexao";
            btnConexao.Size = new Size(75, 23);
            btnConexao.TabIndex = 6;
            btnConexao.Text = "Conexão";
            btnConexao.UseVisualStyleBackColor = true;
            btnConexao.Click += btnConexao_Click;
            // 
            // visualizadorFormulario
            // 
            visualizadorFormulario.AllowExternalDrop = true;
            visualizadorFormulario.CreationProperties = null;
            visualizadorFormulario.DefaultBackgroundColor = Color.White;
            visualizadorFormulario.Dock = DockStyle.Fill;
            visualizadorFormulario.Location = new Point(0, 0);
            visualizadorFormulario.Name = "visualizadorFormulario";
            visualizadorFormulario.Size = new Size(666, 760);
            visualizadorFormulario.TabIndex = 0;
            visualizadorFormulario.ZoomFactor = 1D;
            // 
            // dialogoAbrirModelo
            // 
            dialogoAbrirModelo.FileName = "openFileDialog1";
            // 
            // TelaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 784);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TelaPrincipal";
            Text = "Gerador de Formulário";
            WindowState = FormWindowState.Maximized;
            Load += TelaPrincipal_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControlEditor.ResumeLayout(false);
            tabPropriedades.ResumeLayout(false);
            tabOpcoes.ResumeLayout(false);
            tabOpcoes.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)visualizadorFormulario).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 visualizadorFormulario;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private ComboBox cmbCamposDisponiveis;
        private Button btnAdicionarCampo;
        private Button btnAdicionarSecao;
        private Button btnAdicionarLinha;
        private Button btnRemoverItem;
        private SplitContainer splitContainer2;
        private TreeView treeViewFormulario;
        private PropertyGrid propertyGridItem;
        private ToolStripMenuItem salvarArquivoToolStripMenuItem;
        private SaveFileDialog dialogoSalvarArquivo;
        private Button btnConexao;
        private ToolStripMenuItem modeloToolStripMenuItem;
        private ToolStripMenuItem salvarModeloToolStripMenuItem;
        private ToolStripMenuItem carregarModeloToolStripMenuItem;
        private OpenFileDialog dialogoAbrirModelo;
        private SaveFileDialog dialogoSalvarModelo;
        private TabControl tabControlEditor;
        private TabPage tabPropriedades;
        private TabPage tabOpcoes;
        private Button btnRemoverOpcao;
        private Button btnAdicionarOpcao;
        private TextBox txtNovaOpcao;
        private CheckedListBox lstOpcoes;
    }
}
