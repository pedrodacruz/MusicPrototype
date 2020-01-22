using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPrototype
{
    public class Meta
    {
        private Meta()
        {
            
        }

        public string Name { private set; get; }

        public string FriendlyName { private set; get; }

        static Meta()
        {
            List<Meta> all = new List<Meta>();

            all.Add(new Meta() { FriendlyName = "Casual", Name ="5 minutos / dia" });
            all.Add(new Meta() { FriendlyName = "Regular", Name = "10 minutos / dia" });
            all.Add(new Meta() { FriendlyName = "Sério", Name = "15 minutos / dia" });
            all.Add(new Meta() { FriendlyName = "Intensivo", Name = "20 minutos / dia" });

            all.TrimExcess();
            All = all;
        }

        public static IList<Meta> All { private set; get; }
    }
}
