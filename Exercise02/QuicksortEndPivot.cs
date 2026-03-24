using System.Diagnostics;

namespace Exercise02
{
    public class QuicksortEndPivot : ISortAlgorithm
    {
        public string Name => "Quicksort (End Pivot)";

        public int Steps { get; private set; }

        public TimeSpan ElapsedTime {  get; private set; }

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
            // Base case: 0 or 1 elements are already sorted
            if (leftIndex >= rightIndex)
            {
                return;
            }

            // Place pivot at its correct position and get its index
            int pivotIndex = Partition(array, leftIndex, rightIndex);

            // Recursively sort elements before and after the pivot
            Quicksort(array, leftIndex, pivotIndex - 1);
            Quicksort(array, pivotIndex + 1, rightIndex);
        }

        private int Partition(int[] array, int leftIndex, int rightIndex)
        {
            int pivot = array[rightIndex]; // Use last element as pivot
            int lastSmallIndex = leftIndex - 1; // Points to the last element known to be <= pivot

            for (int currentIndex = leftIndex; currentIndex < rightIndex; currentIndex++)
            {
                Steps++;

                // If current element belongs to the left (smaller) side
                if (array[currentIndex] <= pivot)
                {
                    lastSmallIndex++;
                    (array[lastSmallIndex], array[currentIndex]) = (array[currentIndex], array[lastSmallIndex]);
                }
            }

            // Place pivot between the two sides
            (array[lastSmallIndex + 1], array[rightIndex]) = (array[rightIndex], array[lastSmallIndex + 1]);
            return lastSmallIndex + 1; // Return final pivot position
        }
    }
}
