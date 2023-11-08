using System;
using Utilities.Graphs;

namespace Utilities.Graphs.Extensions;

public static class GraphExtensions
{
    // Predicate: Function which must return true for a vertex pair in the graph to visit the neighboring edge
    public static List<Edge> BFS(this Graph graph, Func<Graph, Edge, bool> predicate)
    {
        // BFS to remove all
        var explored = new bool[graph.V];
        var parent = new int[graph.V];
        var queue = new Queue<Vertex>();

        for (int i = 0; i < graph.V; i++)
            parent[i] = -1;

        queue.Enqueue(graph.Vertices[graph.Source]);
        explored[graph.Source] = true;

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            foreach (var edge in vertex.Edges)
            {
                var neighbor = graph.Vertices[edge.To];
                if (!explored[neighbor.Id])
                {
                    if(predicate.Invoke(graph, edge))
                    {
                        explored[neighbor.Id] = true;
                        parent[neighbor.Id] = vertex.Id;
                        queue.Enqueue(neighbor);

                    }
                }
            }
        }

        //return explored[graph.Target];
        var path = new List<Edge>();
        if (!explored[graph.Target])
            return path;

        for(var v = graph.Vertices[graph.Target].Id; v != graph.Source; v = parent[v])
        {
            var p = graph.Vertices[parent[v]];
            var edge = p.GetEdgeTo(v);
            path.Add(edge);
        }

        return path;
    }
}

