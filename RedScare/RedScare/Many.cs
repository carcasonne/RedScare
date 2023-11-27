using Utilities.Graphs;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RedScare;
public static class Many 
{
    // Assumes given graph is directed
    public static int HowManyReds(Graph graph) =>
        Caller(graph) ? -999 : Solve(graph);

    private static int Solve(Graph graph)
    {
        if(graph.IsType(GraphTypes.Undirected))
        {
            return -42;
        }
        // -1 as default value, since a state can have a valid best value of 0
        var optimals = new int[graph.V];
        for (int i = 0; i < graph.V; i++)
            optimals[i] = -1;

        // Dirty way to allocate shared memory between all sub-calls
        var foundTarget = new bool[1];
        foundTarget[0] = false;

        var optimal = OPT(graph.Vertices[graph.Source], graph, optimals, foundTarget);
        return foundTarget[0] ? optimal : -1;
    }

    private static int OPT(Vertex v, Graph g, int[] optimals, bool[] foundTarget) {

        if (optimals[v.Id] != -1)
            return optimals[v.Id];

        var best = 0;
        for (int i = 0; i < v.Edges.Count; i++)
        {
            var edge = v.Edges[i];
            var neigh = g.Vertices[edge.To];
            int lCount = 0;
            // Don't want to continue the recursive call if at the target
            if(neigh.Id == g.Target)
            {
                foundTarget[0] = true;
                lCount = neigh.IsRed ? 1 : 0;
            }
            else
            {
                lCount = OPT(neigh, g, optimals, foundTarget);
            }

            if (lCount > best)
                best = lCount;
        }
        
        if (v.IsRed)
            best++;

        optimals[v.Id] = best;
        return best;
    }

    // Was running into a recursion loop on graphs with >80000 verticec
    // Solution? Make a new thread with 8mb of memory! (.NET has 1mb by default)
    private static bool Caller(Graph graph)
    {
        var value = false;
        Thread T = new Thread(delegate ()
        {
            value = IsCyclic(graph);
        }, 16 * 1024 * 1024);
        T.Start();
        T.Join();
        return value;
    }

    // Taken from: https://www.geeksforgeeks.org/detect-cycle-in-a-graph/
    private static bool IsCyclic(Graph graph)
    {
        // Mark all the vertices as not visited and
        // not part of recursion stack
        bool[] visited = new bool[graph.V];
        bool[] recStack = new bool[graph.V];

        // Call the recursive helper function to
        // detect cycle in different DFS trees
        for (int i = 0; i < graph.V; i++)
            if (isCyclicUtil(graph, i, visited, recStack))
                return true;

        return false;
    }

    // Taken from: https://www.geeksforgeeks.org/detect-cycle-in-a-graph/ 
    private static bool isCyclicUtil(Graph graph, int i, bool[] visited, bool[] recStack)
    {
        // Mark the current node as visited and
        // part of recursion stack
        if (recStack[i])
            return true;

        if (visited[i])
            return false;

        visited[i] = true;
        recStack[i] = true;

        RuntimeHelpers.EnsureSufficientExecutionStack();
        foreach (var edge in graph.Vertices[i].Edges)
        {
            if (isCyclicUtil(graph, graph.Vertices[edge.To].Id, visited, recStack)) 
                return true;
        }

        recStack[i] = false;
        return false;
    }
}
