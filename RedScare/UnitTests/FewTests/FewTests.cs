using System;
using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.FewTests;

public class FewTests
{
	[Fact]
	public void G_ex_finds_path_size_0()
	{
        var expected = 0;
        Graph graph = GraphParser.ParseGraph("ski-level20-2.txt");
        var actual = Few.FewestNumberOfRed(graph);
        Assert.Equal(expected, actual);
    }
}

