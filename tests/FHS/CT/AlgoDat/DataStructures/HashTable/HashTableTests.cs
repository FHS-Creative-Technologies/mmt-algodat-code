namespace HashTable;

using FHS.CT.AlgoDat.DataStructures;

public class HashTableTests
{
    [Fact]
    public void TestInsertInt()
    {
        var ht = new HashTable<int>();
        for (var i = 1; i <= 20; i++)
        {
            ht.Add(i);
        }


        Assert.Equal(20, ht.Count);
        for (var i = 1; i <= 20; i++)
        {
            Assert.True(ht.Contains(i), $"{i} should be in hash table");
            Assert.Equal(i, ht.Search(i));
        }
    }

    [Fact]
    public void TestInsertChar()
    {
        var ht = new HashTable<char>();
        var items = new char[] { 'h', 'a', 'l', 'o', 'w', 'e', 't' };
        foreach (var item in items)
        {
            ht.Add(item);
        }

        Assert.Equal(items.Length, ht.Count);

        foreach (var item in items)
        {
            Assert.True(ht.Contains(item), $"{item} should be in hash table");
        }
    }

    [Fact]
    public void TestDeleteInt()
    {
        var ht = new HashTable<int>();
        for (var i = 1; i <= 20; i++)
        {
            ht.Add(i);
        }
        ht.Delete(10);
        ht.Delete(2);

        Assert.Equal(18, ht.Count);
        ht.Delete(21); // this is not in list
        Assert.Equal(18, ht.Count);

        Assert.False(ht.Contains(10), "10 should not be in hash table");
        Assert.False(ht.Contains(2), "2 should not be in hash table");
    }

    [Fact]
    public void TestEnumerateInt()
    {
        var ht = new HashTable<int>();
        var items = new List<int>();
        for (var i = 1; i <= 20; i++)
        {
            ht.Add(i);
            items.Add(i);
        }

        var actualItems = new List<int>();
        foreach (var item in ht)
        {
            Assert.True(items.Contains(item), $"{item} should be in list of known items");
            actualItems.Add(item);
        }
        actualItems.Sort();
        Assert.Equal(actualItems, items);
    }
}