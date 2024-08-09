using System.Collections.Generic; 

namespace Studying.Leetcode.Trees
{
    /// <summary>
    /// Given two trees of integers, where A is an origin tree, and B is a modified tree. 
    /// Replace elements of origin tree that were modified, and return a new tree.
    /// Modification of elements could include adding new elements, deleting existing elements, 
    /// changing a value of elements. 
    /// </summary>
    public class ReplaceChangedElementInTree : Studying.Leetcode.ILeetcodeProblem
    {
        private class Node 
        {
            public int Value { get; private set; } = 0; 
            public Node Parent { get; }
            public List<Node> Childs { get; private set; } = new List<Node>(); 

            public Node(int value)
            {
                Value = value; 
            }
            public Node(int value, List<Node> elements)
            {
                if (elements == null) 
                    throw new System.Exception("List of tree elements could not be null");
                Value = value; 
                foreach (Node element in elements) 
                    AddElement(element); 
                // Parent = GetParentOf(this); 
            }

            public void AddElement(Node element)
            {
                if (element == null) 
                    throw new System.Exception("Element of a tree could not be null"); 
                Childs.Add(element); 
            }

            /// <summary>
            /// Gets a full representation of a node, including values and childs of it childs, in a following form:  
            /// [value: 0, childs: [value: 1, childs: []], [values: childs: []]]
            /// </summary>
            public override string ToString()
            {
                return $"[value: {Value}, childs: {ToChildTreeString()}]".Replace(",]", "]"); 
            }
            /// <summary>
            /// Returns a string of a value of a node and its childs
            /// </summary>
            public string ToPureString()
            {
                return $"[value: {Value}, childs: {ToChildsString()}]"; 
            }
            /// <summary>
            /// 
            /// </summary>
            public string ToChildsString()
            {
                string result = "["; 
                foreach (Node element in Childs) 
                    result += element.Value + ","; 
                result += "]"; 
                return result.Replace(",]", "]"); 
            }
            /// <summary>
            /// 
            /// </summary>
            public string ToChildTreeString()
            {
                string result = string.Empty; 
                foreach (Node element in Childs) 
                {
                    result += "[value: " + element.Value + ", childs: "; 
                    result += element.Childs.Count == 0 ? element.ToChildsString() : element.ToChildTreeString(); 
                    result += "],"; 
                }
                return result.Replace(",]", "]"); 
            }
        }

        private class Tree 
        {
            public Node Root { get; }

            public Tree(Node root)
            {
                if (root == null) 
                    throw new System.Exception("Root element of a tree could not be null"); 
                Root = root; 
            }
            
            public Node GetParentOf(Node node)
            {
                return new Node(0); 
            }
            /// <summary>
            /// Checks if any node has only one parent 
            /// </summary>
            public bool IsUndirectedCycle()
            {
                return false; 
            }

            public override string ToString()
            {
                return Root.ToString(); 
            }
        }

        public void Execute()
        {
            System.Console.WriteLine("Replace chanded element in a tree\n".ToUpper()); 

            // Example 1: 
            // input1: [value: 1, childs: [value: 2, childs: [value: 5, childs: []]],[value: 3, childs: []],[value: 4, childs: []]]
            // input2: [value: 5, childs: [value: 2, childs: [value: -6, childs: []]],[value: 3, childs: []],[value: 4, childs: []]]
            Tree input11 = new Tree(new Node(1, new List<Node> { 
                new Node(2, new List<Node> { new Node(5)}),
                new Node(3),
                new Node(4) 
            })); 
            Tree input12 = new Tree(new Node(5, new List<Node> { 
                new Node(2, new List<Node> { new Node(-6)}),
                new Node(3),
                new Node(4) 
            })); 
            Tree output1 = Solve(input11, input12); 
            System.Console.WriteLine("input1: " + input11.ToString()); 
            System.Console.WriteLine("input2: " + input12.ToString()); 
            System.Console.WriteLine("output: " + output1.ToString()); 
        }

        private Tree Solve(Tree tree1, Tree tree2)
        {
            if (tree1 == null || tree2 == null) 
                throw new System.Exception("Input trees could not be null"); 
            return tree2; 
        }
    }
}
