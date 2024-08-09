using System.Collections.Generic; 

namespace Studying.Leetcode.Graphs
{
    /// <summary>
    /// Description: https://leetcode.com/problems/largest-color-value-in-a-directed-graph/
    /// </summary>
    public class LargestColorInDirectedGraph : Studying.Leetcode.ILeetcodeProblem
    {
        private class DirectedEdge 
        {
            public int SrcNode; 
            public int DstNode; 

            public DirectedEdge(int srcNode, int dstNode)
            {
                SrcNode = srcNode; 
                DstNode = dstNode; 
            }
        }

        public void Execute()
        {
            System.Console.WriteLine("Largest Color Value in a Directed Graph\n".ToUpper()); 

            // Input: colors = "abaca", edges = [[0,1],[0,2],[2,3],[3,4]]
            // Output: 3
            // Explanation: The path 0 -> 2 -> 3 -> 4 contains 3 nodes that are colored "a" (red in the above image).
            DirectedEdge[] edges1 = new DirectedEdge[] { 
                new DirectedEdge(0, 1), 
                new DirectedEdge(0, 2), 
                new DirectedEdge(2, 3), 
                new DirectedEdge(3, 4)
            }; 
            char[] colors1 = "abaca".ToCharArray(); 
            int output1 = Find(edges1, colors1); 
            System.Console.Write("edges: "); 
            Print(edges1); 
            System.Console.WriteLine("output: " + output1.ToString()); 

            // Input: colors = "a", edges = [[0,0]]
            // Output: -1
            // Explanation: There is a cycle from 0 to 0.
            DirectedEdge[] edges2 = new DirectedEdge[] { new DirectedEdge(0, 0) }; 
        }

        private int Find(DirectedEdge[] edges, char[] colors)
        {
            // Constraints:
            // 1. n == colors.length (nodes in graph)
            // 2. m == edges.length (edges in graph)
            // 3. 1 <= n <= 105
            // 4. 0 <= m <= 105
            // 5. colors consists of lowercase English letters.
            // 6. 0 <= aj, bj < n

            try
            {
                if (colors.Length < 1 || colors.Length > 105) 
                    throw new System.Exception("Violated constraint: 1 <= colors.length <= 105"); 
                if (edges.Length > 105) 
                    throw new System.Exception("Violated constraint: 0 <= edges.length <= 105"); 
                foreach (char c in colors) if (!char.IsLetter(c) && !char.IsLower(c)) 
                    throw new System.Exception("Violated constraint: colors consists of lowercase English letters"); 

                // Get paths from edges 
                List<int[]> paths = GetPaths(edges);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message); 
            }
            return -1; 
        }

        private List<int[]> GetPaths(DirectedEdge[] edges)
        {
            List<int[]> paths = new List<int[]>(); 
            foreach (DirectedEdge edge in edges) AddToPaths(ref paths, edge); 
            return paths; 
        }

        private void AddToPaths(ref List<int[]> paths, DirectedEdge edge)
        {
            bool isAdded = false; 
            List<int[]> tmpPaths = new List<int[]>(); 
            foreach (var path in paths)
            {
                tmpPaths.Add(path); 
                if (path.Length != 0 && edge.SrcNode == path[path.Length - 1]) 
                {
                    int[] tmp = new int[path.Length + 1]; 
                    for (int i = 0; i < path.Length; i++) tmp[i] = path[i]; 
                    tmp[path.Length] = edge.DstNode; 
                    tmpPaths.RemoveAt(paths.Count - 1); 
                    tmpPaths.Add(tmp); 
                    isAdded = true; 
                }
            }
            paths = tmpPaths; 
            if (!isAdded) paths.Add(new int[] { edge.SrcNode, edge.DstNode }); 
        }

        private void Print(DirectedEdge[] array)
        {
            System.Console.Write("["); 
            for (int i = 0; i < array.Length; i++) System.Console.Write("[" + array[i].SrcNode.ToString() + "," + array[i].DstNode.ToString() + "]" + (i != array.Length - 1 ? "," : "")); 
            System.Console.Write("]\n"); 
        }
    }
}