namespace Studying.Leetcode.Lists
{
    /// <summary>
    /// Description: https://leetcode.com/problems/add-two-numbers/
    /// </summary>
    public class AddTwoNumbers : Studying.Leetcode.ILeetcodeProblem 
    {
        /// <summary>
        /// A node of a linked list 
        /// </summary>
        private class Node
        {
            public int Val = 0; 
            public Node Next = null; 
            private string ExceptionMsg = "Value of a node could not be less than 0 and bigger than 9"; 

            public Node(int val)
            {
                if (val < 0 || val > 9) 
                    
                        throw new System.Exception(ExceptionMsg); 
                Val = val; 
            }
            public Node(int val, Node next)
            {
                if (val < 0 || val > 9) 
                    
                        throw new System.Exception(ExceptionMsg); 
                Val = val; 
                Next = next; 
            }
        }

        /// <summary>
        /// Implementation of a linked list 
        /// </summary>
        private class LinkedList
        {
            #region Properties
            /// <summary>
            /// 
            /// </summary>
            public Node FirstNode = null; 
            /// <summary>
            /// 
            /// </summary>
            public Node LastNode = null; 
            /// <summary>
            /// 
            /// </summary>
            public int Length = 0; 
            #endregion  // Properties

            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            public LinkedList()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public LinkedList(int[] nodes)
            {
                foreach (int node in nodes) AddNode(node); 
            }
            
            /// <summary>
            /// 
            /// </summary>
            public LinkedList(System.Collections.Generic.List<int> nodes)
            {
                foreach (int node in nodes) AddNode(node); 
            }
            #endregion  // Constructors

            #region Public methods 
            /// <summary>
            /// 
            /// </summary>
            public void AddNode(int node)
            {
                var nextNode = new Node(node);
                if (FirstNode == null)
                    FirstNode = nextNode;
                else 
                    LastNode.Next = nextNode; 
                LastNode = nextNode; 
                Length += 1; 
            }

            /// <summary>
            /// 
            /// </summary>
            public System.Collections.Generic.List<int> GetIntList()
            {
                var result = new System.Collections.Generic.List<int>(); 
                try
                {
                    Node node = FirstNode; 
                    while (node != null)
                    {
                        result.Add(node.Val); 
                        node = node.Next; 
                    }
                }
                catch (System.Exception ex) 
                {
                    throw; 
                }
                return result; 
            }

            /// <summary>
            /// 
            /// </summary>
            public LinkedList AddLinkedList(LinkedList list)
            {
                var listBigger = new System.Collections.Generic.List<int>(); 
                var listSmaller = new System.Collections.Generic.List<int>(); 
                
                // Get which list is bigger 
                var l1 = this.GetIntList(); 
                var l2 = list.GetIntList(); 
                if (l1.Count > l2.Count)
                {
                    listBigger = l1; 
                    listSmaller = l2; 
                }
                else 
                {
                    listBigger = l2; 
                    listSmaller = l1; 
                }

                // Create new linked list
                LinkedList output = new LinkedList(); 
                int remainder = 0 ;
                for (int i = 0; i < listBigger.Count; i++)
                {
                    int val = listBigger[i] + (i < listSmaller.Count ? listSmaller[i] : 0) + remainder; 
                    remainder = val > 9 ? 1 : 0; 
                    output.AddNode(val > 9 ? val - 10 : val); 
                }
                if (remainder != 0) output.AddNode(remainder); 

                return output; 
            }

            /// <summary>
            /// 
            /// </summary>
            public string GetString()
            {
                string result = string.Empty; 
                try
                {
                    result += "["; 
                    var list = GetIntList(); 
                    for (int i = 0; i < list.Count; i++) 
                    {
                        result += list[i].ToString() + (i != list.Count - 1 ? "," : ""); 
                    }
                    result += "]"; 
                }
                catch (System.Exception ex) 
                {
                    throw; 
                }
                return result; 
            }
            #endregion  // Public methods 
        }

        public void Execute() 
        {
            System.Console.WriteLine("Add Two Numbers\n".ToUpper()); 

            // Example 1:
            // Input: l1 = [2,4,3], l2 = [5,6,4]
            // Output: [7,0,8]
            // Explanation: 342 + 465 = 807.
            LinkedList l11 = new LinkedList(new int[] { 2,4,3 }); 
            LinkedList l12 = new LinkedList(new int[] { 5,6,4 }); 
            LinkedList output1 = Solve(l11, l12); 
            System.Console.WriteLine("Input: l1 = " + l11.GetString() + ", l2 = " + l12.GetString()); 
            System.Console.WriteLine("Output: " + output1.GetString());

            // Example 2:
            // Input: l1 = [0], l2 = [0]
            // Output: [0]
            LinkedList l21 = new LinkedList(new int[] { 0 }); 
            LinkedList l22 = new LinkedList(new int[] { 0 }); 
            LinkedList output2 = Solve(l21, l22); 
            System.Console.WriteLine("Input: l1 = " + l21.GetString() + ", l2 = " + l22.GetString()); 
            System.Console.WriteLine("Output: " + output2.GetString());

            // Example 3:
            // Input: l1 = [9,9,9,9,9,9,9], l2 = [9,9,9,9]
            // Output: [8,9,9,9,0,0,0,1]
            LinkedList l31 = new LinkedList(new int[] { 9,9,9,9,9,9,9 }); 
            LinkedList l32 = new LinkedList(new int[] { 9,9,9,9 }); 
            LinkedList output3 = Solve(l31, l32); 
            System.Console.WriteLine("Input: l1 = " + l31.GetString() + ", l2 = " + l32.GetString()); 
            System.Console.WriteLine("Output: " + output3.GetString());
        }

        private LinkedList Solve(LinkedList l1, LinkedList l2)
        {
            // Constraints:
            // 1. The number of nodes in each linked list is in the range [1, 100].
            // 2. 0 <= Node.val <= 9
            // 3. It is guaranteed that the list represents a number that does not have leading zeros.

            var intList1 = l1.GetIntList(); 
            var intList2 = l2.GetIntList(); 

            if (intList1.Count < 1 || intList1.Count > 100 || intList2.Count < 1 || intList2.Count > 100) 
                throw new System.Exception("Violated constraint: The number of nodes in each linked list is in the range [1, 100]"); 
            foreach (int i in intList1) if (i < 0 || i > 9) 
                throw new System.Exception("Violated constraint: 0 <= Node.val <= 9"); 
            foreach (int i in intList2) if (i < 0 || i > 9) 
                throw new System.Exception("Violated constraint: 0 <= Node.val <= 9"); 
            if ((intList1.Count != 1 && intList1[intList1.Count - 1] == 0) || (intList2.Count != 1 && intList2[intList2.Count - 1] == 0)) 
                throw new System.Exception("Violated constraint: It is guaranteed that the list represents a number that does not have leading zeros"); 
            
            return l1.AddLinkedList(l2); 
        }
    }
}
