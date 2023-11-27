using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using RedScare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GraphFactory;

namespace UnitTests.ManyTests;
public class ManyTests
{
    [Fact]
    public void fdhjdfhjdsjfdsh()
    {
        var graph = GraphParser.ParseGraph("common-1-2500.txt");
        var actual = None.ShortestPathWithoutReds(graph);
        Assert.Equal(6, actual);
    }

    [Theory]
    [InlineData("ski-illustration.txt", 1)]
    [InlineData("ski-level3-1.txt", 3)]
    [InlineData("ski-level3-3.txt", 1)]
    [InlineData("ski-level10-1.txt", -1)]
    [InlineData("ski-level20-3.txt", -1)]
    public void Many_Skilevel_Returns_Expected(string filename, int expected)
    {
        var directedGraph = GraphParser.ParseGraph(filename);
        var actual = Many.HowManyReds(directedGraph);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("ski-illustration.txt")]
    [InlineData("ski-level3-1.txt")]
    [InlineData("ski-level3-2.txt")]
    [InlineData("ski-level3-3.txt")]
    [InlineData("ski-level5-1.txt")]
    [InlineData("ski-level5-2.txt")]
    [InlineData("ski-level5-3.txt")]
    [InlineData("ski-level10-2.txt")]
    [InlineData("ski-level10-3.txt")]
    [InlineData("ski-level20-1.txt")]
    [InlineData("ski-level20-2.txt")]
    public void Many_Skilevel_Without_Reds_Finds_0_Path(string filename)
    {
        var expected = 0;
        var directedGraph = GraphParser.ParseGraph(filename);

        // Remove all vertices from Red set
        directedGraph.Reds = new List<Utilities.Graphs.Vertex>();
        foreach(var v in directedGraph.Vertices)
            v.IsRed = false;

        var actual = Many.HowManyReds(directedGraph);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("ski-illustration.txt", 9)]
    [InlineData("ski-level3-3.txt", 6)]
    public void Many_Skilevel_Every_Node_Red_Finds_Correct_Max(string filename, int expected)
    {
        var directedGraph = GraphParser.ParseGraph(filename);

        directedGraph.Reds = directedGraph.Vertices;
        foreach (var v in directedGraph.Vertices)
            v.IsRed = true;

        var actual = Many.HowManyReds(directedGraph);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("ski-illustration.txt")]
    [InlineData("ski-level3-1.txt")]
    [InlineData("ski-level3-2.txt")]
    [InlineData("ski-level3-3.txt")]
    [InlineData("ski-level5-1.txt")]
    [InlineData("ski-level5-2.txt")]
    [InlineData("ski-level5-3.txt")]
    // [InlineData("ski-level10-1.txt")] // Start node not connected to anything
    [InlineData("ski-level10-2.txt")]
    [InlineData("ski-level10-3.txt")]
    [InlineData("ski-level20-1.txt")]
    [InlineData("ski-level20-2.txt")]
    // [InlineData("ski-level20-3.txt")] // Start node connected to 'island', no connection to end node
    public void Many_Cyclical_Graph_Returns_Nothing(string filename)
    {
        var expected = -999;
        var directedGraph = GraphParser.ParseGraph(filename);

        // The idea is to make an edge target --> sink
        // Due to the structure of the ski level, this means that the graph becomes cyclical
        directedGraph.AddEdge(directedGraph.Target, directedGraph.Source);

        var actual = Many.HowManyReds(directedGraph);
        Assert.Equal(expected, actual);
    }
}