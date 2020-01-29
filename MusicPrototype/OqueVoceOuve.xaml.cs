using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OqueVoceOuve : ContentPage
    {
        ISimpleAudioPlayer player;
        public OqueVoceOuve()
        {
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            player.Load(GetStreamFromFile($"Audio.NaoMusical.wav"));

            InitializeComponent();
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            player.Play();

        }
    }
}