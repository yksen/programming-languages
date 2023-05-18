namespace ex1
{
    class List
    {
        protected int[] data;
        private (int, int) valueRange = (1, 100);
        private (int, int) sizeRange = (1, 5);

        public List(uint size)
        {
            Random random = new Random();
            data = new int[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = random.Next(valueRange.Item1, valueRange.Item2 + 1);
            }
        }

        public List()
        {
            Random random = new Random();
            data = new int[random.Next(sizeRange.Item1, sizeRange.Item2 + 1)];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = random.Next(valueRange.Item1, valueRange.Item2 + 1);
            }
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", data) + "]";
        }
    }

    class List1 : List, IComparable<List1>
    {
        public List1() : base() { }

        public List1(uint size) : base(size) { }

        public int CompareTo(List1? other)
        {
            if (other == null)
            {
                return 1;
            }
            int minLength = Math.Min(data.Length, other.data.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (data[i] < other.data[i])
                {
                    return -1;
                }
                else if (data[i] > other.data[i])
                {
                    return 1;
                }
            }
            return data.Length.CompareTo(other.data.Length);
        }
    }

    class List2 : List, IComparable<List2>
    {
        public List2() : base() { }

        public List2(uint size) : base(size) { }

        public int CompareTo(List2? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (data.Length == other.data.Length)
            {
                int minLength = Math.Min(data.Length, other.data.Length);
                for (int i = 0; i < minLength; i++)
                {
                    if (data[i] < other.data[i])
                    {
                        return -1;
                    }
                    else if (data[i] > other.data[i])
                    {
                        return 1;
                    }
                }
            }
            return data.Length.CompareTo(other.data.Length);
        }
    }

    class Program
    {
        static void sortTest<T>(T[] lists)
        {
            foreach (T list in lists)
            {
                Console.WriteLine(list);
            }
            Array.Sort(lists);
            Console.WriteLine();
            foreach (T list in lists)
            {
                Console.WriteLine(list);
            }
        }

        static void Main(string[] args)
        {
            sortTest(new List1[] { new List1(10), new List1(), new List1(10), new List1() });
            Console.WriteLine();
            sortTest(new List2[] { new List2(10), new List2(10), new List2(), new List2() });
        }
    }
}