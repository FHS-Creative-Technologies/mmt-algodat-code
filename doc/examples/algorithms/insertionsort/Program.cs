using FHS.CT.AlgoDat;

int[] numbers = { 10, 6, 2, 9, 3, 7, 1 };

foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}
Console.WriteLine();

InsertionSort<int>.Sort(numbers);

foreach (int number in numbers)
{
    Console.Write($"{number}, ");
}
Console.WriteLine();
