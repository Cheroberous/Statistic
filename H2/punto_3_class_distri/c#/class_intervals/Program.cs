// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



using System;

class Program
{
    // Function to generate uniform random variates in [0, 1)
    static double[] GenerateRandomVariates(int N)
    {
        Random random = new Random();
        double[] variates = new double[N];
        for (int i = 0; i < N; i++)
        {
            variates[i] = random.NextDouble();
        }
        return variates;
    }

    // Function to determine the distribution into class intervals
    static int[] DetermineDistribution(double[] variates, int k)
    {
        double intervalSize = 1.0 / k;
        int[] distribution = new int[k];

        foreach (double variate in variates)
        {
            for (int i = 0; i < k; i++)
            {
                if (variate >= i * intervalSize && variate < (i + 1) * intervalSize)
                {
                    distribution[i]++;
                    break;
                }
            }
        }

        return distribution;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Random Variate Distribution");
        Console.Write("Enter the number of random variates to generate: ");
        if (!int.TryParse(Console.ReadLine(), out int N) || N <= 0)
        {
            Console.WriteLine("Please enter a valid number of variates.");
            return;
        }

        Console.Write("Enter the number of intervals (k): ");
        if (!int.TryParse(Console.ReadLine(), out int k) || k <= 0)
        {
            Console.WriteLine("Please enter a valid number of intervals (k).");
            return;
        }

        double[] variates = GenerateRandomVariates(N);
        int[] distribution = DetermineDistribution(variates, k);

        Console.WriteLine("Distribution into class intervals:");
        for (int i = 0; i < k; i++)
        {
            double intervalStart = i * (1.0 / k);
            double intervalEnd = (i + 1) * (1.0 / k);
            Console.WriteLine($"Interval [{intervalStart:F2}, {intervalEnd:F2}): {distribution[i]}");
        }
    }
}

