using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OqueVoceOuve : ContentPage
    {
        ISimpleAudioPlayer player;
        ISimpleAudioPlayer player2;
        string resposta;
        bool validado = false;
        Dictionary<int, LicaoOqueVoceOuve> dicLicoes = new Dictionary<int, LicaoOqueVoceOuve>();
        List<LicaoOqueVoceOuve> licoesAExecutar = new List<LicaoOqueVoceOuve>();
        int numeroFase = 0;
        public OqueVoceOuve( int numeroFase)
        {
            this.numeroFase = numeroFase;
            player = CrossSimpleAudioPlayer.Current;
            player2 = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player2 = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player2.Loop = false;
            player.Loop = false;

            CarregaLicoes();

            InitializeComponent();

            carregaFase();

            if(Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] !=0)
            {
                progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]));
            }
            
        }

        void CarregaLicoes()
        {
            if(numeroFase<2)
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
            }
            else
            {
                dicLicoes.Add(0, new LicaoOqueVoceOuve($"Audio.pianomedio.mp3", $"Audio.PianoAgudo.mp3", "btnButton4"));
                dicLicoes.Add(1, new LicaoOqueVoceOuve($"Audio.pianomedio.mp3", $"Audio.PianoGrave.mp3", "btnButton3"));
                dicLicoes.Add(2, new LicaoOqueVoceOuve($"Audio.PianoGrave.mp3", $"Audio.PianoAgudo.mp3", "btnButton4"));
                dicLicoes.Add(3, new LicaoOqueVoceOuve($"Audio.PianoGrave.mp3", $"Audio.pianomedio.mp3", "btnButton4"));
                dicLicoes.Add(4, new LicaoOqueVoceOuve($"Audio.DoPiano.mp3", $"Audio.RePiano.mp3", "btnButton4"));
                dicLicoes.Add(5, new LicaoOqueVoceOuve($"Audio.RePiano.mp3", $"Audio.MiPiano.mp3", "btnButton4"));
                dicLicoes.Add(6, new LicaoOqueVoceOuve($"Audio.FaPiano.mp3", $"Audio.MiPiano.mp3",  "btnButton3"));
                dicLicoes.Add(7, new LicaoOqueVoceOuve($"Audio.SolPiano.mp3", $"Audio.FaPiano.mp3",  "btnButton3"));
                dicLicoes.Add(8, new LicaoOqueVoceOuve($"Audio.LaPiano.mp3", $"Audio.SolPiano.mp3",  "btnButton3"));
                dicLicoes.Add(9, new LicaoOqueVoceOuve($"Audio.SiPiano.mp3", $"Audio.LaPiano.mp3",  "btnButton3"));

            }
           

            for (int i = 0; i < 4; i++)
            {
                int indice = RandomNumber(0, dicLicoes.Count);
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
            if (numeroFase >= 2)
            {
                Task.Delay(2000).Wait();
                player2.Play();
            }
        }

        private void btnButton5_Clicked(object sender, EventArgs e)
        {
            AtualizaProgresso();
        }

        private void AtualizaProgresso()
        {
            if (!validado)
            {
                if (ValidaResposta())
                {
                    progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] + 1));
                    lblResultado.Text = "Correto!!!";
                    stcResult.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    Singleton.Instance.Zeracontagemconquistas();
                    lblResultado.Text = "Incorreto...";
                    stcResult.BackgroundColor = Color.PaleVioletRed;
                }
                btnButton5.Text = "Coninuar";
            }
            else
            {
                Singleton.Instance.IncrementaProgresso(numeroFase);
                carregaFase();
            }

        }

        void carregaFase()
        {
            if (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] < 4)
            {
                player.Load(GetStreamFromFile(licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Audio));
                if (numeroFase >= 2)
                {
                    lblTituloLicao.Text = "Qual a altura do segundo som?";
                    btnButton1.IsVisible = btnButton2.IsVisible = false;
                    btnButton3.IsVisible = btnButton4.IsVisible = true;
                    player2.Load(GetStreamFromFile(licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Audio2));
                }
                else
                {
                    lblTituloLicao.Text = "Que tipo de som você escuta?";
                    btnButton1.IsVisible = btnButton2.IsVisible = true;
                    btnButton3.IsVisible = btnButton4.IsVisible = false;
                }
                    


                validado = false;
                btnButton1.BackgroundColor = btnButton2.BackgroundColor = btnButton3.BackgroundColor = btnButton4.BackgroundColor = Color.Gray;
                stcResult.BackgroundColor = Color.White;
                btnButton5.Text = "Verificar";
            }
            else
            {
                Singleton.Instance.dadosJogador.adcionaNovaFase(this.numeroFase);
                Singleton.Instance.Save();
                LevelPage pagina = new LevelPage();
                Navigation.PushModalAsync(pagina);
            }
            
        }

        private bool ValidaResposta()
        {
            validado = true;
            return resposta == licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Respostacorreta;
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
        public string Audio2 { get; set; }
        public string Respostacorreta { get; set; }

        public LicaoOqueVoceOuve(string Audio, string Respostacorreta)
        {
            this.Audio = Audio;
            this.Respostacorreta = Respostacorreta;
        }

        public LicaoOqueVoceOuve(string Audio, string Audio2, string Respostacorreta)
        {
            this.Audio2 = Audio2;
            this.Audio = Audio;
            this.Respostacorreta = Respostacorreta;
        }
    }
}