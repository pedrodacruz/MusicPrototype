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
            if(Singleton.Instance.dadosJogador.ProgressoFase == null)
            {
                Singleton.Instance.dadosJogador.ProgressoFase = new Dictionary<int, int>();
                Singleton.Instance.dadosJogador.ProgressoFase.Add(0, 0);
            }
            OqueVoceOuve pagina = new OqueVoceOuve();
            Navigation.PushModalAsync(pagina);
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
                    
                }
            }
            
            
        }
    }
}