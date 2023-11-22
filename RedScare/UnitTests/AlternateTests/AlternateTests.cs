using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedScare;
using Utilities.GraphFactory;
using Utilities.Graphs;
using Utilities.Extensions;

namespace UnitTests.AlternateTests;

public class AlternateTests
{
    [Theory]
    // Examples from the PDF for initial assurance
    [InlineData("G-ex.txt",         false, true)]
    [InlineData("common-1-20.txt",  true,  false)]
    [InlineData("P3.txt",           false, true)]
    // Grids: Diagonal exists with a valid path
    // No directed grids have a valid path
    [InlineData("grid-5-0.txt",     true,  false)]
    [InlineData("grid-10-0.txt",    true,  false)]
    [InlineData("grid-25-0.txt",    true,  false)]
    [InlineData("grid-50-0.txt",    true,  false)]
    [InlineData("grid-5-0.txt",     false, true)]
    [InlineData("grid-10-0.txt",    false, false)] // Grids where N is even do not have a valid path  
    [InlineData("grid-25-0.txt",    false, true)]
    [InlineData("grid-50-0.txt",    false, false)] // Grids where N is even do not have a valid path  
    // Walls: Not too interesting, makes sure no paths are found on dominantly non-red graphs
    [InlineData("wall-n-1.txt",     false, false)]
    [InlineData("wall-n-10.txt",    false, false)]
    [InlineData("wall-n-100.txt",   false, false)]
    [InlineData("wall-n-1000.txt",  false, false)]
    [InlineData("wall-n-10000.txt", false, false)]

    public void Graph_Given_File_Properties_Hold(string filename, bool makeDirected, bool expected)
    {
        var graph = GraphParser.ParseGraph(filename, makeDirected);
        var actual = Alternate.AlternatingPathExists(graph);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("G-ex.txt", false)]
    [InlineData("common-1-20.txt", true)]
    [InlineData("P3.txt", false)]
    [InlineData("grid-5-0.txt", true)]
    [InlineData ("grid-10-0.txt", true)]
    [InlineData("grid-25-0.txt", true)]
    [InlineData("grid-50-0.txt", true)]
    [InlineData("grid-5-0.txt", false)]
    [InlineData("grid-10-0.txt", false)]
    [InlineData("grid-25-0.txt", false)]
    [InlineData("grid-50-0.txt", false)]
    [InlineData("wall-n-1.txt", false)]
    [InlineData("wall-n-10.txt", false)]
    [InlineData("wall-n-100.txt", false)]
    [InlineData("wall-n-1000.txt", false)]
    [InlineData("wall-n-10000.txt", false)]
    public void Graph_Flipping_Reds_And_Blacks_Same_Result(string filename, bool makeDirected)
    {
        var graph = GraphParser.ParseGraph(filename, makeDirected);
        var expected = Alternate.AlternatingPathExists(graph);

        // Flip
        var nonReds = graph.Vertices.Where(x => !graph.Reds.Contains(x))
                                    .ToList();
        graph.Reds = nonReds;
        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    // A modified version of P3, where the Red vertex is moved to make the problem impossible
    [Fact]
    public void Graph_P3_1_Finds_No_Path()
    {
        var expected = false;

        var graph = GraphParser.ParseGraph("P3.txt", false);
        graph.Reds = new List<Vertex>()
        {
            graph.Vertices[graph.Target]
        };

        var actual = Alternate.AlternatingPathExists(graph);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void shit()
    {
        var directory = GetDataDirectory();
        var files = Directory.GetFiles(directory).Single(x => x.Contains("ski-level3-3"));

        var filepath = files.Split('/').Last();
        var directedGraph = GraphParser.ParseGraph(filepath, true);
        var tMany = Many.HowManyReds(directedGraph);
        Assert.Equal(1, tMany);
    }

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
            return n >= 500;
        }).ToList();

        files.Sort();

        var n = files.Count;
        // Name, no. vertices, alternate, few, many, none, some
        var columnNames = new string[] {"Name", "Vertices", "A", "F", "M", "N", "S"};
        var results = new string[n][];
        for(int i = 0; i < n; i++)
            results[i] = new string[columnNames.Length];

        for(int i = 0; i < n; i++) 
        {
            var filepath = files[i].Split('/').Last();
            var tName = filepath.Substring(0, filepath.Length - 4);
            var graph = GraphParser.ParseGraph(filepath, false);
            var tVertices = graph.V;
            var tAlternate = Alternate.AlternatingPathExists(graph);
            var tNone = None.ShortestPathWithoutReds(graph);
            var tFew = Few.FewestNumberOfRed(graph);

            if(tName.Contains("ski") || tName.Contains("increase"))
            {
                var directedGraph = GraphParser.ParseGraph(filepath, true);
                var tMany = Many.HowManyReds(directedGraph);
                results[i][4] = tMany == -999 ? "Not acyclic" : tMany.ToString();
            }
            else
            {
                results[i][4] = "Not directed";
            }

            //var tSome =
            results[i][0] = tName;
            results[i][1] = tVertices.ToString();
            results[i][2] = tAlternate.ToString();
            results[i][3] = tFew.ToString();
            results[i][5] = (tNone == -1 ? false : true).ToString();

        }

        var table = results.ToLatexTable(columnNames);

        using (var writer = new StreamWriter(@"/Users/sonne/Desktop/table.txt"))
        {
            writer.WriteLine(table);
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

