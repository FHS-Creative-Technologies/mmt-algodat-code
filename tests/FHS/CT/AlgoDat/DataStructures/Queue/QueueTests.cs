namespace Queue;

using FHS.CT.AlgoDat.DataStructures;

public class QueueTests
{
    [Fact]
    public void TestQueue()
    {
        var queue = new Queue<int>();
        var items = new List<int>
        {
            1, 2, 3, 5, 7
        };
        foreach (var item in items)
        {
            queue.Enqueue(item);
        }
        Assert.Equal(items.Count, queue.Count);

        var orderedList = new List<int>();
        while (queue.Count > 0)
        {
            orderedList.Add(queue.Dequeue());
        }
        Assert.Equal(items, orderedList);
    }
}