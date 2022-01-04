using Xunit;
using static Algo.KaratsubaAlg;
using static Algo.Inversion;
using static Algo.QuicksortAlg;
using System.Collections.Generic;

namespace AlgoTest
{
    public class AlgoTests
    {
        [Theory]
        [MemberData(nameof(SortData))]
        public void TestMSort(int[] arr, int[] expected)
        {
            Mergesort(arr, 0, arr.Length);
            Assert.Equal(expected, arr);
        }

        [Theory]
        [MemberData(nameof(SortData))]
        public void TestQSort(int[] arr, int[] expected)
        {
            Quicksort(arr);
            Assert.Equal(expected, arr);
        }

        public static IEnumerable<object[]> SortData()
        {
            yield return new object[] { new int[]{ 2,5,3,1,4 }, new int[] { 1,2,3,4,5 } };
            yield return new object[] { new int[] { 4,3,2,1 }, new int[] { 1,2,3,4 } };
            yield return new object[] { new int[] { 3,2,1 }, new int[] { 1,2,3 } };
            yield return new object[] { new int[] { 1,3,2 }, new int[] { 1,2,3 } };
            yield return new object[] { new int[] { 1,2,3 }, new int[] { 1,2,3 } };
        }

        [Theory]
        [MemberData(nameof(MultiplyData))]
        public void TestMultiply(string n1, string n2, string expected)
        {
            Assert.Equal(expected, Karatsuba(n1, n2).ToString());
        }

        public static IEnumerable<object[]> MultiplyData()
        {
            yield return new object[] { "1234", "5678", "7006652" };
            yield return new object[] { "20", "20", "400" };
            yield return new object[] { "75686581564765274121732379327282478238", 
                "26851616999964243213276256236165236", 
                "2032307100213631526661908677734434552542815550883869539671304639742134168" };
        }

    }
}