using GeradorFormulario.Core.Helper;
using GeradorFormulario.Core.Models;
using Newtonsoft.Json;
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
    public partial class TelaSelecaoModelo : Form
    {
        public DefinicaoFormulario ModeloSelecionado { get; private set; }

        private List<ModeloCardControl> _todosOsCartoes = new List<ModeloCardControl>();
        private ModeloCardControl _cartaoSelecionado = null;

        public TelaSelecaoModelo()
        {
            InitializeComponent();

            btnSelecionar.Enabled = false;
            btnSelecionar.DialogResult = DialogResult.OK;
            btnCancelar.DialogResult = DialogResult.Cancel;
        }
        private async void TelaSelecaoModelo_Load(object sender, EventArgs e)
        {
            CarregarCategorias();
            CarregarFiltrosComboBoxes();
            CarregarTodosOsModelos();


            this.listBoxModelos.SelectedIndexChanged += OnFiltroChanged;
            this.txtBusca.TextChanged += OnFiltroChanged;

            this.comboBoxIdioma.SelectedIndexChanged += OnFiltroChanged;
            this.comboBoxCategoria.SelectedIndexChanged += OnFiltroChanged;
            this.comboBoxTipo.SelectedIndexChanged += OnFiltroChanged;

            FiltrarModelosVisiveis();

        }
        private void CarregarCategorias()
        {
            listBoxModelos.Items.Clear();
            listBoxModelos.Items.Add("Todos");
            listBoxModelos.Items.Add("Recursos Humanos");
            listBoxModelos.Items.Add("Hotel");
            listBoxModelos.SelectedIndex = 0;
        }
        private void CarregarFiltrosComboBoxes()
        {
            comboBoxIdioma.Items.Add("Todas as Linguagens");
            comboBoxIdioma.Items.Add("PT-BR");
            comboBoxIdioma.SelectedIndex = 0;

            comboBoxCategoria.Items.Add("Todas as Categorias");
            comboBoxCategoria.Items.Add("Recursos Humanos");
            comboBoxCategoria.Items.Add("Hotelaria");
            comboBoxCategoria.SelectedIndex = 0;

            comboBoxTipo.Items.Add("Todos os Tipos"); // Posição 0
            comboBoxTipo.Items.Add("Admissão");
            comboBoxTipo.Items.Add("Cadastro");
            comboBoxTipo.SelectedIndex = 0;
        }

        private void CarregarTodosOsModelos()
        {
            _todosOsCartoes.Clear();
            string pastaModelos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modelos");

            if (!Directory.Exists(pastaModelos))
            {
                Directory.CreateDirectory(pastaModelos);
                return;
            }
            foreach (string arquivoJson in Directory.GetFiles(pastaModelos, "*.json"))
            {
                try
                {
                    string json = File.ReadAllText(arquivoJson);
                    var def = JsonConvert.DeserializeObject<DefinicaoFormulario>(json);

                    string nomeModelo = Path.GetFileNameWithoutExtension(arquivoJson);
                    string arquivoPng = Path.Combine(pastaModelos, nomeModelo + ".png");

                    ModeloCardControl cartao = new ModeloCardControl();
                    cartao.Titulo = def.NomeFormulario ?? nomeModelo;
                    cartao.Descricao = def.SubtituloHeader ?? "Template de formulário.";
                    cartao.CaminhoJsonTemplate = arquivoJson;
                    cartao.Tags = def.Tags ?? new List<string> { "Geral" };

                    if (File.Exists(arquivoPng))
                    {
                        cartao.Icone = Image.FromFile(arquivoPng);
                    }

                    cartao.Click += Cartao_Click;
                    _todosOsCartoes.Add(cartao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar o template '{arquivoJson}': {ex.Message}");
                }
            }
        }

        private void FiltrarModelosVisiveis()
        {
            flowPanelModelos.Controls.Clear();

            string categoria = listBoxModelos.SelectedItem?.ToString() ?? "Todos";
            string pesquisa = txtBusca.Text.ToLower();
            string filtroIdioma = comboBoxIdioma.SelectedItem?.ToString() ?? "Todas as Linguagens";
            string filtroCategoria = comboBoxCategoria.SelectedItem?.ToString() ?? "Todas as Categorias";
            string filtroTipoFormulario = comboBoxTipo.SelectedItem?.ToString() ?? "Todos os Tipos";

            foreach (var cartao in _todosOsCartoes)
            {
                var tagsDoCartao = cartao.Tags;
                bool CategoriaOK = (categoria == "Todos") || (cartao.Tags.Contains(categoria));
                bool PesquisaOK = string.IsNullOrEmpty(pesquisa) ||
                                  cartao.Titulo.ToLower().Contains(pesquisa) ||
                                  cartao.Descricao.ToLower().Contains(pesquisa);

                bool filtroIdioma_OK = (filtroIdioma == "Todas as Linguagens") || (tagsDoCartao.Contains(filtroIdioma));
                bool filtroCategoria_OK = (filtroCategoria == "Todas as Categorias") || (tagsDoCartao.Contains(filtroCategoria));
                bool filtroTipoFormulario_OK = (filtroTipoFormulario == "Todos os Tipos") || (tagsDoCartao.Contains(filtroTipoFormulario));

                if (CategoriaOK && PesquisaOK && filtroIdioma_OK && filtroCategoria_OK && filtroTipoFormulario_OK)
                {
                    flowPanelModelos.Controls.Add(cartao);
                }
            }
        }

        private void OnFiltroChanged(object sender, EventArgs e)
        {
            FiltrarModelosVisiveis();
        }

        private void Cartao_Click(object sender, EventArgs e)
        {
            ModeloCardControl cartaoClicado = (ModeloCardControl)sender;

            if (_cartaoSelecionado != null)
            {
                _cartaoSelecionado.Desselecionar();
            }

            _cartaoSelecionado = cartaoClicado;
            _cartaoSelecionado.Selecionar();

            btnSelecionar.Enabled = true;
        }

        private void Cartao_DoubleClick(object sender, EventArgs e)
        {
            btnSelecionar_Click(sender, e);
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (_cartaoSelecionado != null)
            {
                try
                {
                    // Carrega o JSON do cartão selecionado
                    string json = File.ReadAllText(_cartaoSelecionado.CaminhoJsonTemplate);
                    this.ModeloSelecionado = JsonConvert.DeserializeObject<DefinicaoFormulario>(json);

                    // O DialogResult (definido no Design) fará o resto
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar o template: {ex.Message}");
                    this.DialogResult = DialogResult.None; // Impede o form de fechar
                }
            }
            else
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("Por favor, selecione um template.", "Nenhum Template Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNovoEmBranco_Click(object sender, EventArgs e)
        {
            var formularioEmBranco = new DefinicaoFormulario();

            // 2. Pré-define alguns valores (opcional, mas bom)
            formularioEmBranco.TituloHeader = "Novo Formulário";
            formularioEmBranco.NomeFormulario = "Novo Formulário";
            formularioEmBranco.EstiloLayout = EstiloDeLayout.Ficheiro; // Começa como Clássico

            // 3. Define este objeto em branco como o "ModeloSelecionado"
            this.ModeloSelecionado = formularioEmBranco;

            // 4. Diz ao Program.cs que o usuário clicou "OK" e fecha
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
