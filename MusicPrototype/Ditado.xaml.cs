using System;
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using TouchTracking;

using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms.Xaml;
using System.Reflection;
using System.IO;

namespace MusicPrototype
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ditado : ContentPage
    {
        TouchManipulationBitmap bitmap;
        List<long> touchIds = new List<long>();

        public Ditado()
        {
            InitializeComponent();

            string resourceID = "MusicPrototype.Images.BarraHorizontal.jpg";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);
                this.bitmap = new TouchManipulationBitmap(bitmap);
                this.bitmap.TouchManager.Mode = TouchManipulationMode.ScaleRotate;
            }
        }

        void OnTouchModePickerSelectedIndexChanged(object sender, EventArgs args)
        {
            if (bitmap != null)
            {
                Picker picker = (Picker)sender;
                bitmap.TouchManager.Mode = (TouchManipulationMode)picker.SelectedItem;
            }
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
                    if (bitmap.HitTest(point))
                    {
                        touchIds.Add(args.Id);
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        break;
                    }
                    break;

                case TouchActionType.Moved:
                    if (touchIds.Contains(args.Id))
                    {
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                case TouchActionType.Cancelled:
                    if (touchIds.Contains(args.Id))
                    {
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        touchIds.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // Display the bitmap
            bitmap.Paint(canvas);

        }
    }
}