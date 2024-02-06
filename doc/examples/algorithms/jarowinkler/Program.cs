using FHS.CT.AlgoDat;

if (args.Length != 2)
{
    Console.WriteLine("Usage: dotnet run <string1> <string2>");
}

string s1 = args[0];
string s2 = args[1];
Console.WriteLine($"s_j({s1}, {s2}) = {JaroWinkler.JaroSimilarity(s1, s2)}");
Console.WriteLine($"s_w({s1}, {s2}) = {JaroWinkler.JaroWinklerSimilarity(s1, s2)}");