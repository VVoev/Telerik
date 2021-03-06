﻿namespace ConsoleApplication1
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {

        private static int n;

        private static int[,] matrix;
        private static int[] used;

        // If we track the path we will need these arrays
        private static int[] minCycle;
        private static int[] cycle;

        private static long curSum;
        private static long minSum;

        private static void Main(string[] args)
        {
            var input = @"5
1 2 5
1 3 4
1 5 10
2 4 7
4 3 2
3 5 1
end";

            StringReader reader = new StringReader(input);

            Console.SetIn(reader);

            // Solution
            ProcessInput();

            minSum = long.MaxValue;
            curSum = 0;

            Hamiltonian(0, 0, 0);

            if (minSum < long.MaxValue)
            {
                Console.WriteLine(minSum);
                //Print(0);
            }
            else
            {
                Console.WriteLine(0);
                //Print(0);
            }
        }

        private static void ProcessInput()
        {
            n = int.Parse(Console.ReadLine());

            used = new int[n];

            // If we track the path we will need these arrays
            cycle = new int[n];
            minCycle = new int[n];

            matrix = new int[n, n];

            var line = Console.ReadLine();
            while (line != "end")
            {
                var data = line.Split(' ').Select(int.Parse).ToArray();

                // The graph is unoriented so we assign the values two way
                matrix[data[0] - 1, data[1] - 1] = data[2];
                matrix[data[1] - 1, data[0] - 1] = data[2];

                line = Console.ReadLine();
            }
        }

        // If we want to have a starting player as an input we must truck it with start. Otherwise we don't need it.
        private static void Hamiltonian(int start, int i, int level)
        {
            if ((start == i) && (level > 0))
            {
                if (level == n)
                {
                    minSum = curSum;

                    // If we track the path we will need this array
                    //Array.Copy(cycle, minCycle, n);
                    return;
                }
            }

            if (used[i] != 0)
            {
                return;
            }

            used[i] = 1;

            for (int k = 0; k < n; k++)
            {
                if (matrix[i, k] != 0 && k != i)
                {
                    // If we track the path we will need this array
                    //cycle[level] = k;
                    curSum += matrix[i, k];
                    var temp = matrix[i, k];

                    if (curSum < minSum)
                    {
                        Hamiltonian(start, k, level + 1);
                    }

                    curSum -= matrix[i, k];
                }
            }

            used[i] = 0;
        }

        // Printing the path
        static void Print(int start)
        {
            int i;
            Console.Write(start + 1);
            for (i = 0; i < n; i++)
            {
                Console.Write(" {0}", minCycle[i] + 1);
            }
            Console.WriteLine();

        }

        // Adjecent matrix printing for check
        private static void PrintAdjacentMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

