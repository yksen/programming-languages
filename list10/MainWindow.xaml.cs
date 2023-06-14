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

// Napisz program, który będzie wyświetlał graf prosty o maksymalnie 10 wierzchołkach. Wierzchołki
// grafu to koła, które posiadają numery od 1 do 10, są ustalonej wielkości i mają zadany kolor, który
// można zmienić, klikając prawym klawiszem myszy nad kołem. Wierzchołki połączone są
// krawędziami (grubymi liniami), początkowo graf jest cykliczny. Przyciskając odpowiednie przyciski
// można dodać i usunąć krawędź między wybranymi widocznymi wierzchołkami. Dowolny
// wierzchołek (wraz ze wszystkimi krawędziami z niego wychodzącymi) można ukryć, klikając nad
// nim prawym klawiszem myszy. Ukryty wierzchołek można wyświetlić używając odpowiedniego
// przycisku. Klikając lewym klawiszem myszy nad wierzchołkiem i przesuwając mysz trzymając
// wciśnięty klawisz, wierzchołek ten można przemieścić do miejsca, w którym klawisz myszy zostanie
// puszczony. Każda krawędź grafu łącząca dwa wierzchołki o zadanych kolorach powinny mieć kolor
// przechodzący z jednego w drugi w sposób ciągły. Miło byłoby, gdyby na pasku tytułu okna aplikacji
// wyświetlana była unikatowa ikonka programu oraz jego nazwa.

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
            DrawGraph();
        }

        private void DrawGraph()
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
                DrawVertex(i + 1, x, y);
            }
        }

        private void DrawVertex(uint number, double x, double y, uint size = 50, Brush? color = null)
        {
            if (color == null)
            {
                color = Brushes.Turquoise;
            }
            Grid grid = new()
            {
                Width = 50,
                Height = 50,
            };
            Ellipse ellipse = new()
            {
                Fill = color,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
            };
            Label label = new()
            {
                FontSize = 20,
                Content = number,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            ellipse.MouseDown += Canvas_MouseLeftButtonDown;
            ellipse.MouseUp += Canvas_MouseLeftButtonUp;
            ellipse.MouseRightButtonDown += Canvas_MouseRightButtonDown;
            ellipse.MouseRightButtonUp += Canvas_MouseRightButtonUp;
            grid.Children.Add(ellipse);
            grid.Children.Add(label);
            Canvas.SetLeft(grid, x - grid.Width / 2);
            Canvas.SetTop(grid, y - grid.Height / 2);
            canvas.Children.Add(grid);
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
