using System.Collections.Generic; 

namespace Studying.Leetcode.Graphs
{
    /// <summary>
    /// Description: https://leetcode.com/problems/clone-graph/description/ 
    /// </summary>
    public class CloneGraph : Studying.Leetcode.ILeetcodeProblem
    {
        private class Node 
        {
            public int val; 
            public List<Node> neighbors; 

            public Node() 
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val) 
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors) 
            {
                val = _val;
                neighbors = _neighbors;
            }

            public string GetNeighborsString()
            {
                string result = "["; 
                foreach (var neighbor in neighbors)
                {
                    result += neighbor.val.ToString() + (neighbor.val != neighbors[neighbors.Count - 1].val ? "," : ""); 
                }
                result += "]"; 
                return result; 
            }
        }

        public void Execute()
        {
            System.Console.WriteLine("Clone Graph\n".ToUpper());
            // Input: adjList = [[2,4],[1,3],[2,4],[1,3]]
            // Output: [[2,4],[1,3],[2,4],[1,3]]
            List<Node> input1 = new List<Node>() { 
                new Node(1, new List<Node>() { new Node(2), new Node(4) }), 
                new Node(2, new List<Node>() { new Node(1), new Node(3) }),
                new Node(3, new List<Node>() { new Node(2), new Node(4) }),
                new Node(4, new List<Node>() { new Node(1), new Node(3) }) 
            }; 
            List<Node> output1 = Clone(input1);
            Print(input1);
            Print(output1);

            // Input: adjList = [[]]
            // Output: [[]]
            List<Node> input2 = new List<Node>() { new Node(1) }; 
            List<Node> output2 = Clone(input2);
            Print(input2);
            Print(output2);

            // Input: adjList = []
            // Output: []
            List<Node> input3 = new List<Node>(); 
            List<Node> output3 = Clone(input3);
            Print(input3);
            Print(output3);
        }

        private List<Node> Clone(List<Node> input)
        {
            // Constraints:
            // 1. The number of nodes in the graph is in the range [0, 99].
            // 2. 1 <= Node.val <= 100
            // 3. Node.val is unique for each node.
            // 4. There are no repeated edges and no self-loops in the graph.
            // 5. The Graph is connected and all nodes can be visited starting from the given node.

            List<Node> output = new List<Node>(); 
            try
            {
                if (input.Count > 99) 
                    throw new System.Exception("The number of nodes in the graph should be in the range [0, 99]"); 
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i].val < 1 || input[i].val > 100) 
                        throw new System.Exception("Violated constraint: 1 <= Node.val <= 100"); 
                    for (int j = i + 1; j < input.Count; j++) if (input[i].val == input[j].val) 
                        throw new System.Exception("Violated constraint: no repeated edges");
                    foreach (var neighbor in input[i].neighbors) if (input[i].val == neighbor.val) 
                        throw new System.Exception("Violated constraint: no self-loops in the graph"); 

                    output.Add(new Node(input[i].val, input[i].neighbors)); 
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message); 
            }
            return output; 
        }

        private void Print(List<Node> list)
        {
            System.Console.Write("["); 
            for (int i = 0; i < list.Count; i++) System.Console.Write(list[i].GetNeighborsString() + (i != list.Count - 1 ? "," : "")); 
            System.Console.Write("]\n"); 
        }
    }
}
