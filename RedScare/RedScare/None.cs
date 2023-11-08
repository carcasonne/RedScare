using System;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace RedScare;
public class None
{
    public None()
    {

    }

    public string GetShortest(Graph g)
    {
        List<Vertex> p = g.getPath();
        if(p == null) 
        {
            return -1;
            
        } else
        {
            return p.Length;
        }


    }


    
}