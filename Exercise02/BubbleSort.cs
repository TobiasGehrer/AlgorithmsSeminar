using System.Diagnostics;

namespace Exercise02
{
    public class BubbleSort : ISortAlgorithm
    {
        public string Name => "BubbleSort";
        public int Steps { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public void Sort(int[] array)
        {
            Steps = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    Steps++;

                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            sw.Stop();
            ElapsedTime = sw.Elapsed;
        }
    }
}
