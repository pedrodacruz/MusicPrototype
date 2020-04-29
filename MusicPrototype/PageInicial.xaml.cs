using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using System.Json;
using Newtonsoft.Json;
using SkiaSharp;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicial : ContentPage
    {
        ISimpleAudioPlayer player;
        bool isInicial = false;
        //PlayerData playerData = new PlayerData();
        public PageInicial(bool inicial)
        {
            isInicial = inicial;
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            player.Load(GetStreamFromFile($"Audio.NaoMusical.mp3"));

            InitializeComponent();

            Singleton.Instance.Load();
            if (Singleton.Instance.dadosJogador.Meta != null)
                ((Button)this.FindByName(Singleton.Instance.dadosJogador.Meta)).BackgroundColor = Color.Black;

            btnPlay.Clicked += BtnPlay_Clicked;
            InputName.Text = Singleton.Instance.dadosJogador.Nome;
        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {
            Singleton.Instance.dadosJogador.Nome = this.InputName.Text.ToString();

            Singleton.Instance.Save();
            player.Play();

            if(isInicial)
            {
                LevelPage pagina = new LevelPage();
                Navigation.PushModalAsync(pagina);
            }
            else
            {
                Navigation.PopModalAsync();
            }
            
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
        }

        


        
        private void btnMeta_Clicked(object sender, EventArgs e)
        {
            btn10minutos.BackgroundColor = btn15minutos.BackgroundColor = btn20minutos.BackgroundColor = btn5minutos.BackgroundColor = Color.Gray;
            ((Button)sender).BackgroundColor = Color.Black;

            Singleton.Instance.dadosJogador.Meta = ((Button)sender).StyleId;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (isInicial)
            {
                LevelPage pagina = new LevelPage();
                Navigation.PushModalAsync(pagina);
            }
            else
            {
                Navigation.PopModalAsync();
            }
        }
    }


}