using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QualANota : ContentPage
    {
        string resposta;

        string resourceDoGrave = "MusicPrototype.Images.Notas.DoGraveClaveSol.jpg";
        string resourceDo = "MusicPrototype.Images.Notas.DoClaveSol.jpg";

        string resourceRe = "MusicPrototype.Images.Notas.ReClaveSol.jpg";
        string resourceReGrave = "MusicPrototype.Images.Notas.ReGraveClaveSol.jpg";

        string resourceMi = "MusicPrototype.Images.Notas.MiClaveSol.jpg";
        string resourceMiMedio = "MusicPrototype.Images.Notas.MiMedioClaveSol.jpg";

        string resourceFaMedio = "MusicPrototype.Images.Notas.FaClaveSol.jpg";
        string resourceFa = "MusicPrototype.Images.Notas.FaMedioClaveSol.jpg";

        string resourceSolAgudo = "MusicPrototype.Images.Notas.SolAgudoClaveSol.jpg";
        string resourceSol = "MusicPrototype.Images.Notas.SolClaveSol.jpg";

        string resourceLa = "MusicPrototype.Images.Notas.LaClaveSol.jpg";

        string resourceSi = "MusicPrototype.Images.Notas.SiClaveSol.jpg";

        public QualANota()
        {
            
            InitializeComponent();
            imgNota.Source = ImageSource.FromResource(resourceDo, typeof(QualANota).GetTypeInfo().Assembly); ;
        }

        private void btnButton5_Clicked(object sender, EventArgs e)
        {
            if (ValidaResposta())
            {
                lblResultado.Text = "Correto!!!";
                stcResult.BackgroundColor = Color.LightGreen;
            }
            else
            {
                lblResultado.Text = "Incorreto...";
                stcResult.BackgroundColor = Color.PaleVioletRed;
            }
            btnButton5.Text = "Coninuar";
        }

        private bool ValidaResposta()
        {
            if (resposta == "btnDo")
                return true;
            else
                return false;
        }

        private void btnButton_Clicked(object sender, EventArgs e)
        {
            btnDo.BackgroundColor = btnRe.BackgroundColor = btnMi.BackgroundColor = btnFa.BackgroundColor = btnSol.BackgroundColor = btnLa.BackgroundColor = btnSi.BackgroundColor = Color.Gray;
            ((Button)sender).BackgroundColor = Color.Red;
            resposta = ((Button)sender).StyleId;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            LevelPage pagina = new LevelPage();
            Navigation.PushModalAsync(pagina);
        }
    }
}