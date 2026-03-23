using System;
using System.Diagnostics;


namespace Exercise01
{
    class Program
    {
        static Random rng = new Random(42);

        // --- Generator ---
        static double[,] Generate(int n)
        {
            var M = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M[i, j] = rng.NextDouble() * 100;
            return M;
        }

        // --- Addition: T(n) = c * n^2  →  O(n^2) ---
        static double[,] Add(double[,] A, double[,] B, int n)
        {
            var C = new double[n, n];
            for (int i = 0; i < n; i++)           // n
                for (int j = 0; j < n; j++)       // n
                    C[i, j] = A[i, j] + B[i, j]; // 1
            return C;
        }

        // --- Multiplication: T(n) = c * n^3  →  O(n^3) ---
        static double[,] Multiply(double[,] A, double[,] B, int n)
        {
            var C = new double[n, n];
            for (int i = 0; i < n; i++)          // n
                for (int j = 0; j < n; j++)      // n
                {
                    double sum = 0;
                    for (int k = 0; k < n; k++)  // n
                        sum += A[i, k] * B[k, j];
                    C[i, j] = sum;
                }
            return C;
        }

        static long Measure(Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Matrizenmultiplikation (O(n^3), parallelisiert) ===");
            Console.WriteLine($"{"N",8} | {"Zeit (ms)",12}");
            Console.WriteLine(new string('-', 25));

            int[] multSizes = { 100, 200, 300, 500, 750, 1000, 1500, 2000 };
            foreach (int n in multSizes)
            {
                var A = Generate(n);
                var B = Generate(n);
                long ms = Measure(() => Multiply(A, B, n));
                Console.WriteLine($"{n,8} | {ms,12} ms");
            }

            Console.WriteLine();
            Console.WriteLine("=== Matrizenaddition (O(n^2)) ===");
            Console.WriteLine($"{"N",8} | {"Zeit (ms)",12}");
            Console.WriteLine(new string('-', 25));

            int[] addSizes = { 500, 1000, 2000, 3000, 5000, 7500, 10000 };
            foreach (int n in addSizes)
            {
                var A = Generate(n);
                var B = Generate(n);
                long ms = Measure(() => Add(A, B, n));
                Console.WriteLine($"{n,8} | {ms,12} ms");
            }

            Console.WriteLine();
            Console.WriteLine("=== Komplexitätsanalyse ===");
            Console.WriteLine("Addition:        T(n) = c·n^2      → O(n^2)");
            Console.WriteLine("Multiplikation:  T(n) = c·n^3      → O(n^3)");
            Console.WriteLine();
            Console.WriteLine("Zeilenbeiträge Multiplikation:");
            Console.WriteLine("  Outer loop (i):         n  Iterationen");
            Console.WriteLine("  Middle loop (j):        n  Iterationen  → n^2");
            Console.WriteLine("  Inner loop (k) + sum:   n  Iterationen  → n^3");
            Console.WriteLine("  C[i,j] = sum:           1  Operation    → n^2");
            Console.WriteLine();
            Console.WriteLine("Konstanten (Beispielwerte):");
            Console.WriteLine("  c1 = 1  (untere Schranke)");
            Console.WriteLine("  c2 = 3  (obere Schranke, da 3 Schleifen + Overhead)");
            Console.WriteLine("  n0 = 1  (gilt für alle n >= 1)");
            Console.WriteLine("  → c1·n³ ≤ T(n) ≤ c2·n³ für alle n ≥ n0  ⟹  Θ(n³)");
        }
    }
}