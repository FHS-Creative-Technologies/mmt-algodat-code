// provide fully qualified name here since .NET has its own Queue
FHS.CT.AlgoDat.DataStructures.Queue<int> q = new();

q.Enqueue(5);
q.Enqueue(3);
q.Enqueue(7);
q.Enqueue(1);

while (q.Count > 0)
{
    Console.WriteLine($"{q.Dequeue()}");
}