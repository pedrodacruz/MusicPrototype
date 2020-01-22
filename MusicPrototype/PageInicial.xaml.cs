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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInicial : ContentPage
    {
        ISimpleAudioPlayer player;
        PlayerData playerData = new PlayerData();
        public PageInicial()
        {
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            player.Load(GetStreamFromFile($"Audio.NaoMusical.wav"));

            InitializeComponent();

            Load();

            btnPlay.Clicked += BtnPlay_Clicked;

            if(this.playerData.Meta!=null)
            MetaText.Text = this.playerData.Meta.ToString();
        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {
            playerData.Meta = ListaMeta.SelectedItem;
            Save();
            player.Play();

            LevelPage pagina = new LevelPage();
            Navigation.PushModalAsync(pagina);
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
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
                this.playerData.Meta = data.Meta;
               
            }

        }

        //Método utlizado para salvar os dados em um arquivo texto
        public void Save()
        {
            //Destino do arquivo
            string destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "MusicPrototype.json");

            //Instancia da classe de dados
            PlayerData data = new PlayerData();

            data.Meta = this.playerData.Meta;
 
            //Obtenção do texto a partir da classe jason
            string dataAsJson = JsonConvert.SerializeObject(data);

            //Gravação do arquivo jason no destino
            File.WriteAllText(destination, dataAsJson);

   

        }

        private void ListaMeta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }

    //Classe criada para armazenar as informações do jogo
    [System.Serializable]
    public class PlayerData
    {
        public object Meta;
    }
}