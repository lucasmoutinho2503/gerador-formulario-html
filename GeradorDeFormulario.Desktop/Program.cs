using GeradorFormulario.Core.Models;

namespace GeradorDeFormulario.Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            TelaSelecaoModelo telaSelecao = new TelaSelecaoModelo();

            // 2. Mostra a tela como um diálogo (pausa o código)
            if (telaSelecao.ShowDialog() == DialogResult.OK)
            {
                // 3. O usuário clicou "OK". Pegamos o template que ele escolheu
                DefinicaoFormulario templateCarregado = telaSelecao.ModeloSelecionado;

                // 4. Inicia o Form1, passando o template para ele
                Application.Run(new TelaPrincipal(templateCarregado));
            }
            else
            {
                // 5. O usuário clicou "Cancelar" ou fechou a tela, então o app fecha
                Application.Exit();
            }
        }
    }
}