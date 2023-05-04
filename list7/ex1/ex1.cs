using System;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number to print multiplication table: ");
            uint n = uint.Parse(Console.ReadLine());
            int maxWidth = (n * n).ToString().Length;
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= n; ++j)
                {
                    Console.Write("{0," + maxWidth + "} ", i * j);
                }
                Console.WriteLine();
            }
        }
    }
}