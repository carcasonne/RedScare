using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.NoneTests;

public class NoneTests
{
    [Fact]
    public void Graph_Without_Path_Finds_No_Path()
    {
        var expected = 0;

        var graph = GraphParser.ParseGraph("commoon-1-20.txt");
        var actual = graph.BreadthFirstSearchAvoidingRed();

        Assert.Equal(expected, actual);
    }
}