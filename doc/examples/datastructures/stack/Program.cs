﻿// provides fully qualified name since .NET has its own Stack
FHS.CT.AlgoDat.DataStructures.Stack<int> stack = new();

stack.Push(3);
stack.Push(4);
stack.Push(5);
stack.Push(6);

while (stack.Count > 0)
{
    Console.WriteLine(stack.Pop());
}
