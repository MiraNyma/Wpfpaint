using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace paintti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           

        }
        public int koko = 0;


        
        
        private void brushkoko_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brushkoko.SelectedItem == Suuri)
            {
                kanvas.DefaultDrawingAttributes.Width = 30;
                kanvas.DefaultDrawingAttributes.Height = 30;

            }
            if (brushkoko.SelectedItem == Keskisuuri)
            {
                kanvas.DefaultDrawingAttributes.Width = 20;
                kanvas.DefaultDrawingAttributes.Height = 20;
            }
            if (brushkoko.SelectedItem == Pieni)
            {
                kanvas.DefaultDrawingAttributes.Width = 10;
                kanvas.DefaultDrawingAttributes.Height = 10;
            }
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            kanvas.DefaultDrawingAttributes.Color = Colors.Red;
        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            kanvas.DefaultDrawingAttributes.Color = Colors.Blue;
        }

        private void yellow_Click(object sender, RoutedEventArgs e)
        {
            kanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
        }

        private void black_Click(object sender, RoutedEventArgs e)
        {
            kanvas.DefaultDrawingAttributes.Color = Colors.Black;
        }

        private void white_Click(object sender, RoutedEventArgs e)
        {
            kanvas.DefaultDrawingAttributes.Color = Colors.White;
        }

        private void brush_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (brush.SelectedItem == Nel)
            {
                kanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Rectangle;
            }

            if (brush.SelectedItem == Erase)
            {
                kanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            if (brush.SelectedItem == Erase_all)
            {
                kanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
            if (brush.SelectedItem == Highlighter)
            {
                kanvas.DefaultDrawingAttributes.FitToCurve = false;
                kanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                kanvas.EditingMode = InkCanvasEditingMode.Ink;
                kanvas.DefaultDrawingAttributes.IsHighlighter = true;


            }
            if (brush.SelectedItem == Grafiikka)
            {
                kanvas.DefaultDrawingAttributes.FitToCurve = false;
                kanvas.DefaultDrawingAttributes.IsHighlighter = false;
                kanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                kanvas.EditingMode = InkCanvasEditingMode.Ink;
                if (brushkoko.SelectedItem == Suuri)
                {
                    kanvas.DefaultDrawingAttributes.Width = 30;
                    kanvas.DefaultDrawingAttributes.Height = 1;

                }
                if (brushkoko.SelectedItem == Keskisuuri)
                {
                    kanvas.DefaultDrawingAttributes.Width = 20;
                    kanvas.DefaultDrawingAttributes.Height = 1;
                }
                if (brushkoko.SelectedItem == Pieni)
                {
                    kanvas.DefaultDrawingAttributes.Width = 10;
                    kanvas.DefaultDrawingAttributes.Height = 1;
                }

            }
            if (brush.SelectedItem == Tussi)
            {
                kanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                kanvas.EditingMode = InkCanvasEditingMode.Ink;
                kanvas.DefaultDrawingAttributes.FitToCurve = false;
                kanvas.DefaultDrawingAttributes.IsHighlighter = false;
                if (brushkoko.SelectedItem == Suuri)
                {
                    kanvas.DefaultDrawingAttributes.Width = 30;
                    kanvas.DefaultDrawingAttributes.Height = 30;

                }
                if (brushkoko.SelectedItem == Keskisuuri)
                {
                    kanvas.DefaultDrawingAttributes.Width = 20;
                    kanvas.DefaultDrawingAttributes.Height = 20;
                }
                if (brushkoko.SelectedItem == Pieni)
                {
                    kanvas.DefaultDrawingAttributes.Width = 10;
                    kanvas.DefaultDrawingAttributes.Height = 10;
                }
            }
            if (brush.SelectedItem == Sivellin)
            {
                kanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
                kanvas.DefaultDrawingAttributes.FitToCurve = true;
                kanvas.DefaultDrawingAttributes.IsHighlighter = false;
                // kanvas.DefaultDrawingAttributes.IsHighlighter = Opacity=3.1;
            }
            if (brush.SelectedItem == Select)
            {
                kanvas.DefaultDrawingAttributes.FitToCurve = false;
                kanvas.DefaultDrawingAttributes.IsHighlighter = false;
                kanvas.EditingMode = InkCanvasEditingMode.Select;

                // kanvas.DefaultDrawingAttributes.IsHighlighter = Opacity=3.1;
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)kanvas.RenderSize.Width,
    (int)kanvas.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(kanvas);

            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, 497, 249));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));
            using (var fs = System.IO.File.OpenWrite($"{Nimi.Text}.png"))
            {
                pngEncoder.Save(fs);
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            kanvas.Children.Clear();
            kanvas.Strokes.Clear();
            Nimi.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage img = new BitmapImage(fileUri);

                Image image = new Image
                {



                    Source = img
                };
                kanvas.Children.Add(image);

            }
        }

        private void Koko(object sender, RoutedEventArgs e)
        {
            kanvas.Width =double.Parse( Nimi.Text);
            kanvas.Width = double.Parse(Korkeus.Text);
        }

        private void Flipped(object sender, RoutedEventArgs e)
        {
            StrokeCollection copiedStrokes = kanvas.Strokes.Clone();
            Matrix rotatingMatrix = new Matrix();
            double canvasLeft = Canvas.GetLeft(kanvas);
            double canvasTop = Canvas.GetTop(kanvas);
            Point rotatePoint = new Point(kanvas.Width / 2, kanvas.Height / 2);

            rotatingMatrix.RotateAt(90, rotatePoint.X, rotatePoint.Y);
            copiedStrokes.Transform(rotatingMatrix, false);
            kanvas.Strokes = copiedStrokes;
        }
        private class CircleArea
        {
            public int X;
            public int Y;
            public int Radius;
        }

        private void CreateCircle(CircleArea circle)
        {
            Ellipse c = new Ellipse()
            {
                Width = circle.Radius * 2,
                Height = circle.Radius * 2,
                Stroke = Brushes.Red,
                StrokeThickness = 6
            };

            kanvas.Children.Add(c);

            c.SetValue(Canvas.LeftProperty, (double)circle.X);
            c.SetValue(Canvas.TopProperty, (double)circle.Y);
        }
        private void CreateLine(double x1, double y1, double x2, double y2)
        {
            // Create a Line  
            Line redLine = new Line();
            redLine.X1 = x1;
            redLine.Y1 = y1;
            redLine.X2 = x2;
            redLine.Y2 = y2;

            // Create a red Brush  
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color  
            redLine.StrokeThickness = 4;
            redLine.Stroke = redBrush;

            // Add line to the Grid.  
            kanvas.Children.Add(redLine);
        }
        public double x;
        public double y;
        public void Kordit(object sender, MouseButtonEventArgs e)
        {
             x = e.GetPosition(kanvas).X;
             y = e.GetPosition(kanvas).Y;
        }

        private void Ympyra_click(object sender, RoutedEventArgs e)
        {
            CreateCircle(new CircleArea() { X= int.Parse(x.ToString()), Y = int.Parse( y.ToString()), Radius = 50 });
        }
    }
}

