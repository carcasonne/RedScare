using RedScare;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
using Utilities.GraphFactory;
using Utilities.Graphs;

namespace UnitTests.SystemTests;
public class SystemTests
{
    [Fact]
    public void TestEverything()
    {
        var directory = GetDataDirectory();
        var files = Directory.GetFiles(directory).Where(x => {
            if (x.Contains("README.md"))
                return false;
            StreamReader sr = new StreamReader(x);
            var firstLine = sr.ReadLine()!.Split(' ').Select(Int32.Parse).ToList();
            int n = firstLine[0];
            return n >= 1;
        }).ToList();

        files.Sort();

        var n = files.Count;
        // Name, no. vertices, alternate, few, many, none, some
        var columnNames = new string[] { "Name", "Vertices", "A", "F", "M", "N", "S" };
        var results = new string[n][];
        var times   = new string[n][];
        for (int i = 0; i < n; i++)
        {
            results[i] = new string[columnNames.Length];
            times[i] = new string[columnNames.Length];
        }

        var watch = new Stopwatch();

        watch.Start();
        for (int i = 0; i < n; i++)
        {

            var filepath = files[i].Split('\\').Last();
            var tName = filepath.Substring(0, filepath.Length - 4);
            var graph = GraphParser.ParseGraph(filepath);
            var directed = graph.IsType(GraphTypes.Directed);

            var tVertices = graph.V;

            watch.Restart();
            var tAlternate = Alternate.AlternatingPathExists(graph);
            watch.Stop();
            var alternateTime = watch.Elapsed.TotalSeconds;

            watch.Restart();
            var tNone = None.ShortestPathWithoutReds(graph);
            watch.Stop();
            var noneTime = watch.Elapsed.TotalSeconds;

            watch.Restart();
            var tMany = Many.HowManyReds(graph);
            watch.Stop();
            var manyTime = watch.Elapsed.TotalSeconds;

            watch.Restart();
            var tSome = Some.SomePathWithReds(graph);
            watch.Stop();
            var someTime = watch.Elapsed.TotalSeconds;

            // Do few last, as it modifies the graph
            watch.Restart();
            var tFew = Few.FewestNumberOfRed(graph);
            watch.Stop();
            var fewTime = watch.Elapsed.TotalSeconds;
            
            // Results
            results[i][0] = tName;
            results[i][1] = tVertices.ToString();
            results[i][2] = tAlternate.ToString();
            results[i][3] = tFew.ToString();
            var xd = (directed) ? tMany.ToString() : "Not A";
            results[i][4] = -42 == tMany ? "Not Directed and Acyclic" : tMany.ToString();
            results[i][5] = tNone.ToString();
            results[i][6] = "Not Directed and Acyclic" == results[i][4] ? "Invalid" : tSome.ToString();
            // Times
            times[i][0] = tName;
            times[i][1] = tVertices.ToString();
            times[i][2] = alternateTime.ToString();
            times[i][3] = fewTime.ToString();
            times[i][4] = manyTime.ToString();
            times[i][5] = noneTime.ToString();
            times[i][6] = someTime.ToString();
        }

        var resultTable = results.ToLatexTable(columnNames);
        var timesTable  = times.ToLatexTable(columnNames);

        using (var writer = new StreamWriter(@"C:\Users\Askou\Documents\AlgorithmDesign\RedScare\RedScare\UnitTests\SystemTests\table_results.txt"))
        {
            writer.WriteLine(resultTable);
        }
        using (var writer = new StreamWriter(@"C:\Users\Askou\Documents\AlgorithmDesign\RedScare\RedScare\UnitTests\SystemTests\times_table.txt"))
        {
            writer.WriteLine(timesTable);
        }

        Assert.True(true);
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
