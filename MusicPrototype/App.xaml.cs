using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MusicPrototype
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //var imagem1 = new Image
            //{
            //    Source = "Primeiro.jpg",
            //    Aspect = Aspect.AspectFit,
            //    HorizontalOptions = LayoutOptions.Fill,
            //    VerticalOptions = LayoutOptions.Fill
            //};

            //var imagemLevel = new Image
            //{
            //    Source = "ActiveStarSemibbreve.jpg",
            //    Aspect = Aspect.AspectFit,
            //    HorizontalOptions = LayoutOptions.Fill,
            //    VerticalOptions = LayoutOptions.Fill
            //};

            MainPage = new OqueVoceOuve();//Ditado();//QualANota();////LevelPage();// PageInicial();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
