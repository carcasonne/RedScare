using System;
using Utilities.Graphs;
using Utilities.Graphs.Extensions;

namespace RedScare;

public static class Alternate
{
	public static bool AlternatingPathExists(Graph graph)
	{
		return Solve(graph);
    }

	// Returns a new graph instance of removing all non-alternating colors starting from the source.
	private static bool Solve(Graph graph)
	{
        // BFS to remove all
        var explored = new bool[graph.V];
        var parent = new int[graph.V];
		var queue = new Queue<Vertex>();

		for(int i = 0; i < graph.V; i++)
            parent[i] = -1;

        queue.Enqueue(graph.Vertices[graph.Source]);
		explored[graph.Source] = true;

		while(queue.Count > 0)
		{
			var vertex = queue.Dequeue();
			var isRed = graph.Reds.Contains(vertex);
			foreach(var edge in vertex.Edges)
			{
				var neighbor = graph.Vertices[edge.To];
				var nIsRed = graph.Reds.Contains(neighbor);
				if (!explored[neighbor.Id] && isRed != nIsRed)
				{
					explored[neighbor.Id] = true;
					parent[neighbor.Id] = vertex.Id;
					queue.Enqueue(neighbor);
                }
			}
		}

		return explored[graph.Target];
	}
}

