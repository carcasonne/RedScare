using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.NoneTests;

public class NoneTests
{
    [Fact]
    public void Graph_Without_Path_Finds_No_Path()
    {
        var expected = -1;

        var graph = GraphParser.ParseGraph("common-1-20.txt");
        var actual = graph.BreadthFirstSearchAvoidingRed();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Graph_Ski_Illustration_Finds_Path_Size_8()
    {
        var expected = 8;

        var graph = GraphParser.ParseGraph("ski-illustration.txt");
        var actual = graph.BreadthFirstSearchAvoidingRed();

        Assert.Equal(expected, actual);
    }
}