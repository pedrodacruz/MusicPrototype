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
        ISimpleAudioPlayer player;


        string resourceBarraHorizontal = "MusicPrototype.Images.BarraHorizontal.jpg";
        string resourceBarraVertical = "MusicPrototype.Images.BarraVertical.jpg";

        SKCanvas canvas;

        List<TouchManipulationBitmap> bitmapCollection =
           new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> VerticalbitmapCollection =
   new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> InvisiblebitmapCollection =
            new List<TouchManipulationBitmap>();

        List<TouchManipulationBitmap> VisiblebitmapCollection =
    new List<TouchManipulationBitmap>();

        Dictionary<long, TouchManipulationBitmap> bitmapDictionary =
            new Dictionary<long, TouchManipulationBitmap>();

        public Ditado()
        {

            player = CrossSimpleAudioPlayer.Current;

            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            player.Loop = false;

            player.Load(GetStreamFromFile($"Audio.Licao1.wav"));

            InitializeComponent();

            Assembly assembly = GetType().GetTypeInfo().Assembly;
            // Load in all the available bitmaps
            SKPoint position = new SKPoint();

            //Barras Verticais
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraVertical))
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);

                position.X = 70;
                position.Y = 50;

                for (int i = 0; i < 6; i++)
                {

                    VerticalbitmapCollection.Add(new TouchManipulationBitmap(bitmap)
                    {
                        Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                    });
                    position.X += 70;
                }
            }

            //Barra horizontal
            using (Stream stream = assembly.GetManifestResourceStream(resourceBarraHorizontal))
            {
                position.X = 200;
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
                position.X = 70;
                position.Y = 50;
                SKBitmap bitmap = SKBitmap.Decode(stream);
                for (int i = 0; i < 5; i++)
                {

                    InvisiblebitmapCollection.Add(new TouchManipulationBitmap(bitmap)
                    {
                        Matrix = SKMatrix.MakeTranslation(position.X, position.Y),
                    });
                    position.X += 70;
                }
            }
        }
        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MusicPrototype." + filename);

            return stream;
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
                position.X = 200;
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
            if(ValidaDitado())
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

        private bool ValidaDitado()
        {
            //Verifica se a quantidade de itens está correta
            if (VisiblebitmapCollection.Count == 4)
            {
                //Verifica todas as posições estão corretas
                foreach (var item in VisiblebitmapCollection)
                {
                    foreach (var item2 in InvisiblebitmapCollection)
                    {
                        if (item.Matrix.TransX == item2.Matrix.TransX)
                        {
                            return false;
                        }
                    }
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
            LevelPage pagina = new LevelPage();
            Navigation.PushModalAsync(pagina);
        }
    }
}