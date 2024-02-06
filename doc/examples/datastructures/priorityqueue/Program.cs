// use fully qualified name here since .NET provides its own PriorityQueue
FHS.CT.AlgoDat.PriorityQueue<string, int> pq = new();

pq.Enqueue("Andreas", 2);
pq.Enqueue("Brigitte", 3);
pq.Enqueue("Hilmar", 1);
pq.Enqueue("Doris", 0);
pq.Enqueue("Jonas", 4);

pq.DecreasePriorityValue("Hilmar", -1);
pq.DecreasePriorityValue("Jonas", 1);
pq.DecreasePriorityValue("Andreas", -2);

while (pq.Count > 0)
{
    Console.WriteLine($"Dequeue {pq.Dequeue()}");
}