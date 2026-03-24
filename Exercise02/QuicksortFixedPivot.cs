using System.Diagnostics;

namespace Exercise02
{
    public class QuicksortFixedPivot : ISortAlgorithm
    {
        public string Name => "Quicksort (Fixed Pivot)";
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
            Quicksort(array, leftIndex, pivotIndex);
            Quicksort(array, pivotIndex + 1, rightIndex);
        }

        private int Partition(int[] array, int leftIndex, int rightIndex)
        {
            int pivot = array[(leftIndex + rightIndex) / 2];
            int left = leftIndex - 1;
            int right = rightIndex + 1;

            while (true)
            {
                do 
                { 
                    left++; 
                    Steps++; 
                } 
                while (array[left] < pivot);

                do 
                { 
                    right--; 
                    Steps++; 
                } 
                while (array[right] > pivot);

                if (left >= right)
                {
                    return right;
                }

                (array[left], array[right]) = (array[right], array[left]);
            }
        }
    }
}
