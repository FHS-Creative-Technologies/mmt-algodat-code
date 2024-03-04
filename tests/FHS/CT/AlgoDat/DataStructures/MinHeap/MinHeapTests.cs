using FHS.CT.AlgoDat.DataStructures;

namespace MinHeap;

public class MinHeapTests
{
    [Fact]
    public void TestOrder()
    {
        var numbers = new List<int>();
        var r = new Random();
        var minHeap = new MinHeap<int>();

        for (var i = 0; i < 200; i++)
        {
            var number = r.Next();

            numbers.Add(number);
            minHeap.Insert(number);
        }

        numbers.Sort();
        var resultNumbers = new List<int>();
        while (minHeap.Count > 0)
        {
            resultNumbers.Add(minHeap.ExtractMin());
        }
        Assert.Equal(numbers, resultNumbers);
    }
}
