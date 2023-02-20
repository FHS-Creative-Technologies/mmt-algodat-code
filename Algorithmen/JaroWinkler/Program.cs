using System;

using AlgoDat;

string s1 = args[0];
string s2 = args[1];
Console.WriteLine($"s_j({s1}, {s2}) = {JaroWinkler.JaroSimilarity(s1, s2)}");
Console.WriteLine($"s_w({s1}, {s2}) = {JaroWinkler.JaroWinklerSimilarity(s1, s2)}");