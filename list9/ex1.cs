namespace ex1
{
    public class Observer
    {
        public string Name { get; }
        public double X { get; }
        public double Y { get; }
        private Observer[] neighbors;

        public Observer(string name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
            neighbors = new Observer[2];
        }

        public void UpdateNeighbors(Observer observer)
        {
            if (neighbors[0] == null)
            {
                neighbors[0] = observer;
            }
            else
            {
                if (neighbors[1] == null)
                {
                    neighbors[1] = observer;
                }
                else
                {
                    double largestDistance = CalculateDistance(neighbors[1]);
                    double newDistance = CalculateDistance(observer);
                    if (newDistance < largestDistance)
                    {
                        neighbors[1] = observer;
                    }
                }
                if (CalculateDistance(neighbors[1]) < CalculateDistance(neighbors[0]))
                {
                    Observer toSwap = neighbors[0];
                    neighbors[0] = neighbors[1];
                    neighbors[1] = toSwap;
                }
            }
        }

        private double CalculateDistance(Observer other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public void PrintNeighbors()
        {
            Console.WriteLine($"Neighbors of {Name}:");
            foreach (Observer neighbor in neighbors)
            {
                if (neighbor != null)
                {
                    Console.WriteLine($"{neighbor.Name} ({neighbor.X,0:F3}, {neighbor.Y,0:F3}) Distance: {CalculateDistance(neighbor),0:F3}");
                }
            }
        }
    }

    public class Creator
    {
        private Random random;
        private uint id;

        public delegate void CreateObserverHandler(Observer observer);
        public CreateObserverHandler? OnCreateObserver;

        public delegate void PrintNeighborsHandler();
        public PrintNeighborsHandler? OnPrintNeighbors;

        public Creator()
        {
            random = new Random();
            id = 0;
        }

        public void CreateObserver()
        {
            string name = $"Observer {id++}";
            double x = random.NextDouble();
            double y = random.NextDouble();
            Observer observer = new Observer(name, x, y);
            OnCreateObserver?.Invoke(observer);
            OnCreateObserver += observer.UpdateNeighbors;
            OnPrintNeighbors += observer.PrintNeighbors;
        }

        public void PrintNeighbors()
        {
            OnPrintNeighbors?.Invoke();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Creator creator = new Creator();
            for (int i = 0; i < 5; i++)
            {
                creator.CreateObserver();
                creator.PrintNeighbors();
                Console.WriteLine();
            }
        }
    }
}