namespace EvenNumberThread
{
    using System;
    using System.Linq;
    using System.Threading;

    public class Program
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n))
                .ToArray();
            int start = numbers[0];
            int end = numbers[1];

            Thread evens = new Thread(() => PrintEvenNumbers(start, end));
            evens.Start();
            evens.Join();

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            if (start % 2 != 0)
            {
                start++;
            }
            for (int i = start; i <= end; i = i + 2)
            {
                Console.WriteLine(i);
            }
        }
    }
}
