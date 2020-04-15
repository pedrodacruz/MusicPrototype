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
    public partial class LevelPage : ContentPage
    {
        public LevelPage()
        {
            InitializeComponent();
            loadProgress();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Perfil pagina = new Perfil();
            Navigation.PushModalAsync(pagina);
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            PageInicial pagina = new PageInicial();
            Navigation.PushModalAsync(pagina);
        }

        async void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            int indiceBotao = Convert.ToInt32(((ImageButton)sender).StyleId.Substring(4));
            if (Singleton.Instance.dadosJogador.ProgressoFase == null)
            {
                Singleton.Instance.dadosJogador.ProgressoFase = new Dictionary<int, int>();
                Singleton.Instance.dadosJogador.ProgressoFase.Add(0, 0);
            }
            else
            {
                if (indiceBotao <= Singleton.Instance.dadosJogador.ProgressoFase.Count - 1 && Singleton.Instance.dadosJogador.ProgressoFase.Count != indiceBotao)
                {
                    bool answer = await DisplayAlert("Atenção!", "Voê gostaria de rejogar esta fase?", "Sim", "Não");
                    if (answer)
                    {
                        await AbreATela(sender);
                    }
                    else
                        return;
                }
            }

            await AbreATela(sender);
        }

        private async Task AbreATela(object sender)
        {
            if (((ImageButton)sender).StyleId == "Fase1")
            {

                OqueVoceOuve pagina = new OqueVoceOuve(0);
                await Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase2")
            {
                Ditado pagina = new Ditado(1);
                await Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase3")
            {
                QualANota pagina = new QualANota(2);
                await Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase4")
            {
                OqueVoceOuve pagina = new OqueVoceOuve(3);
                await Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase5")
            {
                QualANota pagina = new QualANota(4);
                await Navigation.PushModalAsync(pagina);
            }
        }

        void loadProgress()
        {
            if(Singleton.Instance.dadosJogador.ProgressoFase!= null)
            {
                foreach (var item in Singleton.Instance.dadosJogador.ProgressoFase)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        ((Image)this.FindByName(string.Format("Fase{0}progress{1}", item.Key + 1, i+1))).Source = ImageSource.FromResource("MusicPrototype.Images.ActiveSeminima.jpg", typeof(LevelPage).GetTypeInfo().Assembly);
                    }
                    if (item.Key > 0)
                    {
                        if(item.Key + 1 < Singleton.Instance.dadosJogador.ProgressoFase.Count )
                        {
                            ((ImageButton)this.FindByName(string.Format("Fase{0}", item.Key + 1))).Source = ImageSource.FromResource("MusicPrototype.Images.ActiveSemibreve.jpg", typeof(LevelPage).GetTypeInfo().Assembly);
                            ((ImageButton)this.FindByName(string.Format("Fase{0}", item.Key + 1))).IsEnabled = true;
                        }
                        
                    }

                }
                
            }
            
            
        }
    }
}