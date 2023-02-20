using AlgoDat;

using System;

Stack<int> stack = new();

stack.Push(3);
stack.Push(4);
stack.Push(5);
stack.Push(6);

Console.WriteLine(stack.Pop());
Console.WriteLine(stack.Pop());
Console.WriteLine(stack.Pop());
Console.WriteLine(stack.Pop());