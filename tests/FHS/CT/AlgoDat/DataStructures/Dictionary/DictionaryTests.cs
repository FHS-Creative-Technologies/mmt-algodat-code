namespace Dictionary;

using FHS.CT.AlgoDat.DataStructures;

public class DictionaryTests
{
    [Fact]
    public void TestInsert()
    {
        var dict = new Dictionary<string, int>();
        dict.Add("Andreas", 37);
        dict.Add("Maria", 35);
        dict.Add("Samuel", 25);
        dict.Add("Sophie", 27);

        Assert.Equal(37, dict.Get("Andreas"));
        Assert.Equal(35, dict.Get("Maria"));
        Assert.Equal(27, dict.Get("Sophie"));
        Assert.Equal(25, dict.Get("Samuel"));

        Assert.False(dict.Contains("Franz"));
    }

    [Fact]
    public void TestDelete()
    {
        var dict = new Dictionary<string, int>();
        dict.Add("Andreas", 37);
        dict.Add("Maria", 35);
        dict.Add("Samuel", 25);
        dict.Add("Sophie", 27);

        Assert.True(dict.Contains("Maria"));
        dict.Remove("Maria");
        Assert.False(dict.Contains("Maria"));
        Assert.True(dict.Get("Maria") == default);
    }
}
