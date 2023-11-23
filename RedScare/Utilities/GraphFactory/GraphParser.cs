using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities.Graphs;

namespace Utilities.GraphFactory;
public class GraphParser
{
    public static Graph ParseGraph(string filename, bool isDirected)
    {
        var directory = GetDataDirectory();

        StreamReader sr = new StreamReader($"{directory}/{filename}");
        var firstLine = sr.ReadLine()!.Split(' ').Select(Int32.Parse).ToList();
        var secondLine = sr.ReadLine()!.Split(' ').ToList();
        int n = firstLine[0];
        int m = firstLine[1];
        int r = firstLine[2];
        string s = secondLine[0];
        string t = secondLine[1];

        var graph = new Graph();
        graph.GraphName = filename;
        var nameToId = new Dictionary<string, int>();

        var type = isDirected ? GraphTypes.Directed : GraphTypes.Undirected;
        graph.Properties.Add(type);

        // Add all vertices
        for (int i = 0; i < n; i++)
        {
            var line = sr.ReadLine()!.Trim().Split(' ').ToList();
            var name = line[0];
            var isRed = line.Count > 1; // No need to explictly match the '*'
            var id = graph.AddVertex(name, isRed);
            nameToId[name] = id;
        }

        // Set 'source' and 'target' of graph
        graph.Source = nameToId[s];
        graph.Target = nameToId[t];

        // Add all edges
        for(int i = 0; i < m; i++)
        {
            var line = sr.ReadLine()!.Split(' ').ToList();
            var from = line[0];
            var to = line[2];
            graph.AddEdge(nameToId[from], nameToId[to]);
        }

        sr.Close();
        return graph;
    }

    private static string GetDataDirectory()
    {
        var directory = Directory.GetCurrentDirectory();
        // Necessary to do some fuckery due to multiple directories with the same name
        var firstIndex = directory.IndexOf("RedScare");
        while (directory.LastIndexOf("RedScare") != firstIndex)
        {
            var parent = Directory.GetParent(directory)!;
            directory = parent.FullName;
        }
        var dataDirectory = $"{directory}/Data";
        return dataDirectory;
    }
}
