using System.Collections.Generic; 

namespace Studying.Leetcode.Lists
{
    /// <summary>
    /// Reverse a linked list
    /// </summary>
    public class ReverseLinkedList : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// Node of a linked list 
        /// </summary>
        private class Node
        {
            public int Value { get; private set; } = 0; 
            public Node Next { get; private set; } = null; 

            #region Constructors
            public Node(int value)
            {
                Value = value; 
            }
            public Node(int value, Node next)
            {
                Value = value; 
                SetNext(next); 
            }
            #endregion  // Constructors

            public void SetNext(Node next)
            {
                if (next == null) 
                    throw new System.Exception("Next node could not be null"); 

                Next = next; 
            }

            public string ToValString()
            {
                return Value.ToString(); 
            }
            public override string ToString()
            {
                return "[val: " + Value.ToString() + ", next: " + (Next == null ? "none" : Next.Value.ToString()) + "]"; 
            }
        }

        /// <summary>
        /// Common representation of a linked list 
        /// </summary>
        private class LinkedList 
        {
            public Node Head { get; private set; } = null; 
            public Node Tail { get; private set; } = null; 
            public int Length { get; private set; } = 0; 

            #region Constructors
            public LinkedList()
            {

            }
            public LinkedList(int[] values)
            {
                foreach (int value in values) AddNode(value); 
            }
            #endregion  // Constructors

            public void AddNode(int value)
            {
                Node node = new Node(value); 
                if (Head == null) 
                    Head = node; 
                else 
                    Tail.SetNext(node); 
                Tail = node; 
                Length += 1; 
            }

            public LinkedList GetReversed()
            {
                LinkedList result = new LinkedList();
                List<int> list = ToIntList(); 
                if (list.Count > 0) for (int i = list.Count - 1; i >= 0; i--) result.AddNode(list[i]); 
                return result; 
            }

            public List<int> ToIntList()
            {
                List<int> list = new List<int>();
                List<Node> nodes = ToNodeList(); 
                foreach (Node node in nodes) list.Add(node.Value); 
                return list; 
            }
            public int[] ToIntArray()
            {
                if (Head == null) return new int[0]; 
                List<int> list = ToIntList();
                if (list.Count != Length) 
                    throw new System.Exception("Incorrect length of a linked list (might be caused by losing reference on some of the nodes)"); 
                int[] array = new int[Length]; 
                for (int i = 0; i < Length; i++) array[i] = list[i]; 
                return array; 
            }
            public List<Node> ToNodeList()
            {
                List<Node> list = new List<Node>();
                Node node = Head; 
                while (node != null)
                {
                    list.Add(node); 
                    node = node.Next; 
                }
                return list; 
            }
            public string ToValString()
            {
                string result = "["; 
                List<Node> nodes = ToNodeList(); 
                foreach (Node node in nodes) result += node.ToValString() + ","; 
                result += "]"; 
                return result.Replace(",]", "]"); 
            }
            public override string ToString()
            {
                string result = "["; 
                List<Node> nodes = ToNodeList(); 
                foreach (Node node in nodes) result += node.ToString() + ","; 
                result += "]"; 
                return result.Replace(",]", "]"); 
            } 
        }

        public void Execute()
        {
            System.Console.WriteLine("Reverse a linked list\n".ToUpper()); 

            // Example 1: 
            // Input: [2,5,4,9]
            // Output: [9,4,5,2]
            LinkedList input1 = new LinkedList(new int[] { 2,5,4,9 }); 
            LinkedList output1 = input1.GetReversed(); 
            System.Console.WriteLine("input: " + input1.ToValString()); 
            System.Console.WriteLine("output: " + output1.ToValString()); 
        }
    }
}
