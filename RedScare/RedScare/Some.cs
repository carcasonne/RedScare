using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Graphs;

namespace RedScare;
public static class Some
{
    public static bool SomePathWithReds(Graph graph) =>
        Many.HowManyReds(graph) > 0 ? true : false;
}
