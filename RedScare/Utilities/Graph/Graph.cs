using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Graphs;
public class Graph
{
    public List<Vertex> Vertices { get; set; } = new List<Vertex>();
    public List<Vertex> Reds { get; set; } = new List<Vertex>();
    public List<Edge> Edges { get; set; } = new List<Edge>();
    public ISet<GraphTypes> GraphTypes { get; set; } = new HashSet<GraphTypes>();
    public int Source {  get; set; }
    public int Target { get; set; }
    public int V => Vertices.Count;
    public int E => Edges.Count;

    public int AddVertex(string name, bool isRed)
    {
        var id = V;
        var vertex = new Vertex(id, name);
        Vertices.Add(vertex);

        if(isRed)
            Reds.Add(vertex);

        return id;
    }

    // Currently this assumes a directed graph
    // Can be made undirected by also adding a reverse edge to the to node
    public int AddEdge(int from, int to)
    {
        var id = E;
        var edge = new Edge(id, from, to);
        Edges.Add(edge);
        Vertices[from].Edges.Add(edge);

        return id;
    }
}
