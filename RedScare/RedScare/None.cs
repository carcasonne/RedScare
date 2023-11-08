using System;
using Utilities.GraphFactory;
using Utilities.Graphs;
using Utilities.Graphs.Extensions;

namespace RedScare;
public static class None
{
    public static int ShortestPathWithoutReds(Graph graph)
    {
        var path = graph.BFS((g, e) => {
            return !g.Reds.Contains(graph.Vertices[e.To]);
        });
        return path.Count > 0 ? path.Count : -1;
    }
}