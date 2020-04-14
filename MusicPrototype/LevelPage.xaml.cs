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

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            if(((ImageButton)sender).StyleId == "Fase1")
            {
                if (Singleton.Instance.dadosJogador.ProgressoFase == null)
                {
                    Singleton.Instance.dadosJogador.ProgressoFase = new Dictionary<int, int>();
                    Singleton.Instance.dadosJogador.ProgressoFase.Add(0, 0);
                }
                OqueVoceOuve pagina = new OqueVoceOuve(0);
                Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase2")
            {
                Ditado pagina = new Ditado(1);
                Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase3")
            {
                QualANota pagina = new QualANota(2);
                Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase4")
            {
                OqueVoceOuve pagina = new OqueVoceOuve(3);
                Navigation.PushModalAsync(pagina);
            }

            if (((ImageButton)sender).StyleId == "Fase5")
            {
                QualANota pagina = new QualANota(4);
                Navigation.PushModalAsync(pagina);
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