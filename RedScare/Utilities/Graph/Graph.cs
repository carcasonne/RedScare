using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Utilities.Graphs;
public class Graph
{
    public string? GraphName { get; set; }
    public List<Vertex> Vertices { get; set; } = new List<Vertex>();
    // Having Reds as a list has turned out to make a lot of heavy Contains() operations
    // Too lazy to change
    public List<Vertex> Reds { get; set; } = new List<Vertex>();
    public List<Edge> Edges { get; set; } = new List<Edge>();
    public ISet<GraphTypes> Properties { get; set; } = new HashSet<GraphTypes>();
    public int Source {  get; set; }
    public int Target { get; set; }
    public int V => Vertices.Count;
    public int E => Edges.Count;
    public bool IsType(GraphTypes type) => Properties.Contains(type);

    public int AddVertex(string name, bool isRed)
    {
        var id = V;
        var vertex = new Vertex(id, name, isRed);
        Vertices.Add(vertex);

        if(isRed)
            Reds.Add(vertex);

        return id;
    }

    public int AddEdge(int from, int to)
    {
        var id = E;
        var edge = new Edge(id, from, to);
        Edges.Add(edge);
        Vertices[from].Edges.Add(edge);
        
        // IF undirected, allow traversel in the other direction
        if(IsType(GraphTypes.Undirected))
        {
            var reverseEdge = new Edge(id + 1, to, from);
            Edges.Add(reverseEdge);
            Vertices[to].Edges.Add(reverseEdge);
        }

        return id;
    }
}
