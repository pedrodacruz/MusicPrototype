using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using System.Json;
using Newtonsoft.Json;

namespace MusicPrototype
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

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
    }
}
