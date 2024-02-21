namespace Sorting;

using FHS.CT.AlgoDat.Algorithms;

public class SortingTests
{
    [Fact]
    public void TestMergeSort()
    {
        var randomNumbers = GenerateArray(1000);
        var copyOfRandomNumbers = new int[randomNumbers.Length];
        Array.Copy(randomNumbers, copyOfRandomNumbers, randomNumbers.Length);
        MergeSort<int>.Sort(randomNumbers);

        TestSorted(copyOfRandomNumbers, randomNumbers);
    }

    [Fact]
    public void TestQuickSort()
    {
        var randomNumbers = GenerateArray(1000);
        var copyOfRandomNumbers = new int[randomNumbers.Length];
        Array.Copy(randomNumbers, copyOfRandomNumbers, randomNumbers.Length);
        QuickSort<int>.Sort(randomNumbers);

        TestSorted(copyOfRandomNumbers, randomNumbers);
    }

    [Fact]
    public void TestInsertionSort()
    {
        var randomNumbers = GenerateArray(1000);
        var copyOfRandomNumbers = new int[randomNumbers.Length];
        Array.Copy(randomNumbers, copyOfRandomNumbers, randomNumbers.Length);
        InsertionSort<int>.Sort(randomNumbers);

        TestSorted(copyOfRandomNumbers, randomNumbers);
    }

    private static void TestSorted(int[] original, int[] sorted)
    {
        Array.Sort(original);
        Assert.Equal(original, sorted);
    }

    private static int[] GenerateArray(int n)
    {
        var array = new int[n];
        var r = new Random();

        for (var i = 0; i < n; i++)
        {
            array[i] = r.Next();
        }

        return array;
    }
}