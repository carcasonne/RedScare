using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.AlternateTests;

public class AlternateTests
{
    [Fact]
    public void Graph_Without_Edges_Finds_No_Path()
    {
        var expected = false;

        var graph = GraphParser.ParseGraph("common-1-20.txt");
        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Graph_P3_0_Finds_Path()
    {
        var expected = true;

        var graph = GraphParser.ParseGraph("P3.txt");
        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Graph_P3_1_Finds_No_Path()
    {
        var expected = false;

        var graph = GraphParser.ParseGraph("P3.txt");
        graph.Reds = new List<Vertex>()
        {
            graph.Vertices[graph.Target]
        };

        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Graph_Grid_5_0_Finds_Path()
    {
        var expected = true;

        var graph = GraphParser.ParseGraph("grid-5-0.txt");
        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }
}

