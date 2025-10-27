namespace GeradorDeFormulario.Desktop
{
    partial class teste
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
            treeViewFormulario = new TreeView();
            propertyGridItem = new PropertyGrid();
            SuspendLayout();
            // 
            // treeViewFormulario
            // 
            treeViewFormulario.Location = new Point(0, 0);
            treeViewFormulario.Name = "treeViewFormulario";
            treeViewFormulario.Size = new Size(372, 249);
            treeViewFormulario.TabIndex = 0;
            // 
            // propertyGridItem
            // 
            propertyGridItem.Location = new Point(8, 8);
            propertyGridItem.Name = "propertyGridItem";
            propertyGridItem.Size = new Size(130, 130);
            propertyGridItem.TabIndex = 1;
            // 
            // teste
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 587);
            Controls.Add(propertyGridItem);
            Controls.Add(treeViewFormulario);
            Name = "teste";
            Text = "teste";
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeViewFormulario;
        private PropertyGrid propertyGridItem;
    }
}