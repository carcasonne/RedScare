using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;
using Utilities.Extensions;

namespace UnitTests.AlternateTests;

public class AlternateTests
{
    [Theory]
    // Examples from the PDF for initial assurance
    [InlineData("G-ex.txt",         false, true)]
    [InlineData("common-1-20.txt",  true,  false)]
    [InlineData("P3.txt",           false, true)]
    // Grids: Diagonal exists with a valid path
    // No directed grids have a valid path
    [InlineData("grid-5-0.txt",     true,  false)]
    [InlineData("grid-10-0.txt",    true,  false)]
    [InlineData("grid-25-0.txt",    true,  false)]
    [InlineData("grid-50-0.txt",    true,  false)]
    [InlineData("grid-5-0.txt",     false, true)]
    [InlineData("grid-10-0.txt",    false, false)] // Grids where N is even do not have a valid path  
    [InlineData("grid-25-0.txt",    false, true)]
    [InlineData("grid-50-0.txt",    false, false)] // Grids where N is even do not have a valid path  
    // Walls: Not too interesting, makes sure no paths are found on dominantly non-red graphs
    [InlineData("wall-n-1.txt",     false, false)]
    [InlineData("wall-n-10.txt",    false, false)]
    [InlineData("wall-n-100.txt",   false, false)]
    [InlineData("wall-n-1000.txt",  false, false)]
    [InlineData("wall-n-10000.txt", false, false)]

    public void Graph_Given_File_Properties_Hold(string filename, bool makeDirected, bool expected)
    {
        var graph = GraphParser.ParseGraph(filename, makeDirected);
        var actual = Alternate.AlternatingPathExists(graph);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("G-ex.txt", false)]
    [InlineData("common-1-20.txt", true)]
    [InlineData("P3.txt", false)]
    [InlineData("grid-5-0.txt", true)]
    [InlineData ("grid-10-0.txt", true)]
    [InlineData("grid-25-0.txt", true)]
    [InlineData("grid-50-0.txt", true)]
    [InlineData("grid-5-0.txt", false)]
    [InlineData("grid-10-0.txt", false)]
    [InlineData("grid-25-0.txt", false)]
    [InlineData("grid-50-0.txt", false)]
    [InlineData("wall-n-1.txt", false)]
    [InlineData("wall-n-10.txt", false)]
    [InlineData("wall-n-100.txt", false)]
    [InlineData("wall-n-1000.txt", false)]
    [InlineData("wall-n-10000.txt", false)]
    public void Graph_Flipping_Reds_And_Blacks_Same_Result(string filename, bool makeDirected)
    {
        var graph = GraphParser.ParseGraph(filename, makeDirected);
        var expected = Alternate.AlternatingPathExists(graph);

        // Flip
        var nonReds = graph.Vertices.Where(x => !graph.Reds.Contains(x))
                                    .ToList();
        graph.Reds = nonReds;
        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    // A modified version of P3, where the Red vertex is moved to make the problem impossible
    [Fact]
    public void Graph_P3_1_Finds_No_Path()
    {
        var expected = false;

        var graph = GraphParser.ParseGraph("P3.txt", false);
        graph.Reds = new List<Vertex>()
        {
            graph.Vertices[graph.Target]
        };

        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }
}

