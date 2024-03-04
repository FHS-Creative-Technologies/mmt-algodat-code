namespace PriorityQueue;

public class PriorityQueueTests
{
    [Fact]
    public void TestOrder()
    {
        var numbers = new List<int>();
        var r = new Random();
        var priorityQueue = new FHS.CT.AlgoDat.DataStructures.PriorityQueue<int, int>();

        for (var i = 0; i < 200; i++)
        {
            var number = r.Next();

            numbers.Add(number);
            priorityQueue.Enqueue(number, number);
        }

        numbers.Sort();
        var resultNumbers = new List<int>();
        while (priorityQueue.Count > 0)
        {
            resultNumbers.Add(priorityQueue.Dequeue());
        }
        Assert.Equal(numbers, resultNumbers);
    }

    [Fact]
    public void TestDecreaseKey()
    {
        var priorityQueue = new FHS.CT.AlgoDat.DataStructures.PriorityQueue<string, int>();
        priorityQueue.Enqueue("Andreas", 100);
        priorityQueue.Enqueue("Maria", 90);
        priorityQueue.Enqueue("Sophie", 80);
        priorityQueue.Enqueue("Marcel", 70);

        priorityQueue.DecreasePriorityValue("Andreas", 60);
        Assert.Equal("Andreas", priorityQueue.Dequeue());
        Assert.Equal("Marcel", priorityQueue.Dequeue());
        Assert.Equal("Sophie", priorityQueue.Dequeue());
        Assert.Equal("Maria", priorityQueue.Dequeue());
    }
}
