using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace list10
{
    // <summary>
    // Interaction logic for MainWindow.xaml
    // </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            drawGraph();
        }

        private void drawGraph()
        {
            uint n = 10;
            double r = 100;
            double x0 = 200;
            double y0 = 200;
            double alpha = 2 * Math.PI / n;
            double x, y;

            for (uint i = 0; i < n; i++)
            {
                x = x0 + r * Math.Cos(i * alpha);
                y = y0 + r * Math.Sin(i * alpha);
                drawVertex(i + 1, x, y);
            }
        }

        private void drawVertex(uint number, double x, double y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = Brushes.Red;
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 2;
            ellipse.MouseDown += Canvas_MouseLeftButtonDown;
            ellipse.MouseUp += Canvas_MouseLeftButtonUp;
            ellipse.MouseRightButtonDown += Canvas_MouseRightButtonDown;
            ellipse.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            canvas.Children.Add(ellipse);
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveEdgeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HideVertexButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowVertexButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
