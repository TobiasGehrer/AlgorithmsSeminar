using System.Diagnostics;

namespace Exercise02
{
    public class MergeSort : ISortAlgorithm
    {
        public string Name => "MergeSort";
        public int Steps { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public void Sort(int[] array)
        {
            Steps = 0;
            var sw = Stopwatch.StartNew();
            MergeSortRecursive(array, 0, array.Length - 1);
            sw.Stop();
            ElapsedTime = sw.Elapsed;
        }

        private void MergeSortRecursive(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex >= rightIndex)
            {
                return;
            }

            int middleIndex = (leftIndex + rightIndex) / 2;

            // Split into two halves
            MergeSortRecursive(array, leftIndex, middleIndex);
            MergeSortRecursive(array, middleIndex + 1, rightIndex);

            // Merge the sorted halves
            Merge(array, leftIndex, middleIndex, rightIndex);
        }

        private void Merge(int[] array, int leftIndex, int middleIndex, int rightIndex)
        {
            // Copy both halves into temporary arrays
            int[] left = array[leftIndex..(middleIndex + 1)];
            int[] right = array[(middleIndex + 1)..(rightIndex + 1)];

            int i = 0, j = 0, k = leftIndex;

            // Merge back in sorted order
            while (i < left.Length && j < right.Length)
            {
                Steps++;

                if (left[i] <= right[j])
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            // Copy any remaining elements
            while (i < left.Length) 
            { 
                array[k++] = left[i++]; Steps++; 
            }
            
            while (j < right.Length) 
            { 
                array[k++] = right[j++]; Steps++; 
            }
        }
    }
}
