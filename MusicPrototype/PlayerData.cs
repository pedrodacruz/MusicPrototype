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

        public void adcionaNovaFase(int faseAtual)
        {
            if(ProgressoFase!= null)
            {
                if(ProgressoFase.Count<= faseAtual+1)
                    ProgressoFase.Add(ProgressoFase.Count, 0);
            }
            else
            {
                ProgressoFase = new Dictionary<int, int>();
            }
        }
    }
}
