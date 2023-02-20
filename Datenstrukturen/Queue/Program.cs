using AlgoDat;

using System;

Queue<int> q = new();

q.Enqueue(5);
q.Enqueue(3);
q.Enqueue(7);
q.Enqueue(1);

for (int i = 0; i < 4; i++)
{
    Console.WriteLine($"{q.Dequeue()}");
}