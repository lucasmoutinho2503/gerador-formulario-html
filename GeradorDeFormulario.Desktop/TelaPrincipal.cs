using GeradorFormulario.Core;
using GeradorFormulario.Core.Fabrica;
using GeradorFormulario.Core.Helper;
using GeradorFormulario.Core.Models;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace GeradorDeFormulario.Desktop
{
    public partial class TelaPrincipal : Form
    {
        private DefinicaoFormulario definicaoFormulario;
        private GeradorFormularioHtml gerador;
        private string arquivoHtmlTemporario;

        public TelaPrincipal()
        {
            InitializeComponent();
            gerador = new GeradorFormularioHtml();
        }

        private async void TelaPrincipal_Load(object sender, EventArgs e)
        {
            var gerador = new GeradorFormularioHtml();
            await visualizadorFormulario.EnsureCoreWebView2Async(null);

            definicaoFormulario = new DefinicaoFormulario
            {
                NomeFormulario = "Meu Formul�rio Din�mico",
                TituloHeader = "Formul�rio de Teste",
                UrlLogo = "https://via.placeholder.com/140", // Placeholder
                CorPrincipal = "#FFA500"
            };

            arquivoHtmlTemporario = Path.Combine(Path.GetTempPath(), "form_preview.html");
            cmbCamposDisponiveis.Items.Add("Nome Completo");
            cmbCamposDisponiveis.Items.Add("Email");
            cmbCamposDisponiveis.Items.Add("CPF");
            cmbCamposDisponiveis.Items.Add("Data de Nascimento");
            cmbCamposDisponiveis.Items.Add("Celular");
            cmbCamposDisponiveis.Items.Add("Arquivo");
            cmbCamposDisponiveis.Items.Add("Linha Endere�o (B/C/C)"); // Componente de linha
            cmbCamposDisponiveis.SelectedIndex = 0;

            treeViewFormulario.Tag = definicaoFormulario; // Associa o objeto raiz
            propertyGridItem.SelectedObject = definicaoFormulario;

        }
        private void AtualizarTreeView()
        {
            treeViewFormulario.Nodes.Clear();
            TreeNode noRaiz = new TreeNode($"Formul�rio: {definicaoFormulario.TituloHeader}");
            noRaiz.Tag = definicaoFormulario;
            treeViewFormulario.Nodes.Add(noRaiz);
            if (definicaoFormulario.Conexao != null)
            {
                TreeNode noConexao = new TreeNode("Conex�o API");
                noConexao.Tag = definicaoFormulario.Conexao; // O n� edita o objeto ConexaoAPI
                noRaiz.Nodes.Add(noConexao);
            }


            foreach (var secao in definicaoFormulario.Secoes)
            {
                TreeNode noSecao = new TreeNode(secao.Titulo ?? "Nova Se��o");
                noSecao.Tag = secao;
                noRaiz.Nodes.Add(noSecao);

                foreach (var linha in secao.Linhas)
                {
                    TreeNode noLinha = new TreeNode("Linha (Layout em Coluna)");
                    noLinha.Tag = linha;
                    noSecao.Nodes.Add(noLinha);

                    foreach (var campo in linha.Campos)
                    {
                        TreeNode noCampo = new TreeNode($"Campo: {campo.Rotulo} ({campo.Tipo})");
                        noCampo.Tag = campo;
                        noLinha.Nodes.Add(noCampo);
                    }
                }
            }
            treeViewFormulario.ExpandAll();
        }

        private void AtualizarPreview()
        {
            try
            {
                // 1. Gera o HTML usando seu motor (do .Core)
                string htmlFinal = gerador.GerarHtml(definicaoFormulario);

                // 2. Salva o HTML no arquivo tempor�rio
                File.WriteAllText(arquivoHtmlTemporario, htmlFinal);

                // 3. Manda o WebView2 ATUALIZAR
                visualizadorFormulario.CoreWebView2.Navigate($"file:///{arquivoHtmlTemporario}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar preview: {ex.Message}");
            }
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var novaSecao = new SecaoFormulario { Titulo = "Nova Se��o" };
            definicaoFormulario.Secoes.Add(novaSecao);

            AtualizarTreeView();
            AtualizarPreview(); // Atualiza o preview
        }

        private void btnAdicionarCampo_Click(object sender, EventArgs e)
        {
            if (treeViewFormulario.SelectedNode?.Tag is LinhaFormulario linha)
            {
                string campoEscolhido = cmbCamposDisponiveis.SelectedItem.ToString();

                // L�gica especial para o componente de endere�o
                if (campoEscolhido == "Linha Endere�o (B/C/C)")
                {
                    // Esse componente j� � uma linha, n�o um campo
                    MessageBox.Show("Este item � especial. Use 'Adicionar Linha' e selecione uma Se��o.");
                    return;
                }

                // Adiciona o campo
                switch (campoEscolhido)
                {
                    case "Nome Completo": linha.Campos.Add(FabricaCampos.CriarNomeCompleto()); break;
                    case "CPF": linha.Campos.Add(FabricaCampos.CriarCPF()); break;
                    case "Celular": linha.Campos.Add(FabricaCampos.CriarCelular()); break;
                    // ... (etc. para todos os campos)
                    case "Arquivo": linha.Campos.Add(FabricaCampos.CriarEnvioArquivo()); break;
                }

                AtualizarTreeView();
                AtualizarPreview(); // Atualiza o preview
            }
            else
            {
                MessageBox.Show("Selecione uma Linha na �rvore primeiro.");
            }
        }


        private void btnAdicionarLinha_Click(object sender, EventArgs e)
        {
            if (treeViewFormulario.SelectedNode?.Tag is SecaoFormulario secao)
            {
                secao.Linhas.Add(new LinhaFormulario());
                AtualizarTreeView();
                AtualizarPreview(); // Atualiza o preview
            }
            else
            {
                MessageBox.Show("Selecione uma Se��o na �rvore primeiro.");
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            var noSelecionado = treeViewFormulario.SelectedNode;
            if (noSelecionado == null || noSelecionado.Tag is DefinicaoFormulario)
            {
                MessageBox.Show("N�o � poss�vel remover o formul�rio raiz.");
                return;
            }

            var itemPai = noSelecionado.Parent.Tag;
            var item = noSelecionado.Tag;

            if (item is ConexaoApi)
            {
                if (MessageBox.Show("Tem certeza que quer remover a Conex�o API?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    definicaoFormulario.Conexao = null;
                }
            }

            if (item is SecaoFormulario secao)
                definicaoFormulario.Secoes.Remove(secao);
            else if (item is LinhaFormulario linha && itemPai is SecaoFormulario secaoPai)
                secaoPai.Linhas.Remove(linha);
            else if (item is CampoFormulario campo && itemPai is LinhaFormulario linhaPai)
                linhaPai.Campos.Remove(campo);

            AtualizarTreeView();
            AtualizarPreview(); // Atualiza o preview
        }

        private void treeViewFormulario_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Vincula a PropertyGrid ao item clicado
            if (e.Node?.Tag != null)
            {
                propertyGridItem.SelectedObject = e.Node.Tag;
            }
        }

        private void propertyGridItem_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            AtualizarTreeView();
            AtualizarPreview();
        }

        private void salvarArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoSalvarArquivo.FileName = "meu_formulario.html";
            dialogoSalvarArquivo.Filter = "Arquivos HTML (*.html)|*.html|Todos os arquivos (*.*)|*.*";
            dialogoSalvarArquivo.Title = "Salvar Formul�rio Como...";

            if (dialogoSalvarArquivo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string htmlFinal = gerador.GerarHtml(definicaoFormulario);

                    File.WriteAllText(dialogoSalvarArquivo.FileName, htmlFinal);

                    MessageBox.Show("Formul�rio salvo com sucesso!",
                                    "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}",
                                    "Erro",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

        }

        private void btnConexao_Click(object sender, EventArgs e)
        {
            if (definicaoFormulario.Conexao == null)
            {
                definicaoFormulario.Conexao = new ConexaoApi();
                definicaoFormulario.Conexao.UrlAcao = "URL-Externa";
                definicaoFormulario.Conexao.Target = "formEnviadoFrame";
                definicaoFormulario.Conexao.Usuario = "nome-usuario";
                definicaoFormulario.Conexao.Senha = "senha";
                // --- FIM DA ATUALIZA��O ---


                AtualizarTreeView();
            }

            propertyGridItem.SelectedObject = definicaoFormulario.Conexao;

            AtualizarPreview();
        }

        private void carregarModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoAbrirModelo.Filter = "Arquivos de Template JSON (*.json)|*.json";
            dialogoAbrirModelo.Title = "Carregar Template de Formul�rio";

            if (dialogoAbrirModelo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = File.ReadAllText(dialogoAbrirModelo.FileName);

                    definicaoFormulario = JsonConvert.DeserializeObject<DefinicaoFormulario>(json);

                    AtualizarTreeView();
                    AtualizarPreview();
                    propertyGridItem.SelectedObject = definicaoFormulario;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar o template: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void salvarModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoSalvarModelo.Filter = "Arquivos de Template JSON (*.json)|*.json";
            dialogoSalvarModelo.Title = "Salvar Template Como...";
            dialogoSalvarModelo.FileName = "novo_template.json";

            if (dialogoSalvarModelo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(definicaoFormulario, Formatting.Indented);

                    File.WriteAllText(dialogoSalvarModelo.FileName, json);

                    MessageBox.Show("Template salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o template: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
