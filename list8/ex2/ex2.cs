namespace ex2
{
    class Distribution
    {
        private int[][] data;

        public Distribution(uint size)
        {
            Random random = new Random();
            data = new int[size][];
            for (int i = 0; i < size; i++)
            {
                data[i] = new int[random.Next(1, 10)];
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j] = random.Next(0, (int)size);
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                result += string.Join(" ", data[i]) + "\n";
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Distribution graph = new Distribution(10);
            Console.WriteLine(graph);
        }
    }
}