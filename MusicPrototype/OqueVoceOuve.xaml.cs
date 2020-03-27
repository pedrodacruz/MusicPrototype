using Plugin.SimpleAudioPlayer;
using System;
using System.IO;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OqueVoceOuve : ContentPage
    {
        ISimpleAudioPlayer player;
        string resposta;
        public OqueVoceOuve()
        {
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            player.Load(GetStreamFromFile($"Audio.NaoMusical.wav"));

            InitializeComponent();
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            player.Play();

        }

        private void btnButton5_Clicked(object sender, EventArgs e)
        {
            if (ValidaResposta())
            {
                lblResultado.Text = "Correto!!!";
                stcResult.BackgroundColor = Color.LightGreen;
            }
            else
            {
                lblResultado.Text = "Incorreto...";
                stcResult.BackgroundColor = Color.PaleVioletRed;
            }
            btnButton5.Text = "Coninuar";
        }

        private bool ValidaResposta()
        {
            if(resposta== "btnButton2")
                return true;
            else
                return false;
        }

        private void btnButton_Clicked(object sender, EventArgs e)
        {
            btnButton1.BackgroundColor = btnButton2.BackgroundColor = btnButton3.BackgroundColor = btnButton4.BackgroundColor = Color.Gray;
            ((Button)sender).BackgroundColor = Color.Red;
            resposta = ((Button)sender).StyleId;
        }
    }
}