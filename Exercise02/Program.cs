namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] original = GenerateRandomArray(10000, seed: 42);

            ISortAlgorithm[] algorithms = [
                new QuicksortEndPivot(),
                new QuicksortRandomPivot(),
                new QuicksortFixedPivot(),
                new MergeSort(),
                new BubbleSort()
            ];

            foreach (var algo in algorithms)
            {
                int[] arr = (int[])original.Clone();
                algo.Sort(arr);
                Console.WriteLine($"{algo.Name}: {algo.Steps} steps | {algo.ElapsedTime.TotalMilliseconds}ms | Correct: {IsSorted(arr)}");
            }
        }

        static int[] GenerateRandomArray(int size, int seed)
        {
            var rng = new Random(seed);
            return Enumerable.Range(0, size).Select(_ => rng.Next(1, 10000)).ToArray();
        }

        static bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}