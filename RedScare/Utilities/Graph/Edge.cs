using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Graphs;
public class Edge
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    // TODO: move weight to its own class?
    public int? Weight { get; set; }

    // TODO: move flow shit to its own class?
    public int? Capacity { get; set; }
    public int? Flow { get; set; } = 0;
    public char? Sign { get; set; }
    public bool? IsReverse { get; set; } = false;
    public int? ResidualCapacity => Capacity - Flow;

    public Edge(int id, int from, int to)
    {
        Id = id;
        From = from;
        To = to;
    }
}
