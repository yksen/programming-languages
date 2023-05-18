namespace ex1
{
    public class ObserverEventArgs : EventArgs
    {
        public string name;
        public double x;
        public double y;

        public ObserverEventArgs(string name, double x, double y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }

    public class Observer
    {
        public string name;
        public double x;
        public double y;
        private List<Observer> neighbors = new List<Observer>();

        public Observer(string name, double x, double y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public void NewObserver(object creator, ObserverEventArgs e)
        {
            Observer observer = new Observer(e.name, e.x, e.y);
            neighbors.Add(observer);
        }

        public void Print(object creator)
        {
            neighbors.Sort((a, b) =>
                Math.Sqrt(Math.Pow(a.x - x, 2) + Math.Pow(a.y - y, 2))
                .CompareTo(Math.Sqrt(Math.Pow(b.x - x, 2) + Math.Pow(b.y - y, 2))));
            Console.WriteLine("Observer: " + name);
            for (int i = 0; i < Math.Min(2, neighbors.Count); i++)
            {
                Console.WriteLine(neighbors[i].name + " x = " + neighbors[i].x + " y = " + neighbors[i].y +
                " distance = " + Math.Sqrt(Math.Pow(neighbors[i].x - x, 2) + Math.Pow(neighbors[i].y - y, 2)));
            }

        }
    }

    public class Creator
    {
        private static Random random = new Random();
        private int id = 0;
        public delegate void NewObserverHandler(object creator, ObserverEventArgs e);
        public delegate void PrintHandler(object creator);

        public NewObserverHandler NewObserver;
        public PrintHandler Print;

        public void CreateObserver()
        {
            Observer observer = new Observer("Observer" + id++, random.NextDouble(), random.NextDouble());
            NewObserver += observer.NewObserver;
        }

        public void PrintObservers()
        {
            Print(this);
        }
    }

    public class Program
    {
        public static void Main()
        {
            Creator creator = new Creator();
            for (int i = 0; i < 5; i++)
            {
                creator.CreateObserver();
                creator.PrintObservers();
            }
        }
    }
}