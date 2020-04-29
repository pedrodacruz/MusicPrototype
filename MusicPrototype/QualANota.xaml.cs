using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QualANota : ContentPage
    {
        string resposta;
        bool validado = false;
        Dictionary<int, LicaoQualANota> dicLicoes = new Dictionary<int, LicaoQualANota>();
        List<LicaoQualANota> licoesAExecutar = new List<LicaoQualANota>();
        int numeroFase = 0;
        
        public QualANota(int numeroFase)
        {
            this.numeroFase = numeroFase;
            CarregaLicoes();

            InitializeComponent();

            carregaFase();

            if (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] != 0)
            {
                progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]));
            }

           
        }

        void CarregaLicoes()
        {
            if(numeroFase<=2)
            {
                dicLicoes.Add(0, new LicaoQualANota($"MusicPrototype.Images.Notas.DoGraveClaveSol.jpg", "btnDo"));
                dicLicoes.Add(1, new LicaoQualANota($"MusicPrototype.Images.Notas.DoClaveSol.jpg", "btnDo"));
                dicLicoes.Add(2, new LicaoQualANota($"MusicPrototype.Images.Notas.ReClaveSol.jpg", "btnRe"));
                dicLicoes.Add(3, new LicaoQualANota($"MusicPrototype.Images.Notas.ReGraveClaveSol.jpg", "btnRe"));
                dicLicoes.Add(4, new LicaoQualANota($"MusicPrototype.Images.Notas.MiClaveSol.jpg", "btnMi"));
                dicLicoes.Add(5, new LicaoQualANota($"MusicPrototype.Images.Notas.MiMedioClaveSol.jpg", "btnMi"));
                dicLicoes.Add(6, new LicaoQualANota($"MusicPrototype.Images.Notas.FaClaveSol.jpg", "btnfa"));
                dicLicoes.Add(7, new LicaoQualANota($"MusicPrototype.Images.Notas.FaMedioClaveSol.jpg", "btnFa"));
                dicLicoes.Add(8, new LicaoQualANota($"MusicPrototype.Images.Notas.SolAgudoClaveSol.jpg", "btnSol"));
                dicLicoes.Add(9, new LicaoQualANota($"MusicPrototype.Images.Notas.LaClaveSol.jpg", "btnLa"));
                dicLicoes.Add(10, new LicaoQualANota($"MusicPrototype.Images.Notas.SiClaveSol.jpg", "btnSi"));
            }
            else
            {
                dicLicoes.Add(0, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.DoAgudoClaveSol.jpg", "btnDo"));
                dicLicoes.Add(1, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.FaAgudoClaveSol.jpg", "btnFa"));
                dicLicoes.Add(2, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.FaGraveClaveSol.jpg", "btnFa"));
                dicLicoes.Add(3, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.LaAgudoClaveSol.jpg", "btnLa"));
                dicLicoes.Add(4, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.LaGraveClaveSol.jpg", "btnLa"));
                dicLicoes.Add(5, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.MiAgudoClaveSol.jpg", "btnMi"));
                dicLicoes.Add(6, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.MiGraveClaveSol.jpg", "btnMi"));
                dicLicoes.Add(7, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.ReAgudoClaveSol.jpg", "btnFa"));
                dicLicoes.Add(8, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.SiAgudoClaveSol.jpg", "btnSi"));
                dicLicoes.Add(9, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.SiGraveClaveSol.jpg", "btnSi"));
                dicLicoes.Add(10, new LicaoQualANota($"MusicPrototype.Images.Notas.Suplementares.SolGraveClaveSol.jpg", "btnSol"));
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

        void carregaFase()
        {
            if (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] < 4)
            {
                imgNota.Source = ImageSource.FromResource(licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Imagem, typeof(QualANota).GetTypeInfo().Assembly);
                validado = false;
                btnDo.BackgroundColor = btnRe.BackgroundColor = btnMi.BackgroundColor = btnFa.BackgroundColor = btnSol.BackgroundColor = btnLa.BackgroundColor = btnSi.BackgroundColor = Color.Gray;
                stcResult.BackgroundColor = Color.White;
                btnButton5.Text = "Verificar";
            }
            else
            {
                Singleton.Instance.dadosJogador.adcionaNovaFase(this.numeroFase);
                Singleton.Instance.Save();
                //LevelPage pagina = new LevelPage();
                //Navigation.PushModalAsync(pagina);
                Navigation.PopModalAsync();
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

        private bool ValidaResposta()
        {
            validado = true;
            return resposta == licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Respostacorreta;
        }

        private void btnButton_Clicked(object sender, EventArgs e)
        {
            btnDo.BackgroundColor = btnRe.BackgroundColor = btnMi.BackgroundColor = btnFa.BackgroundColor = btnSol.BackgroundColor = btnLa.BackgroundColor = btnSi.BackgroundColor = Color.Gray;
            ((Button)sender).BackgroundColor = Color.Red;
            resposta = ((Button)sender).StyleId;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            //LevelPage pagina = new LevelPage();
            //Navigation.PushModalAsync(pagina);
            Navigation.PopModalAsync();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }

    class LicaoQualANota
    {
        public string Imagem { get; set; }
        public string Respostacorreta { get; set; }

        public LicaoQualANota(string Imagem, string Respostacorreta)
        {
            this.Imagem = Imagem;
            this.Respostacorreta = Respostacorreta;
        }
    }
}