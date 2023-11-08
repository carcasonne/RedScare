using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.NoneTests;

public class NoneTests
{
    [Theory]
    [InlineData("common-1-20.txt", -1)]
    [InlineData("ski-illustration.txt", 8)]
    public void Graph_FileName_Finds_Correct_Path_Length(string filename, int expected)
    {
        var graph = GraphParser.ParseGraph(filename, true);
        var actual = None.ShortestPathWithoutReds(graph);
        Assert.Equal(expected, actual);
    }
}