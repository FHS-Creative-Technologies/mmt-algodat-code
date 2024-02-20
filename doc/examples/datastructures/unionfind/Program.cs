using FHS.CT.AlgoDat.DataStructures;

List<int> numbers = new(new int[] { 1, 2, 4, 5, 7, 9, 12 });

UnionFind<int> sets = new();
foreach (int number in numbers)
{
    sets.MakeSet(number);
}

Console.WriteLine("Initial sets");
PrintSets(sets);

sets.Union(1, 2);
sets.Union(4, 5);

Console.WriteLine("Sets after unions");
PrintSets(sets);

sets.Union(4, 1);
sets.Union(7, 12);

Console.WriteLine("Sets after more unions");
PrintSets(sets);

static void PrintSets(UnionFind<int> sets)
{
    foreach (var set in sets)
    {
        Console.Write("Set = { ");

        foreach (var element in set)
        {
            Console.Write($"{element.Data}; ");
        }

        Console.WriteLine("}");
    }
}