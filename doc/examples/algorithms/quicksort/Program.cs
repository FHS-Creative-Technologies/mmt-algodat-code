using FHS.CT.AlgoDat;

int[] numbers = { 7, 2, 109, 19, 28, 1, 10, 4 };
foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}
Console.WriteLine();
QuickSort<int>.Sort(numbers);
foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}
Console.WriteLine();