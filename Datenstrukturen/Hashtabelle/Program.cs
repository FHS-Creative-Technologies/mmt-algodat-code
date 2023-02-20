using AlgoDat;

using System;
using System.Diagnostics;

TestHashTableEnumerator();

HashTable<int> table = new();
Random r = new();

Stopwatch w = new();

w.Start();
for (int i = 0; i < 2000000; i++)
{
    table.Add(r.Next());
}
w.Stop();
Console.WriteLine($"Adding to table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 200000; i++)
{
    table.Search(r.Next());
}
w.Stop();
Console.WriteLine($"Searching table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 2000000; i++)
{
    table.Add(r.Next());
}
w.Stop();
Console.WriteLine($"Adding more to table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 200000; i++)
{
    table.Search(r.Next());
}
w.Stop();
Console.WriteLine($"Searching table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 2000000; i++)
{
    table.Add(r.Next());
}
w.Stop();
Console.WriteLine($"Adding more to table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 200000; i++)
{
    table.Search(r.Next());
}
w.Stop();
Console.WriteLine($"Searching table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 2000000; i++)
{
    table.Add(r.Next());
}
w.Stop();
Console.WriteLine($"Adding more to table took {w.ElapsedMilliseconds} ms");

w.Restart();
for (int i = 0; i < 200000; i++)
{
    table.Search(r.Next());
}
w.Stop();
Console.WriteLine($"Searching table took {w.ElapsedMilliseconds} ms");

static void TestHashTableEnumerator()
{
    HashTable<int> hashTable = new(4);

    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Adding {i * 4}");
        hashTable.Add(i * 4);
    }

    Console.WriteLine("Hash Table Enumerator");
    foreach (int item in hashTable)
    {
        Console.WriteLine($"Item {item}");
    }
}
