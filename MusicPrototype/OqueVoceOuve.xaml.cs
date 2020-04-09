using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
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
        bool validado = false;
        Dictionary<int, LicaoOqueVoceOuve> dicLicoes = new Dictionary<int, LicaoOqueVoceOuve>();
        List<LicaoOqueVoceOuve> licoesAExecutar = new List<LicaoOqueVoceOuve>();
        public OqueVoceOuve()
        {
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            CarregaLicoes();

            InitializeComponent();

            carregaFase();

            if(Singleton.Instance.dadosJogador.ProgressoFase[0]!=0)
            {
                progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[0]));
            }
            
        }

        void CarregaLicoes()
        {
            dicLicoes.Add(0, new LicaoOqueVoceOuve($"Audio.NaoMusical.mp3", "btnButton2"));
            dicLicoes.Add(1, new LicaoOqueVoceOuve($"Audio.pianomedio.mp3", "btnButton1"));
            dicLicoes.Add(2, new LicaoOqueVoceOuve($"Audio.DoReMiTrompete.mp3", "btnButton1"));
            dicLicoes.Add(3, new LicaoOqueVoceOuve($"Audio.Furadeira.mp3", "btnButton2"));
            dicLicoes.Add(4, new LicaoOqueVoceOuve($"Audio.Agua1.mp3", "btnButton2"));
            dicLicoes.Add(5, new LicaoOqueVoceOuve($"Audio.Buzina1.mp3", "btnButton2"));
            dicLicoes.Add(6, new LicaoOqueVoceOuve($"Audio.Descarga1.mp3", "btnButton2"));
            dicLicoes.Add(7, new LicaoOqueVoceOuve($"Audio.DoAgudoPianomp3.mp3", "btnButton1"));
            dicLicoes.Add(8, new LicaoOqueVoceOuve($"Audio.Agua3.mp3", "btnButton2"));
            dicLicoes.Add(9, new LicaoOqueVoceOuve($"Audio.PianoGrave.mp3", "btnButton1"));
            dicLicoes.Add(10, new LicaoOqueVoceOuve($"Audio.Doflauta.mp3", "btnButton1"));
            dicLicoes.Add(11, new LicaoOqueVoceOuve($"Audio.DoTuba.mp3", "btnButton1"));

            for (int i = 0; i < 4; i++)
            {
                int indice = RandomNumber(0, 11);
                if (!licoesAExecutar.Contains(dicLicoes[indice]))
                {
                    licoesAExecutar.Add(dicLicoes[indice]);
                }
                else
                    i--;
                
            }
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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
            if(!validado)
            {
                if (ValidaResposta())
                {
                    progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[0]+1));
                    lblResultado.Text = "Correto!!!";
                    stcResult.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    lblResultado.Text = "Incorreto...";
                    stcResult.BackgroundColor = Color.PaleVioletRed;
                }
            }
            else
            {
                Singleton.Instance.dadosJogador.ProgressoFase[0]++;
                carregaFase();
            }
            btnButton5.Text = "Coninuar";
        }
        void carregaFase()
        {
            if (Singleton.Instance.dadosJogador.ProgressoFase[0] < 4)
            {
                player.Load(GetStreamFromFile(licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[0]].Audio));
                validado = false;
                btnButton1.BackgroundColor = btnButton2.BackgroundColor = btnButton3.BackgroundColor = btnButton4.BackgroundColor = Color.Gray;
                stcResult.BackgroundColor = Color.White;
                btnButton5.Text = "Verificar";
            }
            else
            {
                Singleton.Instance.dadosJogador.adcionaNovaFase(0);
                LevelPage pagina = new LevelPage();
                Navigation.PushModalAsync(pagina);
            }
            
        }

        private bool ValidaResposta()
        {
            validado = true;
            return resposta == licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[0]].Respostacorreta;
        }

        private void btnButton_Clicked(object sender, EventArgs e)
        {
            btnButton1.BackgroundColor = btnButton2.BackgroundColor = btnButton3.BackgroundColor = btnButton4.BackgroundColor = Color.Gray;
            ((Button)sender).BackgroundColor = Color.Red;
            resposta = ((Button)sender).StyleId;
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            LevelPage pagina = new LevelPage();
            Navigation.PushModalAsync(pagina);
        }
    }

    class LicaoOqueVoceOuve
    {
        public string Audio { get; set; }
        public string Respostacorreta { get; set; }

        public LicaoOqueVoceOuve(string Audio, string Respostacorreta)
        {
            this.Audio = Audio;
            this.Respostacorreta = Respostacorreta;
        }
    }
}