using GeradorFormulario.Core;
using GeradorFormulario.Core.Fabrica;
using GeradorFormulario.Core.Helper;
using GeradorFormulario.Core.Layouts;
using GeradorFormulario.Core.Models;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System.Reflection;

namespace GeradorDeFormulario.Desktop
{
    public partial class TelaPrincipal : Form
    {
        private DefinicaoFormulario definicaoFormulario;
        private GeradorFormularioHtml gerador;
        private string arquivoHtmlTemporario;
        private TabPage paginaDeOpcoes;

        public TelaPrincipal()
        {
            InitializeComponent();
            gerador = new GeradorFormularioHtml();
            this.definicaoFormulario = new DefinicaoFormulario
            {
                TituloHeader = "Novo Formulário em Branco",
                CorPrincipal = "#007bff"
            };

        }
        public TelaPrincipal(DefinicaoFormulario modelo)
        {
            InitializeComponent(); // Sempre chame isso primeiro
            gerador = new GeradorFormularioHtml();

            this.definicaoFormulario = modelo;

        }

        private async void TelaPrincipal_Load(object sender, EventArgs e)
        {
            await visualizadorFormulario.EnsureCoreWebView2Async(null);

            arquivoHtmlTemporario = Path.Combine(Path.GetTempPath(), "form_preview.html");

            cmbCamposDisponiveis.Items.Add("Texto");
            cmbCamposDisponiveis.Items.Add("Email");
            cmbCamposDisponiveis.Items.Add("Senha");
            cmbCamposDisponiveis.Items.Add("Número");
            cmbCamposDisponiveis.Items.Add("Área de Texto");
            cmbCamposDisponiveis.Items.Add("Seleção");
            cmbCamposDisponiveis.Items.Add("Caixa de Seleção");
            cmbCamposDisponiveis.Items.Add("Arquivo");
            cmbCamposDisponiveis.Items.Add("Assinatura");
            cmbCamposDisponiveis.SelectedIndex = 0;

            treeViewFormulario.Tag = definicaoFormulario; // Associa o objeto raiz
            propertyGridItem.SelectedObject = definicaoFormulario;

            this.tabControlEditor.TabPages.Remove(this.tabOpcoes);
            this.paginaDeOpcoes = this.tabOpcoes;
            AtualizarTreeView();
            AtualizarPreview();
        }
        private void AtualizarTreeView()
        {
            treeViewFormulario.Nodes.Clear();
            TreeNode noRaiz = new TreeNode($"Formulário: {definicaoFormulario.TituloHeader}");
            noRaiz.Tag = definicaoFormulario;
            treeViewFormulario.Nodes.Add(noRaiz);
            if (definicaoFormulario.Conexao != null)
            {
                TreeNode noConexao = new TreeNode("Conexão API");
                noConexao.Tag = definicaoFormulario.Conexao; // O nó edita o objeto ConexaoAPI
                noRaiz.Nodes.Add(noConexao);
            }


            foreach (var secao in definicaoFormulario.Secoes)
            {
                TreeNode noSecao = new TreeNode(secao.Titulo ?? "Nova Seção");
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

                // 2. Salva o HTML no arquivo temporário
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
            var novaSecao = new SecaoFormulario { Titulo = "Nova Seção" };
            definicaoFormulario.Secoes.Add(novaSecao);

            AtualizarTreeView();
            AtualizarPreview(); // Atualiza o preview
        }

        private void btnAdicionarCampo_Click(object sender, EventArgs e)
        {
            if (treeViewFormulario.SelectedNode?.Tag is LinhaFormulario linha)
            {
                string campoEscolhido = cmbCamposDisponiveis.SelectedItem.ToString();

                // Adiciona o campo
                switch (campoEscolhido)
                {
                    case "Texto": linha.Campos.Add(FabricaCampos.CriarTexto()); break;
                    case "Email": linha.Campos.Add(FabricaCampos.CriarEmail()); break;
                    case "Senha": linha.Campos.Add(FabricaCampos.CriarSenha()); break;
                    case "Número": linha.Campos.Add(FabricaCampos.CriarNumero()); break;
                    case "Área de Texto": linha.Campos.Add(FabricaCampos.CriarAreaDeTexto()); break;
                    case "Seleção": linha.Campos.Add(FabricaCampos.CriarSelecao()); break;
                    case "Caixa de Seleção": linha.Campos.Add(FabricaCampos.CriarCaixaDeSelecao()); break;
                    case "Arquivo": linha.Campos.Add(FabricaCampos.CriarEnvioArquivo()); break;
                    case "Assinatura": linha.Campos.Add(FabricaCampos.CriarAssinatura()); break;
                }

                AtualizarTreeView();
                AtualizarPreview(); // Atualiza o preview
            }
            else
            {
                MessageBox.Show("Selecione uma Linha na árvore primeiro.");
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
                MessageBox.Show("Selecione uma Seção na árvore primeiro.");
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            var noSelecionado = treeViewFormulario.SelectedNode;
            if (noSelecionado == null || noSelecionado.Tag is DefinicaoFormulario)
            {
                MessageBox.Show("Não é possível remover o formulário raiz.");
                return;
            }

            var itemPai = noSelecionado.Parent.Tag;
            var item = noSelecionado.Tag;

            if (item is ConexaoApi)
            {
                if (MessageBox.Show("Tem certeza que quer remover a Conexão API?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            if (e.Node?.Tag == null)
            {
                if (this.tabControlEditor.TabPages.Contains(paginaDeOpcoes))
                {
                    this.tabControlEditor.TabPages.Remove(paginaDeOpcoes);
                }
                return;
            }

            propertyGridItem.SelectedObject = e.Node.Tag;

            if (e.Node.Tag is CampoFormulario campo && campo.Tipo == GeradorFormulario.Core.Enums.TipoCampo.Selecao)
            {
                // É UM CAMPO DE SELEÇÃO!

                // 3. Mostra a aba de opções (se ela já não estiver lá)
                if (!this.tabControlEditor.TabPages.Contains(paginaDeOpcoes))
                {
                    this.tabControlEditor.TabPages.Add(paginaDeOpcoes);
                }

                // 4. Carrega as opções na lista
                CarregarOpcoesNaLista(campo);

                // 5. Opcional: Foca automaticamente na aba de opções
                this.tabControlEditor.SelectedTab = paginaDeOpcoes;
            }
            else
            {
                // NÃO É UM CAMPO DE SELEÇÃO!

                // 6. Esconde a aba de opções (se ela estiver visível)
                if (this.tabControlEditor.TabPages.Contains(paginaDeOpcoes))
                {
                    this.tabControlEditor.TabPages.Remove(paginaDeOpcoes);
                }
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
            dialogoSalvarArquivo.Title = "Salvar Formulário Como...";

            if (dialogoSalvarArquivo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string htmlFinal = gerador.GerarHtml(definicaoFormulario);

                    File.WriteAllText(dialogoSalvarArquivo.FileName, htmlFinal);

                    MessageBox.Show("Formulário salvo com sucesso!",
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

        private void carregarModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoAbrirModelo.Filter = "Arquivos de Template JSON (*.json)|*.json";
            dialogoAbrirModelo.Title = "Carregar Template de Formulário";

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
        private void CarregarOpcoesNaLista(CampoFormulario campo)
        {
            lstOpcoes.Items.Clear();
            foreach (string opcao in campo.Opcoes)
            {
                lstOpcoes.Items.Add(opcao);
            }
        }

        private void btnAdicionarOpcao_Click(object sender, EventArgs e)
        {
            if (treeViewFormulario.SelectedNode?.Tag is CampoFormulario campo && !string.IsNullOrWhiteSpace(txtNovaOpcao.Text))
            {
                // 2. Adiciona a nova opção à lista de dados
                campo.Opcoes.Add(txtNovaOpcao.Text);

                // 3. Atualiza a UI
                CarregarOpcoesNaLista(campo); // Atualiza o ListBox
                AtualizarPreview();           // Atualiza o WebView2

                txtNovaOpcao.Clear();
                txtNovaOpcao.Focus();
            }
        }

        private void btnRemoverOpcao_Click(object sender, EventArgs e)
        {
            if (treeViewFormulario.SelectedNode?.Tag is CampoFormulario campo && lstOpcoes.SelectedItem != null)
            {
                // 2. Remove a opção da lista de dados
                string opcaoRemover = lstOpcoes.SelectedItem.ToString();
                campo.Opcoes.Remove(opcaoRemover);

                // 3. Atualiza a UI
                CarregarOpcoesNaLista(campo);
                AtualizarPreview();
            }
        }

        private void conexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (definicaoFormulario.Conexao == null)
            {
                definicaoFormulario.Conexao = new ConexaoApi();
                definicaoFormulario.Conexao.UrlAcao = "URL-Externa";
                definicaoFormulario.Conexao.Target = "formEnviadoFrame";
                definicaoFormulario.Conexao.Usuario = "nome-usuario";
                definicaoFormulario.Conexao.Senha = "senha";
                // --- FIM DA ATUALIZAÇÃO ---


                AtualizarTreeView();
            }

            propertyGridItem.SelectedObject = definicaoFormulario.Conexao;

            AtualizarPreview();
        }

        private void exportarProjetoCompletoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dialogoSelecionarPasta.ShowDialog() == DialogResult.OK)
            {
                string pastaDeDestino = dialogoSelecionarPasta.SelectedPath;

                try
                {
                    string nomeDaPastaProjeto = definicaoFormulario.TituloHeader.Replace(" ", "_")
                                                .Replace("(", "").Replace(")", "")
                                                .Replace("/", "-").Replace(":", "-");

                    string pastaRaizProjeto = Path.Combine(pastaDeDestino, nomeDaPastaProjeto);

                    if (Directory.Exists(pastaRaizProjeto))
                    {
                        var resultado = MessageBox.Show(
                            $"A pasta '{pastaRaizProjeto}' já existe.\nDeseja sobrescrever os arquivos?",
                            "Atenção: Pasta existente",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (resultado == DialogResult.No)
                        {
                            return;
                        }
                    }

                    Directory.CreateDirectory(pastaRaizProjeto);

                    string pastaJson = Path.Combine(pastaRaizProjeto, "json");
                    string pastaImg = Path.Combine(pastaRaizProjeto, "img");

                    Directory.CreateDirectory(pastaJson);
                    Directory.CreateDirectory(pastaImg);

                    string jsonModelo = JsonConvert.SerializeObject(definicaoFormulario, Formatting.Indented);
                    string caminhoJson = Path.Combine(pastaJson, "template_formulario.json");
                    File.WriteAllText(caminhoJson, jsonModelo);

                    if (!string.IsNullOrEmpty(definicaoFormulario.UrlLogo) && File.Exists(definicaoFormulario.UrlLogo))
                    {
                        string nomeLogo = Path.GetFileName(definicaoFormulario.UrlLogo);
                        string caminhoImgDestino = Path.Combine(pastaImg, nomeLogo);
                        File.Copy(definicaoFormulario.UrlLogo, caminhoImgDestino, true);

                        definicaoFormulario.UrlLogo = $"img/{nomeLogo}";
                    }

                    string htmlFinal = gerador.GerarHtml(definicaoFormulario);

                    string caminhoHtml = Path.Combine(pastaRaizProjeto, "index.html");
                    File.WriteAllText(caminhoHtml, htmlFinal);

                    if (definicaoFormulario.Confirmacao.Habilitado)
                    {
                        IGeradorLayout layoutGerador;
                        if (definicaoFormulario.EstiloLayout == EstiloDeLayout.Classico)
                            layoutGerador = new GeradorFormulario.Core.Layouts.GeradorLayoutClassico();
                        else 
                            layoutGerador = new GeradorFormulario.Core.Layouts.GeradorLayoutFicheiro();

                        // (Isso assume que 'GerarHtmlConfirmacao' foi adicionado a IGeradorLayout)
                        string htmlObrigado = layoutGerador.GerarHtmlConfirmacao(definicaoFormulario);
                        string caminhoObrigado = Path.Combine(pastaRaizProjeto, "confirmacao.html");
                        File.WriteAllText(caminhoObrigado, htmlObrigado);
                    }

                    AtualizarPreview();
                    propertyGridItem.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o projeto: {ex.Message}",
                                    "Erro",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void criarTermosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TelaEditarTermos telaEdicao = new TelaEditarTermos(definicaoFormulario.Termos))
            {
                if (telaEdicao.ShowDialog() == DialogResult.OK)
                {
                    definicaoFormulario.Termos = telaEdicao.ConfigTermos;

                    AtualizarPreview();
                }
            }
        }

        private void telaDeConfirmaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TelaEditarConfirmacao telaConfirmacao = new TelaEditarConfirmacao(definicaoFormulario.Confirmacao))
            {
                if (telaConfirmacao.ShowDialog() == DialogResult.OK)
                {


                    AtualizarPreview();
                }
            }
        }
    }
}
