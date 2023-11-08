using System;
using Utilities.Graphs;
using Utilities.Graphs.Extensions;

namespace RedScare;

public static class Alternate
{
	public static bool AlternatingPathExists(Graph graph)
	{
		var path = graph.BFS((g, e) => {
			var fromRed = graph.Reds.Contains(graph.Vertices[e.From]);
			var toRed	= graph.Reds.Contains(graph.Vertices[e.To]);
			return fromRed != toRed;
        });
		return path.Count > 0;
    }
}

