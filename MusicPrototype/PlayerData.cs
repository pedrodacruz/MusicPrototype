using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPrototype
{
    //Classe criada para armazenar as informações do jogo
    [System.Serializable]
    public class PlayerData
    {
        public string Meta;
        public string Nome;
        public Dictionary<int, int> ProgressoFase;
    }
}
