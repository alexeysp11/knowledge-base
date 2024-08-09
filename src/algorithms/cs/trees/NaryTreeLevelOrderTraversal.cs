using System.Collections.Generic;
using System.Linq;

namespace Studying.Leetcode.Trees
{
    /// <summary>
    /// 
    /// </summary>
    public class NaryTreeLevelOrderTraversal : Studying.Leetcode.ILeetcodeProblem
    {
        public class Node 
        {
            public int Value;
            public IList<Node> Children;

            public Node() {}

            public Node(int value)
            {
                Value = value;
            }

            public Node(int value, IList<Node> children)
            {
                Value = value;
                Children = children;
            }
        }

        public void Execute()
        {
            // Example 1:
            // Input: root = [1,null,3,2,4,null,5,6]
            // Output: [[1],[3,2,4],[5,6]]
            // var rootList01 = new List<int?> {1,null,3,2,4,null,5,6};
            // var root01 = CreateRoot(rootList01);
            var root01 = new Node
            {
                Value = 1,
                Children = new List<Node>
                {
                    new Node
                    {
                        Value = 3,
                        Children = new List<Node>
                        {
                            new Node(5),
                            new Node(6)
                        }
                    },
                    new Node(2),
                    new Node(4)
                }
            };
            var output01 = LevelOrder(root01);
            PrintResult(output01);

            // Example 2:
            // Input: root = [1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14]
            // Output: [[1],[2,3,4,5],[6,7,8,9,10],[11,12,13],[14]]
            var rootList02 = new List<int?> {1,null,2,3,4,5,null,null,6,7,null,8,null,9,10,null,null,11,null,12,null,13,null,null,14};
            var root02 = CreateRoot(rootList02);
            var output02 = LevelOrder(root02);
            PrintResult(output02);
        }

        private Node CreateRoot(IList<int?> elements)
        {
            // Attention: this function should be rewritten because it does not work.
            if (elements == null || elements.Count == 0)
                return null;

            Node root = new Node(elements[0].HasValue ? elements[0].Value : 0);
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            int index = 1;
            while (queue.Count > 0 && index < elements.Count)
            {
                Node current = queue.Dequeue();

                while (index < elements.Count && elements[index].HasValue)
                {
                    Node child = new Node(elements[index].Value);
                    current.Children.Add(child);
                    queue.Enqueue(child);
                    index++;
                }

                if (index < elements.Count && elements[index] == null)
                {
                    index++;
                }
            }
            return root;
        }

        private IList<IList<int>> LevelOrder(Node root)
        {
            var result = new List<IList<int>>();

            if (root == null)
                return result;
            
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            result.Add(new List<int> {root.Value});
            while (queue.Count > 0)
            {
                var localChildren = new List<int>();
                var current = queue.Dequeue();

                if (current.Children == null || !current.Children.Any())
                    continue;
                
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                    localChildren.Add(child.Value);
                }
                result.Add(localChildren);
            }
            return result;
        }

        private void PrintResult(IList<IList<int>> list)
        {
            System.Console.Write("[");
            foreach (var nestedList in list)
            {
                System.Console.Write("[");
                System.Console.Write(string.Join(" ", nestedList));
                System.Console.Write("]");
            }
            System.Console.WriteLine("]");
        }
    }
}
