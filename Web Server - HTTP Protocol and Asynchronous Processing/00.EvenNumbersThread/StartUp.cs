using System;
using System.Linq;
using System.Threading;

namespace _00.EvenNumbersThread
{
    public class StartUp
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var printNumbers = new Thread(() =>
            {
                for (int i = numbers[0]; i <= numbers[1]; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine(i);
                    }
                }
            });

            printNumbers.Start();
            printNumbers.Join();

            Console.WriteLine("Thread finished work");
        }
    }
}
