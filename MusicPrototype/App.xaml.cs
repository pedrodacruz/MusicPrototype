using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MusicPrototype
{
    public partial class App : Application
    {
        const int smallWightResolution = 768;
        const int smallHeightResolution = 1280;
        public App()
        {
            InitializeComponent();

            LoadStyles();

            Singleton.Instance.Load();
            Singleton.Instance.tempoInicioJogo = DateTime.Now;

            if (Singleton.Instance.dadosJogador.Nome != null)
                MainPage = new LevelPage();//PageInicial();//Perfil(); // QualANota();//OqueVoceOuve();//Ditado();//
            else
                MainPage = new PageInicial();
        }

        protected override void OnStart()
        {
        }


        protected override void OnSleep()
        {
            Singleton.Instance.VerificaTempoJogador();
        }

        protected override void OnResume()
        {
        }

        void LoadStyles()
        {
            if (IsASmallDevice())
            {
                dictionary.MergedDictionaries.Add(SmallDevicesStyle.SharedInstance);
            }
            else
            {
                dictionary.MergedDictionaries.Add(GeneralDevicesStyle.SharedInstance);
            }
        }

        public static bool IsASmallDevice()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            return (width <= smallWightResolution && height <= smallHeightResolution);
        }
    }
}
