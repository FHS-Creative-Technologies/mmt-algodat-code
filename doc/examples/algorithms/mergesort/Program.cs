using FHS.CT.AlgoDat.Algorithms;

int[] numbers = { 9, 2, 1, 3, 7, 12, 5, 4, 21, 6 };
foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}
Console.WriteLine();
MergeSort<int>.Sort(numbers);
foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}