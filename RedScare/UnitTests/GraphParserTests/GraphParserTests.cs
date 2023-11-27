using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GraphFactory;

namespace UnitTests.GraphParserTest;
public class GraphParserTests
{
    [Theory]
    [InlineData("common-1-20.txt", 20, 0, 9)]
    [InlineData("common-2-5757.txt", 5757, 163671, 2457)]
    [InlineData("gnm-5000-10000-1.txt", 5000, 10000, 2500)]
    [InlineData("ski-illustration.txt", 36, 49, 1)]
    public void ParseGraph_Directed_Graph_Properties_Hold(string file, int expectedNodes, int expectedEdges, int expectedReds)
    {
        var graph = GraphParser.ParseGraph(file);

        var actualNodes = graph.V;
        var actualEdges = graph.E;
        var actualReds = graph.Reds.Count;

        Assert.Equal(expectedNodes, actualNodes);
        Assert.Equal(expectedEdges, actualEdges);
        Assert.Equal(expectedReds, actualReds);
    }

    // No vertex should have a neighbor
    [Fact]
    public void ParseGraph_NoEdges_NoNeighbors()
    {
        var expectedNeighbors = 0;

        var graph = GraphParser.ParseGraph("common-1-20.txt");
        var actualNeighbors = graph.Vertices.Sum(x => x.Edges.Count);

        Assert.Equal(expectedNeighbors, actualNeighbors);
    }
}
