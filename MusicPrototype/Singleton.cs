using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicPrototype
{
    public sealed class Singleton
    {
        public PlayerData dadosJogador = new PlayerData();

        private static Singleton instance = null;
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static Singleton()
        {

        }
        private Singleton()
        {
        }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

        //Método utlizado para salvar os dados em um arquivo texto
        public void Save()
        {
            //Destino do arquivo
            string destination = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "MusicPrototype.json");

            //Instancia da classe de dados
            PlayerData data = new PlayerData();

            data.Meta = Singleton.Instance.dadosJogador.Meta;
            data.Nome = Singleton.Instance.dadosJogador.Nome;
            data.ProgressoFase = Singleton.Instance.dadosJogador.ProgressoFase;

            //Obtenção do texto a partir da classe jason
            string dataAsJson = JsonConvert.SerializeObject(data);

            //Gravação do arquivo jason no destino
            File.WriteAllText(destination, dataAsJson);
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
                Singleton.Instance.dadosJogador.ProgressoFase = data.ProgressoFase;


            }

        }
    }
}
