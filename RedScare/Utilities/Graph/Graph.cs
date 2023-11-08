using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


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


  public int BreadthFirstSearchAvoidingRed() 
  {
    var queue = new Queue<Vertex>();
    var explored = new bool[V];
    var parent = new int[V];

    queue.Enqueue(graph.Vertices[graph.Source]);
    while(queue.Count > 0)
    {
        var vertex = queue.Dequeue();
        foreach(var edge in vertex.Edges)
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

    var temp = parent[Target];
    var pathLength = 1;

    while (temp != Source)
    {
        pathLength++;
        temp = parent[temp];
    }

    return pathLength;
  }


}
