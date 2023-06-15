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
        private List<Ellipse> vertices = new List<Ellipse>();
        private List<List<Edge>> edges = new List<List<Edge>>();
        private Ellipse? selectedVertex = null;
        private Ellipse? targetVertex = null;
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
                edges.Add(new List<Edge>());
            }

            for (int i = 0; i < MaxVertices; i++)
            {
                int previousIndex = (i - 1 + MaxVertices) % MaxVertices;
                int nextIndex = (i + 1) % MaxVertices;

                Ellipse previousVertex = (Ellipse)vertices[previousIndex];
                Ellipse currentVertex = (Ellipse)vertices[i];
                Ellipse nextVertex = (Ellipse)vertices[nextIndex];

                Line line = new Line
                {
                    X1 = Canvas.GetLeft(currentVertex) + currentVertex.Width / 2,
                    Y1 = Canvas.GetTop(currentVertex) + currentVertex.Height / 2,
                    X2 = Canvas.GetLeft(nextVertex) + nextVertex.Width / 2,
                    Y2 = Canvas.GetTop(nextVertex) + nextVertex.Height / 2,
                    StrokeThickness = 5,
                    Stroke = GenerateGradientBrush(currentVertex.Fill, nextVertex.Fill)
                };

                Edge edge = new Edge(currentVertex, nextVertex, line);
                canvas.Children.Insert(0, line);

                edges[i].Add(edge);
                edges[nextIndex].Add(edge);
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
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (targetVertex != null)
                    targetVertex.Stroke = Brushes.Black;
                targetVertex = (Ellipse)sender;
                targetVertex.Stroke = Brushes.Orange;
            }
            else if (!isDragging)
            {
                if (selectedVertex != null)
                    selectedVertex.Stroke = Brushes.Black;
                selectedVertex = (Ellipse)sender;
                selectedVertex.Stroke = Brushes.Red;
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
            if (e.ChangedButton == MouseButton.Left && selectedVertex != null && Keyboard.Modifiers != ModifierKeys.Control)
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
            List<Edge> vertexEdges = edges[vertexIndex];

            foreach (Edge edge in vertexEdges)
            {
                Ellipse startVertex = edge.StartVertex;
                Ellipse endVertex = edge.EndVertex;
                Line line = edge.Line;

                double startX = Canvas.GetLeft(startVertex) + startVertex.Width / 2;
                double startY = Canvas.GetTop(startVertex) + startVertex.Height / 2;
                double endX = Canvas.GetLeft(endVertex) + endVertex.Width / 2;
                double endY = Canvas.GetTop(endVertex) + endVertex.Height / 2;

                line.X1 = startX;
                line.Y1 = startY;
                line.X2 = endX;
                line.Y2 = endY;

                line.Stroke = GenerateGradientBrush(startVertex.Fill, endVertex.Fill);
            }
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
            List<Edge> vertexEdges = edges[vertexIndex];

            foreach (Edge edge in vertexEdges)
            {
                edge.Line.Visibility = Visibility.Hidden;
            }
        }

        private void ShowConnectedEdges(Ellipse vertex)
        {
            int vertexIndex = vertices.IndexOf(vertex);
            List<Edge> vertexEdges = edges[vertexIndex];

            foreach (Edge edge in vertexEdges)
            {
                if (edge.StartVertex.Opacity == 1 && edge.EndVertex.Opacity == 1)
                    edge.Line.Visibility = Visibility.Visible;
            }
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex == null || targetVertex == null || selectedVertex == targetVertex ||
                selectedVertex.Opacity < 1 || targetVertex.Opacity < 1)
                return;
            
            bool doesEdgeExist = false;
            foreach (Edge edge in edges[vertices.IndexOf(selectedVertex)])
            {
                if (edge.StartVertex == selectedVertex && edge.EndVertex == targetVertex ||
                    edge.StartVertex == targetVertex && edge.EndVertex == selectedVertex)
                {
                    doesEdgeExist = true;
                    break;
                }
            }
            if (doesEdgeExist)
                return;

            Line line = new Line
            {
                X1 = Canvas.GetLeft(selectedVertex) + selectedVertex.Width / 2,
                Y1 = Canvas.GetTop(selectedVertex) + selectedVertex.Height / 2,
                X2 = Canvas.GetLeft(targetVertex) + targetVertex.Width / 2,
                Y2 = Canvas.GetTop(targetVertex) + targetVertex.Height / 2,
                StrokeThickness = 5,
                Stroke = GenerateGradientBrush(selectedVertex.Fill, targetVertex.Fill)
            };

            Edge newEdge = new Edge(selectedVertex, targetVertex, line);
            canvas.Children.Insert(0, newEdge.Line);

            edges[vertices.IndexOf(selectedVertex)].Add(newEdge);
            edges[vertices.IndexOf(targetVertex)].Add(newEdge);

            UpdateEdges(selectedVertex);
        }

        private void RemoveEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVertex == null || targetVertex == null || selectedVertex == targetVertex ||
                selectedVertex.Opacity < 1 || targetVertex.Opacity < 1)
                return;

            int selectedVertexIndex = vertices.IndexOf(selectedVertex);
            int targetVertexIndex = vertices.IndexOf(targetVertex);

            List<Edge> selectedVertexEdges = edges[selectedVertexIndex];
            List<Edge> targetVertexEdges = edges[targetVertexIndex];

            Edge? edgeToRemove = null;

            foreach (Edge edge in selectedVertexEdges)
            {
                if (edge.StartVertex == selectedVertex && edge.EndVertex == targetVertex ||
                    edge.StartVertex == targetVertex && edge.EndVertex == selectedVertex)
                {
                    edgeToRemove = edge;
                    break;
                }
            }

            if (edgeToRemove != null)
            {
                canvas.Children.Remove(edgeToRemove.Line);
                selectedVertexEdges.Remove(edgeToRemove);
                targetVertexEdges.Remove(edgeToRemove);
            }
        }
    }

    public class Edge
    {
        public Ellipse StartVertex { get; set; }
        public Ellipse EndVertex { get; set; }
        public Line Line { get; set; }

        public Edge(Ellipse startVertex, Ellipse endVertex, Line line)
        {
            StartVertex = startVertex;
            EndVertex = endVertex;
            Line = line;
        }
    }
}
