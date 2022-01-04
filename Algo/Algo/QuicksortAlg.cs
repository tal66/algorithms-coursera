using System;

namespace Algo
{
    // Quicksort with different pivots
    public class QuicksortAlg
    {
        public static int Quicksort(int[] arr)
        {
            int comparisonsCount = 0;
            Quicksort(arr, 0, arr.Length, ref comparisonsCount);
            return comparisonsCount;
        }

        public static void Quicksort(int[] arr, int i, int j, ref int count)
        {
            if (j - i <= 1) return;

            /// choosing + placing pivot at 1st position            
            //int pivotIdx = i;   
            //int pivotIdx = j-1; 
            int pivotIdx = MedianOfThree(arr, i , j-1);   
            Swap(arr, i, pivotIdx);     

            int p = Partition(arr, i, j);
            Quicksort(arr, i, p, ref count);
            Quicksort(arr, p + 1, j, ref count);
            
            count += j - i - 1;
        }

        static int Partition(int[] arr, int i, int j)
        {
            int left = i + 1;
            for (int k = left; k < j; k++)
            {
                if (arr[k] <= arr[i])
                {
                    Swap(arr, left, k);
                    left++;
                }
            }

            Swap(arr, left - 1, i);
            return left - 1;
        }


        static void Swap(int[] arr, int i, int j)
        {
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }


        public static int MedianOfThree(int[] arr, int i, int j)
        {
            int pivotIdx;

            int m = i + (j - i) / 2;
            int maxij = arr[i] > arr[j] ? i : j;
            int maxmj = arr[m] > arr[j] ? m : j;
            if (arr[m] >= arr[maxij])
            {
                pivotIdx = maxij;
            }
            else if (arr[i] >= arr[maxmj])
            {
                pivotIdx = maxmj;
            }
            else
            {
                pivotIdx = arr[m] > arr[i] ? m : i;
            }

            return pivotIdx;
        }
    }
}
