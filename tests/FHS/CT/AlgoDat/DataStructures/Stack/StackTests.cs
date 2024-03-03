namespace Stack;

using FHS.CT.AlgoDat.DataStructures;

public class StackTests
{
    [Fact]
    public void TestStack()
    {
        var stack = new Stack<int>();
        var items = new List<int>
        {
            1, 2, 3, 5, 7
        };
        foreach (var item in items)
        {
            stack.Push(item);
        }
        Assert.Equal(items.Count, stack.Count);

        var orderedList = new List<int>();
        while (stack.Count > 0)
        {
            orderedList.Add(stack.Pop());
        }

        items.Reverse(); // LIFO
        Assert.Equal(items, orderedList);
    }
}