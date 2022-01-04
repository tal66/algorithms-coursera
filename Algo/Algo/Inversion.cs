using System;

namespace Algo
{
    public class Inversion
    {
        public static long Mergesort(int[] arr, int i, int j)
        {
            long count = 0;
            int[] aux = new int[arr.Length];
            Mergesort(arr, aux, i, j, ref count);
            return count;
        }

        static void Mergesort(int[] arr, int[] aux, int i, int j, ref long count)
        {
            if (j - i <= 1)
            {
                return;
            }

            int m = i + (j - i) / 2;
            Mergesort(arr, aux, i, m, ref count);
            Mergesort(arr, aux, m, j, ref count);
            Merge(arr, aux, i, m, j, ref count);
        }

        static void Merge(int[] arr, int[] aux, int a, int m, int b, ref long count)
        {
            for (int k = a; k < b; k++)
            {
                aux[k] = arr[k];
            }

            int i = a;
            int j = m;

            int idx = a;
            int next;
            while (i < m || j < b)
            {
                if (i >= m)
                {
                    next = aux[j++];
                }
                else if (j >= b)
                {
                    next = aux[i++];
                }
                else if (aux[i] <= aux[j])
                {
                    next = aux[i++];
                }
                else
                {
                    next = aux[j++];
                    count += m - i;
                }

                arr[idx] = next;
                idx++;
            }

            return;
        }
    }
}
