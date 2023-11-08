using System;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace RedScare;
public static class None
{
    public static int ShortestPathWithoutReds(Graph graph)
    {
        return BreadthFirstSearchAvoidingRed(graph);
    }

    public static int BreadthFirstSearchAvoidingRed(Graph graph)
    {
        var queue = new Queue<Vertex>();
        var explored = new bool[graph.V];
        var parent = new int[graph.V];

        for (int i = 0; i < graph.V; i++)
            parent[i] = -1;

        queue.Enqueue(graph.Vertices[graph.Source]);
        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            foreach (var edge in vertex.Edges)
            {
                var neighbor = graph.Vertices[edge.To];
                var nIsRed = graph.Reds.Contains(neighbor);

                if (!explored[neighbor.Id] && !nIsRed)
                {
                    explored[neighbor.Id] = true;
                    parent[neighbor.Id] = vertex.Id;
                    queue.Enqueue(neighbor);
                }
            }
        }

        var trace = parent[graph.Target];
        if (trace == -1)
            return -1;

        var pathLength = 1;
        while (trace != graph.Source)
        {
            pathLength++;
            trace = parent[trace];
        }

        return pathLength;
    }
}