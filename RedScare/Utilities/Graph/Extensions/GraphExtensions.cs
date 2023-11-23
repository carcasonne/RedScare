using System;
using Utilities.Graphs;

namespace Utilities.Graphs.Extensions;

public static class GraphExtensions
{
    // Predicate: Function which must return true for a vertex pair in the graph to visit the neighboring edge
    public static List<Edge> BFS(this Graph graph, Func<Graph, Edge, bool> predicate)
    {
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
                    if (predicate.Invoke(graph, edge))
                    {
                        explored[neighbor.Id] = true;
                        parent[neighbor.Id] = vertex.Id;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        var path = new List<Edge>();
        if (!explored[graph.Target])
            return path;

        for (var v = graph.Vertices[graph.Target].Id; v != graph.Source; v = parent[v])
        {
            var p = graph.Vertices[parent[v]];
            var edge = p.GetEdgeTo(v);
            path.Add(edge);
        }

        return path;
    }

    public static List<Edge> Dijkstra(this Graph graph)
    {
        if(!graph.IsType(GraphTypes.Weighted) ||
            graph.IsType(GraphTypes.Cyclic))
        {
            throw new Exception("Running Dijkstra on cyclic or unweighted graph");
        }

        var distance = new int[graph.V];
        var parent = new int[graph.V];
        var queue = new PriorityQueue<Vertex, int>();

        for (int i = 0; i < graph.V; i++)
        {
            parent[i] = -1;
            distance[i] = int.MaxValue;
        }

        var source = graph.Vertices[graph.Source];
        queue.Enqueue(source, 0);
        distance[source.Id] = 0;

        while(queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            var distToV = distance[vertex.Id];
            foreach (var edge in vertex.Edges)
            {
                var neighbor = graph.Vertices[edge.To];
                var distToN = distance[neighbor.Id];
                if (distToN > distToV + edge.Weight)
                {
                    distance[neighbor.Id] = distToV + edge.Weight;
                    parent[neighbor.Id] = vertex.Id;
                    queue.Enqueue(neighbor, distToV + edge.Weight);
                }
            }
        }

        var path = new List<Edge>();
        for (var v = graph.Vertices[graph.Target].Id; v != graph.Source; v = parent[v])
        {
            var p = graph.Vertices[parent[v]];
            var edge = p.GetEdgeTo(v);
            path.Add(edge);
        }

        return path;
    }
}

