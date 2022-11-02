using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            CreateCircle(new CircleArea() { X = 100, Y = 200, Radius = 100 });

        }
        public int koko = 0;

        private void Maalaus(object sender, MouseButtonEventArgs e)
        {

        }
        private void CreateLine(double x1, double y1, double x2, double y2)
        {
            // Create a Line  
            Line redLine = new Line();

            // Create a red Brush  
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color  
            redLine.StrokeThickness = 4;
            redLine.Stroke = redBrush;

            // Add line to the Grid.  
            kanvas.Children.Add(redLine);
        }

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
                kanvas.EditingMode = InkCanvasEditingMode.Ink;
                kanvas.DefaultDrawingAttributes.IsHighlighter = true;


            }
            if (brush.SelectedItem == Grafiikka)
            {
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
                kanvas.DefaultDrawingAttributes.FitToCurve = true;
                // kanvas.DefaultDrawingAttributes.IsHighlighter = Opacity=3.1;
            }
            if (brush.SelectedItem == Select)
            {
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
        

        }
    }
}

