using System;
using Utilities.Graphs;
using Utilities.Graphs.Extensions;

namespace RedScare;

public static class Few
{
	public static int FewestNumberOfRed(Graph graph)
	{
		var rGraph = RedWeightedGraph(graph);
		try
		{
			var path = rGraph.Dijkstra();
			var sourceIsRed = graph.Vertices[graph.Source].IsRed ? 1 : 0;
			return path.Sum(x => x.Weight) + sourceIsRed;
		}
		catch(Exception)
		{
			// No path exists
			return -1;
		}
	}

	// Makea graph, where every edge has weight 0, except the edges going into a red vertex
	private static Graph RedWeightedGraph(Graph graph)
	{
		//var rGraph = new Graph();
		//rGraph.Properties.Add(GraphTypes.Weighted);
		//var vertices = graph.Vertices.Select(x => new Vertex(x.Id, x.Name, x.IsRed)).ToList();

		var criticalEdges = graph.Edges.Where(x => graph.Vertices[x.To].IsRed).ToList();
		foreach (var cEdge in criticalEdges)
			cEdge.Weight = 1;

        graph.Properties.Add(GraphTypes.Weighted);

        return graph;
	}
}

