using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using System.Json;
using Newtonsoft.Json;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();

            txtNome.Text = Singleton.Instance.dadosJogador.Nome.ToString();

            carregaProgresso();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            LevelPage pagina = new LevelPage();
            Navigation.PushModalAsync(pagina);
        }

        public void carregaProgresso()
        {
            if (Singleton.Instance.dadosJogador.ProgressoFase == null)
            {
                Singleton.Instance.novoJogo();
            }
            StatusNaMosca.Text = ((int)Singleton.Instance.dadosJogador.ProgressoConquistas[1] / 4).ToString() + "/1";
            StatusSuperacaoTotal.Text = ((int)Singleton.Instance.dadosJogador.ProgressoConquistas[1] / 4).ToString() + "/3";
            progressNaMosca.Progress = ((double)Singleton.Instance.dadosJogador.ProgressoConquistas[0])/ 4;
            progressSuperacaoTotal.Progress = ((double)Singleton.Instance.dadosJogador.ProgressoConquistas[1]) / 12;
        }
    }
}