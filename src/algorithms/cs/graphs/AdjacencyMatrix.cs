using System.Collections.Generic; 
using System.Linq; 

namespace Studying.Leetcode.Graphs
{
    /// <summary>
    /// https://stackoverflow.com/questions/15306040/generate-an-adjacency-matrix-for-a-weighted-graph
    /// </summary>
    public class AdjacencyMatrix : Studying.Leetcode.ILeetcodeProblem
    {
        private class Graph
        {
            public Vertex Root;
            public List<Vertex> AllVertices = new List<Vertex>();

            public Vertex CreateRoot(string name)
            {
                Root = CreateVertex(name);
                return Root;
            }

            public Vertex CreateVertex(string name)
            {
                var n = new Vertex(name);
                AllVertices.Add(n);
                return n;
            }

            public int?[,] CreateAdjMatrix()
            {
                int?[,] adj = new int?[AllVertices.Count, AllVertices.Count];
                for (int i = 0; i < AllVertices.Count; i++)
                {
                    Vertex n1 = AllVertices[i];
                    for (int j = 0; j < AllVertices.Count; j++)
                    {
                        Vertex n2 = AllVertices[j];
                        var edge = n1.Edges.FirstOrDefault(a => a.Child == n2);
                        if (edge != null)
                        {
                            adj[i, j] = edge.Weigth;
                        }
                    }
                }
                return adj;
            }
        }

        private class Vertex
        {
            public string Name { get; set; }
            public List<Edge> Edges { get; set; } = new List<Edge>();  

            public Vertex(string name)
            {
                Name = name; 
            }

            public Vertex AddEdge(Vertex child, int w)
            {
                Edges.Add(new Edge
                {
                    Parent = this,
                    Child = child,
                    Weigth = w
                });
                if (!child.Edges.Exists(a => a.Parent == child && a.Child == this))
                {
                    child.AddEdge(this, w);
                }
                return this;
            }
        }

        private class Edge 
        {
            public int Weigth;
            public Vertex Parent;
            public Vertex Child;
        }

        public void Execute()
        {
            System.Console.WriteLine("Adjacency Matrix".ToUpper()); 
            var graph = new Graph();

            var a = graph.CreateRoot("A");
            var b = graph.CreateVertex("B");
            var c = graph.CreateVertex("C");
            var d = graph.CreateVertex("D");
            var e = graph.CreateVertex("E");
            var f = graph.CreateVertex("F");
            var g = graph.CreateVertex("G");
            var h = graph.CreateVertex("H");
            var i = graph.CreateVertex("I");
            var j = graph.CreateVertex("J");
            var k = graph.CreateVertex("K");
            var l = graph.CreateVertex("L");
            var m = graph.CreateVertex("M");
            var n = graph.CreateVertex("N");
            var o = graph.CreateVertex("O");
            var p = graph.CreateVertex("P");

            a.AddEdge(b, 1)
             .AddEdge(c, 1);

            b.AddEdge(e, 1)
             .AddEdge(d, 3);

            c.AddEdge(f, 1)
             .AddEdge(d, 3);

            c.AddEdge(f, 1)
             .AddEdge(d, 3);

            d.AddEdge(h, 8);

            e.AddEdge(g, 1)
             .AddEdge(h, 3);

            f.AddEdge(h, 3)
             .AddEdge(i, 1);

            g.AddEdge(j, 3)
             .AddEdge(l, 1);

            h.AddEdge(j, 8)
             .AddEdge(k, 8)
             .AddEdge(m, 3);

            i.AddEdge(k, 3)
             .AddEdge(n, 1);

            j.AddEdge(o, 3);

            k.AddEdge(p, 3);

            l.AddEdge(o, 1);

            m.AddEdge(o, 1)
             .AddEdge(p, 1);

            n.AddEdge(p, 1);

            // o - Already added

            // p - Already added

            int?[,] adj = graph.CreateAdjMatrix();
            PrintMatrix(ref adj, graph.AllVertices.Count); 
        }

        public void PrintMatrix(ref int?[,] matrix, int Count)
        {
            System.Console.Write("       ");
            for (int i = 0; i < Count; i++)
            {
                System.Console.Write("{0}  ", (char)('A' + i));
            }
            System.Console.WriteLine();
            for (int i = 0; i < Count; i++)
            {
                System.Console.Write("{0} | [ ", (char)('A' + i));
                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        System.Console.Write(" &,");
                    }
                    else if (matrix[i, j] == null)
                    {
                        System.Console.Write(" .,");
                    }
                    else
                    {
                        System.Console.Write(" {0},", matrix[i, j]);
                    }
                }
                System.Console.Write(" ]\r\n");
            }
            System.Console.Write("\r\n");
        }
    }
}