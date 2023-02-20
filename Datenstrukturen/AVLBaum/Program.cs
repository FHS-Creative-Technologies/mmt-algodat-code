using AlgoDat;
using AVLTree;

using System;
using System.Collections.Generic;

TestInsert();
TestDelete();

static void TestDelete()
{
    AvlTree<int> tree = new();
    List<int> insertedNumbers = new();
    Random r = new();

    while (insertedNumbers.Count <= 500)
    {
        var number = r.Next(20000);
        tree.Insert(number);
        insertedNumbers.Add(number);
    }

    AvlTreeChecker<int>.Check(tree);

    int counter = 0;
    while (counter <= 40)
    {
        var number = insertedNumbers[r.Next(insertedNumbers.Count - 1)];
        var node = tree.Search(number);

        tree.Delete(node);
        insertedNumbers.Remove(number);
        AvlTreeChecker<int>.Check(tree);

        counter++;
    }
}

static void TestInsert()
{
    AvlTree<int> tree = new();

    Random r = new();

    int counter = 0;
    while (counter <= 1000)
    {
        var number = r.Next();
        tree.Insert(number);

        counter++;
    }

    AvlTreeChecker<int>.Check(tree);
}