using AlgoDat;

DoubleLinkedList<int> myList = new();

DoubleLinkedList<int>.Node<int> node2 = myList.Append(2);
DoubleLinkedList<int>.Node<int> node3 = myList.Append(3);
DoubleLinkedList<int>.Node<int> node1 = myList.Append(1);
DoubleLinkedList<int>.Node<int> node4 = myList.Append(4);

Console.WriteLine($"foreach list: items {myList.Count}");
foreach (int num in myList)
{
    Console.WriteLine($"Item: {num}");
}

var search = myList.Search(1);

Console.WriteLine("Delete 1 and 4");
myList.Delete(node1);
myList.Delete(node4);

Console.WriteLine($"foreach list: items {myList.Count}");
foreach (int num in myList)
{
    Console.WriteLine($"Item: {num}");
}

Console.WriteLine("Delete 3 and 2");
myList.Delete(node3);
myList.Delete(node2);

Console.WriteLine($"foreach list: items {myList.Count}");
foreach (int num in myList)
{
    Console.WriteLine($"Item: {num}");
}