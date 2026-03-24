using System.Diagnostics;

namespace Exercise02
{
    public class QuicksortRandomPivot : ISortAlgorithm
    {
        public string Name => "Quicksort (Random Pivot)";
        public int Steps { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public void Sort(int[] array)
        {
            Steps = 0;
            var stopwatch = Stopwatch.StartNew();
            Quicksort(array, 0, array.Length - 1);
            stopwatch.Stop();
            ElapsedTime = stopwatch.Elapsed;
        }

        private void Quicksort(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex >= rightIndex)
            {
                return;
            }

            int pivotIndex = Partition(array, leftIndex, rightIndex);
            Quicksort(array, leftIndex, pivotIndex - 1);
            Quicksort(array, pivotIndex + 1, rightIndex);
        }

        private int Partition(int[] array, int leftIndex, int rightIndex)
        {
            // Swap random element to end, then partition like fixed
            int randomIndex = Random.Shared.Next(leftIndex, rightIndex + 1);
            (array[randomIndex], array[rightIndex]) = (array[rightIndex], array[randomIndex]);

            int pivot = array[rightIndex];
            int lastSmallIndex = leftIndex - 1;

            for (int currentIndex = leftIndex; currentIndex < rightIndex; currentIndex++)
            {
                Steps++;

                if (array[currentIndex] <= pivot)
                {
                    lastSmallIndex++;
                    (array[lastSmallIndex], array[currentIndex]) = (array[currentIndex], array[lastSmallIndex]);
                }
            }

            (array[lastSmallIndex + 1], array[rightIndex]) = (array[rightIndex], array[lastSmallIndex + 1]);
            return lastSmallIndex + 1;
        }
    }
}
