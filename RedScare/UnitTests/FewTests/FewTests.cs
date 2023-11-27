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
        var expected = 9;
        Graph graph = GraphParser.ParseGraph("ski-level20-2.txt", true);
        var actual = Few.FewestNumberOfRed(graph);
        Assert.Equal(expected, actual);
    }
}

