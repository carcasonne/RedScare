using Utilities.Graphs;
using System;

namespace RedScare;
public static class Many 
{
    public static int HowManyReds(Graph graph) => Solve(graph);
        //IsAcyclical(graph, graph.Vertices[graph.Source], new HashSet<int>()) ? 1 : -999;

    private static int Solve(Graph graph)
    {
        var optimals = new int[graph.V];
        for (int i = 0; i < graph.V; i++)
            optimals[i] = -1;

        return OPT(graph.Vertices[graph.Source], graph, optimals);
       
        


        
        
        return 0;
    }

    private static int OPT(Vertex v, Graph g, int[] optimals) {

        if (optimals[v.Id] != -1)
        {
            return optimals[v.Id];
        }

        var best = 0;
        for (int i = 0; i < v.Edges.Count; i++)
        {
            var edge = v.Edges[i];
            var neigh = g.Vertices[edge.To];

            var lCount = 0;
            if(neigh.Id == g.Target)
            {
                lCount = neigh.IsRed ? 1 : 0;
            }
            else
            {
                lCount = OPT(neigh, g, optimals);
            }

            if (lCount > best)
                best = lCount;
        }
        
        if (v.IsRed)
        {
            best++;
        }
        optimals[v.Id] = best;
        return best;
    }

    private static bool IsAcyclical(Graph graph, Vertex vertex, HashSet<int> path)
    {
        if (path.Contains(vertex.Id))
            return true;

        var extendedPath = new HashSet<int>(path) {vertex.Id};

        foreach (var edge in vertex.Edges)
        {
            var u = graph.Vertices[edge.To];
            if (IsAcyclical(graph, u, extendedPath))
                return true;
        }

        return false;
    }
}