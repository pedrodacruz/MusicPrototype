using System;
using System.Collections.Generic;
using System.Linq;
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
            OqueVoceOuve pagina = new OqueVoceOuve();
            Navigation.PushModalAsync(pagina);
        }
    }
}