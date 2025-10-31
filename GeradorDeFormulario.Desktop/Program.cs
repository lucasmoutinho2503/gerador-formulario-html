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

            // 2. Mostra a tela como um di�logo (pausa o c�digo)
            if (telaSelecao.ShowDialog() == DialogResult.OK)
            {
                // 3. O usu�rio clicou "OK". Pegamos o template que ele escolheu
                DefinicaoFormulario templateCarregado = telaSelecao.ModeloSelecionado;

                // 4. Inicia o Form1, passando o template para ele
                Application.Run(new TelaPrincipal(templateCarregado));
            }
            else
            {
                // 5. O usu�rio clicou "Cancelar" ou fechou a tela, ent�o o app fecha
                Application.Exit();
            }
        }
    }
}