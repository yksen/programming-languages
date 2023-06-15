using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphApp
{
    public partial class MainWindow : Window
    {
        private const int MaxVertices = 10;
        private List<UIElement> vertices = new List<UIElement>();
        private List<Line> edges = new List<Line>();
        private Ellipse? selectedVertex = null;
        private bool isDragging = false;
        private Point offset;

        public MainWindow()
        {
            InitializeComponent();
            canvas.Loaded += Canvas_Loaded;
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateVertices();
            GenerateEdges();
        }

        private void GenerateVertices(double radius = 150, uint size = 50)
        {
            double alpha = 2 * Math.PI / MaxVertices;

            for (int i = 0; i < MaxVertices; i++)
            {
                Ellipse vertex = new Ellipse
                {
                    Width = size,
                    Height = size,
                    Fill = Brushes.Turquoise,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Tag = i + 1
                };

                vertex.MouseRightButtonUp += Vertex_MouseRightButtonUp;
                vertex.MouseLeftButtonDown += Vertex_MouseLeftButtonDown;
                vertex.MouseLeftButtonUp += Vertex_MouseLeftButtonUp;

                double x = radius * Math.Cos(alpha * i) + canvas.ActualWidth / 2;
                double y = radius * Math.Sin(alpha * i) + canvas.ActualHeight / 2;

                Canvas.SetLeft(vertex, x - vertex.Width / 2);
                Canvas.SetTop(vertex, y - vertex.Height / 2);

                canvas.Children.Add(vertex);
                vertices.Add(vertex);
            }
        }

        private void GenerateEdges()
        {
            for (int i = 0; i < MaxVertices; i++)
            {
                int nextIndex = (i + 1) % MaxVertices;
                Ellipse currentVertex = (Ellipse)vertices[i];
                Ellipse nextVertex = (Ellipse)vertices[nextIndex];

                double currentLeft = Canvas.GetLeft(currentVertex);
                double currentTop = Canvas.GetTop(currentVertex);
                double nextLeft = Canvas.GetLeft(nextVertex);
                double nextTop = Canvas.GetTop(nextVertex);

                Line edge = new Line
                {
                    X1 = currentLeft + currentVertex.Width / 2,
                    Y1 = currentTop + currentVertex.Height / 2,
                    X2 = nextLeft + nextVertex.Width / 2,
                    Y2 = nextTop + nextVertex.Height / 2,
                    StrokeThickness = 5,
                    Stroke = GenerateGradientBrush(currentVertex.Fill, nextVertex.Fill)
                };

                canvas.Children.Insert(0, edge);
                edges.Add(edge);
            }
        }

        private LinearGradientBrush GenerateGradientBrush(Brush startColor, Brush endColor)
        {
            GradientStopCollection gradientStops = new GradientStopCollection
            {
                new GradientStop(((SolidColorBrush)startColor).Color, 0),
                new GradientStop(((SolidColorBrush)endColor).Color, 1)
            };

            LinearGradientBrush gradientBrush = new LinearGradientBrush(gradientStops)
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };

            return gradientBrush;
        }

        private void Vertex_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse vertex = (Ellipse)sender;
            vertex.Fill = GenerateRandomColor();
            UpdateEdges(vertex);
        }

        private SolidColorBrush GenerateRandomColor()
        {
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]));
            return brush;
        }

        private void Vertex_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isDragging)
            {
                selectedVertex = (Ellipse)sender;
                offset = e.GetPosition(selectedVertex);
                selectedVertex.CaptureMouse();
                isDragging = true;
            }
        }

        private void Vertex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && selectedVertex != null)
            {
                selectedVertex.ReleaseMouseCapture();
                isDragging = false;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && selectedVertex != null)
            {
                Point currentPosition = e.GetPosition(canvas);
                double newX = currentPosition.X - offset.X;
                double newY = currentPosition.Y - offset.Y;

                Canvas.SetLeft(selectedVertex, newX);
                Canvas.SetTop(selectedVertex, newY);

                UpdateEdges(selectedVertex);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && selectedVertex != null)
            {
                selectedVertex.ReleaseMouseCapture();
                isDragging = false;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedVertex != null)
            {
                Point currentPosition = e.GetPosition(canvas);
                double newX = currentPosition.X - offset.X;
                double newY = currentPosition.Y - offset.Y;

                Canvas.SetLeft(selectedVertex, newX);
                Canvas.SetTop(selectedVertex, newY);

                UpdateEdges(selectedVertex);
            }
        }

        private void UpdateEdges(Ellipse vertex)
        {
            int vertexIndex = vertices.IndexOf(vertex);
            int nextIndex = (vertexIndex + 1) % MaxVertices;
            int previousIndex = (vertexIndex + MaxVertices - 1) % MaxVertices;

            Ellipse nextVertex = (Ellipse)vertices[nextIndex];
            Ellipse previousVertex = (Ellipse)vertices[previousIndex];

            Line outgoingEdge = edges[vertexIndex];
            Line incomingEdge = edges[previousIndex];

            outgoingEdge.X1 = Canvas.GetLeft(vertex) + vertex.Width / 2;
            outgoingEdge.Y1 = Canvas.GetTop(vertex) + vertex.Height / 2;
            outgoingEdge.X2 = Canvas.GetLeft(nextVertex) + nextVertex.Width / 2;
            outgoingEdge.Y2 = Canvas.GetTop(nextVertex) + nextVertex.Height / 2;

            incomingEdge.X2 = Canvas.GetLeft(vertex) + vertex.Width / 2;
            incomingEdge.Y2 = Canvas.GetTop(vertex) + vertex.Height / 2;

            LinearGradientBrush brush = GenerateGradientBrush(previousVertex.Fill, vertex.Fill);
            outgoingEdge.Stroke = brush;
            incomingEdge.Stroke = brush;
        }

        private void HideVertexButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex != null)
            {
                selectedVertex.Opacity = 0.2;
                HideConnectedEdges(selectedVertex);
            }
        }

        private void ShowVertexButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex != null && selectedVertex.Opacity < 1)
            {
                selectedVertex.Opacity = 1;
                ShowConnectedEdges(selectedVertex);
            }
        }

        private void HideConnectedEdges(Ellipse vertex)
        {
            int vertexIndex = vertices.IndexOf(vertex);
            int nextIndex = (vertexIndex + 1) % MaxVertices;
            int previousIndex = (vertexIndex - 1 + MaxVertices) % MaxVertices;

            Line outgoingEdge = edges[vertexIndex];
            Line incomingEdge = edges[previousIndex];

            outgoingEdge.Visibility = Visibility.Hidden;
            incomingEdge.Visibility = Visibility.Hidden;
        }

        private void ShowConnectedEdges(Ellipse vertex)
        {
            int vertexIndex = vertices.IndexOf(vertex);
            int nextIndex = (vertexIndex + 1) % MaxVertices;
            int previousIndex = (vertexIndex + MaxVertices - 1) % MaxVertices;

            Line outgoingEdge = edges[vertexIndex];
            Line incomingEdge = edges[previousIndex];

            outgoingEdge.Visibility = Visibility.Visible;
            incomingEdge.Visibility = Visibility.Visible;
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex != null)
            {
                int vertexIndex = vertices.IndexOf(selectedVertex);
                int nextIndex = (vertexIndex + 1) % MaxVertices;
                Ellipse nextVertex = (Ellipse)vertices[nextIndex];

                Line newEdge = new Line
                {
                    X1 = Canvas.GetLeft(selectedVertex) + selectedVertex.Width / 2,
                    Y1 = Canvas.GetTop(selectedVertex) + selectedVertex.Height / 2,
                    X2 = Canvas.GetLeft(nextVertex) + nextVertex.Width / 2,
                    Y2 = Canvas.GetTop(nextVertex) + nextVertex.Height / 2,
                    StrokeThickness = 5,
                    Stroke = GenerateGradientBrush(selectedVertex.Fill, nextVertex.Fill)
                };

                edges.Insert(vertexIndex, newEdge);
                canvas.Children.Insert(0, newEdge);

                UpdateEdgeIndices();
            }
        }

        private void RemoveEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex != null)
            {
                int vertexIndex = vertices.IndexOf(selectedVertex);
                int previousIndex = (vertexIndex + MaxVertices - 1) % MaxVertices;

                Line outgoingEdge = edges[vertexIndex];
                Line incomingEdge = edges[previousIndex];

                canvas.Children.Remove(outgoingEdge);
                canvas.Children.Remove(incomingEdge);
                edges.Remove(outgoingEdge);
                edges.Remove(incomingEdge);

                UpdateEdgeIndices();
            }
        }

        private void UpdateEdgeIndices()
        {
            for (int i = 0; i < edges.Count; i++)
            {
                Line edge = edges[i];
                Ellipse vertex = (Ellipse)vertices[i];
                int nextIndex = (i + 1) % MaxVertices;
                Ellipse nextVertex = (Ellipse)vertices[nextIndex];

                edge.Stroke = GenerateGradientBrush(vertex.Fill, nextVertex.Fill);
            }
        }
    }
}
