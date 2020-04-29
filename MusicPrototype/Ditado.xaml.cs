using Plugin.SimpleAudioPlayer;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;
using System;



namespace MusicPrototype
{
    public partial class Ditado : ContentPage
    {
        int xValue = 70;
        int yValue = 50;

        ISimpleAudioPlayer player;

        bool validado = false;
        Dictionary<int, LicaoDitado> dicLicoes = new Dictionary<int, LicaoDitado>();
        List<LicaoDitado> licoesAExecutar = new List<LicaoDitado>();

        string resourceBarraHorizontal = "MusicPrototype.Images.BarraHorizontal.jpg";
        string resourceBarraVertical = "MusicPrototype.Images.BarraVertical.jpg";

        SKCanvas canvas;

        List<TouchManipulationBitmap> bitmapCollection = new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> VerticalbitmapCollection = new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> InvisiblebitmapCollection = new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> VisiblebitmapCollection = new List<TouchManipulationBitmap>();

        Dictionary<long, TouchManipulationBitmap> bitmapDictionary = new Dictionary<long, TouchManipulationBitmap>();
        int numeroFase = 0;
        public Ditado(int numeroFase)
        {
            this.numeroFase = numeroFase;
            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            CarregaLicoes();

            InitializeComponent();

            CarregaBarras();

            carregaFase();

            if (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] != 0)
            {
                progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]));
            }
        }

        private void CarregaBarras()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            // Load in all the available bitmaps
            SKPoint position = new SKPoint();

            VisiblebitmapCollection.Clear();
            canvasView.InvalidateSurface();

            //Barras Verticais
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraVertical))
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);

                position.X = xValue+150;
                position.Y = yValue;

                for (int i = 0; i < 6; i++)
                {

                    VerticalbitmapCollection.Add(new TouchManipulationBitmap(bitmap)
                    {
                        Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                    });
                    position.X += xValue;
                }
            }

            //Barra horizontal
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraHorizontal))
            {
                position.X = 350;
                position.Y = 179;
                SKBitmap bitmap = SKBitmap.Decode(stream);
                bitmapCollection.Add(new TouchManipulationBitmap(bitmap)
                {
                    Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                });
            }

            //Barra horizontal (invisíveis)
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraHorizontal))
            {
                position.X = xValue+150;
                position.Y = yValue;
                SKBitmap bitmap = SKBitmap.Decode(stream);
                for (int i = 0; i < 5; i++)
                {

                    InvisiblebitmapCollection.Add(new TouchManipulationBitmap(bitmap)
                    {
                        Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                    });
                    position.X += xValue;
                }
            }
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
        }

        void CarregaLicoes()
        {
            dicLicoes.Add(0, new LicaoDitado($"Audio.Ditado.Licao1.mp3", new List<int> { 1, 2, 3, 5 }));
            dicLicoes.Add(1, new LicaoDitado($"Audio.Ditado.Licao2.mp3", new List<int> { 1, 2, 3, 4, 5 }));
            dicLicoes.Add(2, new LicaoDitado($"Audio.Ditado.Licao3.mp3", new List<int> { 2, 3, 5 }));
            dicLicoes.Add(3, new LicaoDitado($"Audio.Ditado.Licao4.mp3", new List<int> { 2, 3, 5 }));
            dicLicoes.Add(4, new LicaoDitado($"Audio.Ditado.Licao5.mp3", new List<int> { 1, 3, 5 }));
            dicLicoes.Add(5, new LicaoDitado($"Audio.Ditado.Licao6.mp3", new List<int> { 1, 5 }));
            dicLicoes.Add(6, new LicaoDitado($"Audio.Ditado.Licao7.mp3", new List<int> { 3, 5 }));
            dicLicoes.Add(7, new LicaoDitado($"Audio.Ditado.Licao8.mp3", new List<int> { 5 }));
            dicLicoes.Add(8, new LicaoDitado($"Audio.Ditado.Licao9.mp3", new List<int> { 1, 2, 3, 5 }));
            dicLicoes.Add(9, new LicaoDitado($"Audio.Ditado.Licao10.mp3", new List<int> { 1, 2, 5 }));
            dicLicoes.Add(10, new LicaoDitado($"Audio.Ditado.Licao11.mp3", new List<int>()));

            for (int i = 0; i < 4; i++)
            {
                int indice = RandomNumber(0, dicLicoes.Count);
                if (!licoesAExecutar.Contains(dicLicoes[indice]))
                {
                    licoesAExecutar.Add(dicLicoes[indice]);
                }
                else
                    i--;

            }
        }

        private void AtualizaProgresso()
        {
            if (!this.validado)
            {
                if (ValidaResposta())
                {
                    progressLesson.Progress = (0.25 * (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] + 1));
                    lblResultado.Text = "Correto!!!";
                    stcResult.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    Singleton.Instance.Zeracontagemconquistas();
                    lblResultado.Text = "Incorreto...";
                    stcResult.BackgroundColor = Color.PaleVioletRed;
                }
                btnButton5.Text = "Coninuar";
            }
            else
            {
                Singleton.Instance.IncrementaProgresso(numeroFase);
                carregaFase();
                CarregaBarras();
            }

        }

        void carregaFase()
        {
            if (Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase] < 4)
            {
                player.Load(GetStreamFromFile(licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].Audio));
                this.validado = false;
                stcResult.BackgroundColor = Color.White;
                btnButton5.Text = "Verificar";
            }
            else
            {
                Singleton.Instance.dadosJogador.adcionaNovaFase(this.numeroFase);
                Singleton.Instance.Save();
                //LevelPage pagina = new LevelPage();
                //Navigation.PushModalAsync(pagina);
                Navigation.PopModalAsync();
            }

        }

        private bool ValidaResposta()
        {
            this.validado = true;
            return ValidaDitado();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            // Convert Xamarin.Forms point to pixels
            Point pt = args.Location;
            SKPoint point =
                new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                            (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    for (int i = bitmapCollection.Count - 1; i >= 0; i--)
                    {
                        TouchManipulationBitmap bitmap = bitmapCollection[i];

                        if (bitmap.HitTest(point))
                        {
                            // Move bitmap to end of collection
                            bitmapCollection.Remove(bitmap);
                            bitmapCollection.Add(bitmap);

                            // Do the touch processing
                            bitmapDictionary.Add(args.Id, bitmap);
                            bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                            canvasView.InvalidateSurface();
                            break;
                        }
                    }
                    break;

                case TouchActionType.Moved:
                    if (bitmapDictionary.ContainsKey(args.Id))
                    {
                        TouchManipulationBitmap bitmap = bitmapDictionary[args.Id];
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                case TouchActionType.Cancelled:
                    if (bitmapDictionary.ContainsKey(args.Id))
                    {
                        TouchManipulationBitmap bitmap = bitmapDictionary[args.Id];
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);

                        foreach (var item in InvisiblebitmapCollection)
                        {
                            if (new Point(bitmap.Matrix.TransX, bitmap.Matrix.TransY).Distance(new Point(item.Matrix.TransX, item.Matrix.TransY)) < 10)
                            {
                                InvisiblebitmapCollection.Remove(item);
                                VisiblebitmapCollection.Add(item);
                                break;
                            }
                        }

                        bitmapCollection.Remove(bitmap);

                        CriaBarraHorizontal();

                        bitmapDictionary.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        private void CriaBarraHorizontal()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            //Barra horizontal
            SKPoint position = new SKPoint();
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraHorizontal))
            {
                position.X = 350;
                position.Y = 179;
                SKBitmap bitmap3 = SKBitmap.Decode(stream);
                bitmapCollection.Add(new TouchManipulationBitmap(bitmap3)
                {
                    Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                });
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            canvas = args.Surface.Canvas;

            canvas.Clear();

            // Display the bitmap
            //bitmap.Paint(canvas);
            foreach (TouchManipulationBitmap bitmap in bitmapCollection)
            {

                bitmap.Paint(canvas);
            }

            // Display the bitmap
            //bitmap.Paint(canvas);
            foreach (TouchManipulationBitmap bitmap in VerticalbitmapCollection)
            {
                bitmap.Paint(canvas);
            }


            // Display the bitmap
            //bitmap.Paint(canvas);
            foreach (TouchManipulationBitmap bitmap in VisiblebitmapCollection)
            {
                bitmap.Paint(canvas);
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            player.Play();
        }

        private void btnButton5_Clicked(object sender, EventArgs e)
        {
            AtualizaProgresso();
        }

        private bool ValidaDitado()
        {
            //Verifica se a quantidade de itens está correta
            if (VisiblebitmapCollection.Count == licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].valoresRespostas.Count)
            {
                //Verifica todas as posições estão corretas
                foreach (var item in VisiblebitmapCollection)
                {
                    if(!licoesAExecutar[Singleton.Instance.dadosJogador.ProgressoFase[this.numeroFase]].valoresRespostas.Contains((int)item.Matrix.TransX / xValue))
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            //LevelPage pagina = new LevelPage();
            //Navigation.PushModalAsync(pagina);
        }
    }

    class LicaoDitado
    {
        public string Audio { get; set; }
        public List<int> valoresRespostas { get; set; }

        public LicaoDitado(string Audio, List<int> valoresRespostas)
        {
            this.Audio = Audio;
            this.valoresRespostas = valoresRespostas;
        }
    }
}