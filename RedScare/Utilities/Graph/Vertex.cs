using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Graphs;
public class Vertex
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public List<Edge> Edges { get; set; } = new List<Edge>();
    public Edge GetEdgeTo(int to) => this.Edges.First(x => x.To == to);
    public bool IsRed { get; set; }
    public Vertex(int id, string name, bool isRed)
    {
        Id = id;
        Name = name;
        IsRed = isRed;
    }
}
