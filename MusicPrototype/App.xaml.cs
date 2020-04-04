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
            Load();
            if(Singleton.Instance.dadosJogador.Nome != null)
                MainPage = new LevelPage();//PageInicial();//Perfil(); // QualANota();//OqueVoceOuve();//Ditado();//
            else
                MainPage = new PageInicial();
        }

        protected override void OnStart()
        {
        }
        //Método utilizado para carregar o arquivo onde os dados foram salvos
        public void Load()
        {
            //Obtem o caminho do arquivo
            string destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "MusicPrototype.json");

            //Se o arquivo realmente existir então...
            if (System.IO.File.Exists(destination))
            {
                //Le todo o conteúdo do arquivo e converte em string
                string dataAsJson = File.ReadAllText(destination);
                //Decodifica os dados de Jason para o formato da classe de dados
                PlayerData data = JsonConvert.DeserializeObject<PlayerData>(dataAsJson);

                //Pega a informação 
                Singleton.Instance.dadosJogador.Meta = data.Meta;
                Singleton.Instance.dadosJogador.Nome = data.Nome;
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
