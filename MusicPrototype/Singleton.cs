using System;
using System.Collections.Generic;
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
    }
}
