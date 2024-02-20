// use here the fully qualified name since .NET provides its own Dictionary
FHS.CT.AlgoDat.Datastructures.Dictionary<string, int> dictionary = new();

dictionary.Add("Andreas", 35);
dictionary.Add("Helen", 28);

dictionary.Add("Maria", 2);
dictionary.Add("Franz", 17);

dictionary.Add("Josef", 19);
dictionary.Add("Abraham", 5);

dictionary.Add("Adele", 3);

Console.WriteLine($"Andreas is {dictionary.Get("Andreas")} years old");
Console.WriteLine($"Maria is {dictionary.Get("Maria")} years old");
Console.WriteLine($"Adele is {dictionary.Get("Adele")} years old");

dictionary.Remove("Franz");

foreach (FHS.CT.AlgoDat.Datastructures.Dictionary<string, int>.KeyValuePair kvp in dictionary)
{
    Console.WriteLine($"Key {kvp.Key}, Value {kvp.Value}");
}

Console.WriteLine($"Franz is {dictionary.Get("Franz")} years old"); // get default value